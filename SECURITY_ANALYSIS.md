# SQL Security Analysis: C# Todo App

SQL injection analysis of the C# todo app built on the Generic Temper ORM. This analysis focuses exclusively on SQL generation and execution — the core value proposition of the ORM.

**Analysis Date:** 2026-03-12
**Framework:** ASP.NET Core Razor Pages + Microsoft.Data.Sqlite
**ORM:** Generic Temper ORM (compiled to C#)

---

## How This App Uses the ORM

All user-facing CRUD operations flow through the Temper ORM's type-safe SQL generation:

| Operation | Method | SQL Source |
|-----------|--------|------------|
| SELECT lists/todos | `SrcGlobal.From(SafeIdentifier(...)).Where(...).ToSql()` | ORM |
| INSERT list/todo | `SrcGlobal.Changeset(table, params).Cast(fields).ValidateRequired(fields).ToInsertSql()` | ORM |
| UPDATE list name | `SrcGlobal.Changeset(table, params).Cast(fields).ValidateRequired(fields).ToUpdateSql(id)` | ORM |
| UPDATE todo title | `SrcGlobal.Changeset(table, params).Cast(fields).ToUpdateSql(id)` | ORM |
| DELETE list/todo | `SrcGlobal.DeleteSql(table, id)` | ORM |
| WHERE clauses | `SqlBuilder.AppendSafe()` + `.AppendInt32()` | ORM |
| Toggle completed | `UPDATE todos SET completed = CASE ... WHERE id = {SqlBuilder}` | ORM (via SqlBuilder) |
| DDL | `CREATE TABLE IF NOT EXISTS ...` | Raw (static) |

### ORM SQL Generation Path

```
User input (form field)
  → ASP.NET model binding (Request.Form["Name"])
    → Temper Map construction
      → SrcGlobal.Changeset(tableDef, paramsMap)     [factory — sealed interface]
        → .Cast(allowedFields)                        [SafeIdentifier whitelist]
          → .ValidateRequired(fields)                 [non-null enforcement]
            → .ToInsertSql()                          [type-dispatched escaping]
              → .ToString()                           [rendered SQL string]
                → cmd.CommandText = sql; cmd.ExecuteNonQuery()  [ADO.NET]
```

For queries:
```
Route parameter (@page "{id:int}")
  → Razor Pages int constraint (guaranteed integer)
    → SqlBuilder.AppendInt32(id)                     [bare integer]
      → SrcGlobal.From(safeId).Where(fragment).ToSql()
        → cmd.ExecuteReader()
```

---

## SQL Injection Analysis

### ORM-Generated SQL: Protected

**SafeIdentifier validation** — `SrcGlobal.SafeIdentifier()` validates against `[a-zA-Z_][a-zA-Z0-9_]*`. C#'s compiled ORM preserves the sealed-interface pattern.

**SqlString escaping** — String values pass through `SqlString` which escapes `'` → `''`.

**Changeset field whitelisting** — `Cast()` requires `IList<SafeIdentifier>`, preventing mass assignment.

**Razor Pages `{id:int}` constraint** — Route parameters are type-constrained to `int` by ASP.NET's routing. Non-integer path segments return 404 at the framework level.

**Toggle via SqlBuilder** — Unlike most other apps that use raw SQL for toggle, this app constructs the toggle query using `SqlBuilder.AppendSafe()` + `.AppendInt32()`, keeping even the toggle operation within the ORM's type-safe boundary.

### Raw SQL: Minimal

The only raw SQL in this app is DDL (`CREATE TABLE`) and `PRAGMA` statements — both hardcoded with no user input.

```csharp
// DDL — static
cmd.CommandText = @"CREATE TABLE IF NOT EXISTS lists (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    created_at TEXT DEFAULT (datetime('now'))
)";

// Pragma — static
cmd.CommandText = "PRAGMA foreign_keys = ON";
```

**This app has the highest ORM coverage of all 6 apps** — even toggle-completed uses `SqlBuilder` rather than raw SQL.

### DDL: Static

All schema creation uses hardcoded SQL strings with no dynamic content.

---

## Findings

| # | Severity | CWE | Finding |
|---|----------|-----|---------|
| CS-SQL-1 | INFO | CWE-89 | ORM SQL executed via `cmd.CommandText = sql; cmd.ExecuteNonQuery()` as a pre-rendered string. The escaping is correct, but ADO.NET's `SqliteParameter` with `@param` placeholders would add defense-in-depth. |
| CS-SQL-2 | INFO | CWE-400 | SELECT queries use `ToSql()` without limits. `SafeToSql(defaultLimit)` available but unused. |

### ORM-Level Concerns (shared across all apps)

| # | Severity | CWE | Finding |
|---|----------|-----|---------|
| ORM-1 | MEDIUM | CWE-89 | `ToInsertSql`/`ToUpdateSql` pass `pair.Key` (a `String`) to `AppendSafe`. Safe by construction but not type-enforced. |
| ORM-2 | LOW | CWE-89 | `SqlDate.FormatTo` wraps `value.ToString()` in quotes without escaping. |
| ORM-3 | LOW | CWE-20 | `SqlFloat64.FormatTo` can produce `NaN`/`Infinity`. |

---

## Verdict

**No SQL injection vulnerabilities found.** This app has the highest ORM coverage of all 6 apps — all CRUD operations including toggle-completed use the ORM's type-safe SQL generation. The only raw SQL is static DDL and pragma statements. No user input ever reaches SQL via string concatenation.
