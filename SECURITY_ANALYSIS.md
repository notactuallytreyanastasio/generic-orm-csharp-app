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

---

## Evolution: Temper-Level Remediation

**Date:** 2026-03-12
**Commit:** [`1df8c7a`](https://github.com/notactuallytreyanastasio/generic_orm/commit/1df8c7a)

The security analysis above identified 3 ORM-level concerns (ORM-1, ORM-2, ORM-3) shared across all 6 app implementations. Because the ORM is written once in Temper and compiled to all backends, fixing these issues at the Temper source level automatically resolves them in every language — including this C# app.

### What Changed

**ORM-1 (MEDIUM → RESOLVED): Column name type safety in INSERT/UPDATE SQL**

The `ToInsertSql()` and `ToUpdateSql()` methods previously passed `pair.Key` (a raw `string`) to `AppendSafe()`. While safe by construction (keys originated from `Cast()` via `SafeIdentifier.SqlValue`), the type system didn't enforce this. A future refactor could have silently introduced an unvalidated code path.

The fix routes column names through the looked-up `FieldDef.Name.SqlValue` — a `SafeIdentifier` — so the column name in the generated SQL always comes from a validated identifier, not a raw map key.

**ORM-2 (LOW → RESOLVED): SqlDate quote escaping**

`SqlDate.FormatTo()` previously wrapped `value.ToString()` in quotes without escaping. The fix adds character-by-character quote escaping identical to `SqlString.FormatTo()`, ensuring defense-in-depth against any future Date format that might contain single quotes.

**ORM-3 (LOW → RESOLVED): SqlFloat64 NaN/Infinity handling**

`SqlFloat64.FormatTo()` previously called `value.ToString()` directly, which could produce `NaN`, `Infinity`, or `-Infinity` — none valid SQL literals. The fix checks for these values and renders `NULL` instead, which is the safest SQL representation for non-representable floating-point values.

### Why This Matters

This is the core value proposition of a cross-compiled ORM: **one fix in Temper source propagates to all 6 backends simultaneously.** The same commit that fixed the C# compiled output also fixed JavaScript, Python, Rust, Java, and Lua. No per-language patches needed. No risk of inconsistent fixes across implementations.

### Updated Status

| Finding | Original | Current | Resolution |
|---------|----------|---------|------------|
| ORM-1 | MEDIUM | RESOLVED | Column names routed through `SafeIdentifier` |
| ORM-2 | LOW | RESOLVED | `SqlDate.FormatTo()` now escapes quotes |
| ORM-3 | LOW | RESOLVED | `SqlFloat64.FormatTo()` renders NaN/Infinity as `NULL` |
| ORM-4 | INFO | ACKNOWLEDGED | Design limitation — escaping-based, not parameterized |
