# SQL Security Analysis: C# Todo App

SQL injection analysis of the C# todo app built on the Generic Temper ORM. This analysis focuses exclusively on SQL generation and execution — the core value proposition of the ORM.

**Analysis Date:** 2026-03-12
**Updated:** 2026-03-13 (SQL-only scope, JOIN feature analysis)
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

## JOIN Feature Security Analysis

The ORM recently added JOIN support (INNER, LEFT, RIGHT, FULL OUTER JOIN). This section analyzes the security properties of the new feature.

### Architecture

The JOIN system introduces three new types:

1. **`JoinType`** -- A sealed interface with four concrete implementations (`InnerJoin`, `LeftJoin`, `RightJoin`, `FullJoin`). Each returns a hardcoded SQL keyword string from its `keyword()` method (e.g., `"INNER JOIN"`, `"LEFT JOIN"`). Because the interface is sealed and the keyword values are compile-time constants, there is no injection vector through `JoinType`.

2. **`JoinClause`** -- A value type holding `(joinType: JoinType, table: SafeIdentifier, onCondition: SqlFragment)`. Both `table` and `onCondition` are typed -- `SafeIdentifier` for the table name, `SqlFragment` for the ON condition. No raw strings accepted.

3. **`col()` helper** -- `col(table: SafeIdentifier, column: SafeIdentifier): SqlFragment` constructs qualified column references (`table.column`). Both components require `SafeIdentifier`, preventing injection through qualified names.

### SQL Generation Path

In `Query.toSql()`, JOINs are rendered as:

```
for (let jc of joinClauses) {
  b.appendSafe(" ");                    // hardcoded literal
  b.appendSafe(jc.joinType.keyword());  // hardcoded keyword from sealed interface
  b.appendSafe(" ");                    // hardcoded literal
  b.appendSafe(jc.table.sqlValue);      // SafeIdentifier-validated table name
  b.appendSafe(" ON ");                 // hardcoded literal
  b.appendFragment(jc.onCondition);     // SqlFragment (type-safe parts)
}
```

Every component is either a hardcoded string literal or a validated type:

| Component | Type | Injection Risk |
|-----------|------|----------------|
| `" "` | String literal | None |
| `jc.joinType.keyword()` | Sealed interface, hardcoded return values | None |
| `jc.table.sqlValue` | `SafeIdentifier` validated against `[a-zA-Z_][a-zA-Z0-9_]*` | None |
| `" ON "` | String literal | None |
| `jc.onCondition` | `SqlFragment` composed of `ISqlPart` implementations | None (same safety as WHERE clauses) |

### ON Condition Safety

The `onCondition` parameter is typed as `SqlFragment`, not `String`. This means:
- It must be constructed through `SqlBuilder` methods (`appendSafe`, `appendInt32`, `appendString`, etc.)
- User values can only enter via typed append methods (`appendString` uses `SqlString` with quote escaping, `appendInt32` uses `SqlInt32` with integer formatting)
- The `col()` helper produces `SafeIdentifier.SafeIdentifier`-based fragments for column references

The ON condition has the exact same security properties as WHERE conditions -- the same `SqlFragment` type, the same `SqlBuilder` construction pattern, the same `ISqlPart` type dispatch for value escaping.

### Findings

| # | Severity | CWE | Finding |
|---|----------|-----|---------|
| JOIN-1 | INFO | -- | JOIN table names require `SafeIdentifier`, which prevents injection. The same `[a-zA-Z_][a-zA-Z0-9_]*` validation used for FROM table names applies to JOIN table names. |
| JOIN-2 | INFO | -- | ON conditions use `SqlFragment`, providing the same type-safe escaping as WHERE conditions. No new injection surface introduced. |
| JOIN-3 | INFO | -- | `JoinType` is a sealed interface with hardcoded keyword strings. No extensibility vector for injecting arbitrary SQL keywords. |
| JOIN-4 | INFO | CWE-400 | JOINs can produce Cartesian products or large result sets. The existing `SafeToSql(defaultLimit)` applies a LIMIT after the JOIN, but if not used, an unbounded JOIN could cause resource exhaustion. This is the same concern as CS-SQL-2 -- the JOIN feature does not make it worse, but it does not improve it either. |
| JOIN-5 | INFO | -- | The C# Todo App does not currently use JOIN functionality. The lists-to-todos relationship is resolved via N+1 queries (one query per list in `GetAllLists()`). If the app adopted JOINs to replace this pattern, the security properties would be maintained because the `WhereEq` helper already constructs `SqlFragment` conditions via `SqlBuilder`, and the same pattern would apply to ON conditions. |

### JOIN Security Verdict

**The JOIN feature introduces no new injection vectors.** It follows the same `SafeIdentifier` + `SqlFragment` type-safety pattern used by the rest of the ORM. Table names in JOINs are validated identifiers. ON conditions are typed fragments. JOIN keywords are hardcoded sealed-interface values. The feature is a safe, consistent extension of the existing security model.

The only concern is the amplification of CWE-400 (unbounded queries) -- JOINs can produce larger result sets than single-table queries, making the use of `SafeToSql(defaultLimit)` even more important. This is not a new vulnerability but an existing concern (CS-SQL-2) with increased relevance.

---

## Evolution: Temper-Level Remediation

**Date:** 2026-03-12
**Commit:** [`1df8c7a`](https://github.com/notactuallytreyanastasio/alloy/commit/1df8c7a)

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

---

## SQL-Related CWE Analysis

This section maps SQL-specific Common Weakness Enumerations from the MITRE CWE database against this application.

| CWE ID | Name | Status | Details |
|--------|------|--------|---------|
| CWE-89 | SQL Injection | **Mitigated** | All CRUD operations use the ORM's type-safe SQL generation. User strings go through `SqlString` (`'` -> `''`). Integer IDs go through `SqlBuilder.AppendInt32()`. Table/column names are `SafeIdentifier`-validated (`[a-zA-Z_][a-zA-Z0-9_]*`). Route parameters use `{id:int}` constraints. Only raw SQL is static DDL. No parameterized queries (ADO.NET `@param`), so this is escaping-based rather than bind-variable-based, but the escaping is correct and consistently applied. See CS-SQL-1 (INFO). |
| CWE-20 | Improper Input Validation | **Partially Mitigated** | The ORM's `ValidateRequired()` enforces non-null constraints. `SqlFloat64.FormatTo()` was fixed to reject NaN/Infinity values (ORM-3, RESOLVED). However, no length validation on SQL string inputs — the ORM provides `ValidateLength()` but apps typically don't use it. SQLite imposes practical limits. |
| CWE-400 | Uncontrolled Resource Consumption | **Partially Mitigated** | SELECT queries use `ToSql()` without `LIMIT`. The ORM provides `SafeToSql(defaultLimit)` specifically for this, but the app uses `ToSql()` everywhere. A malicious user (or data growth) could cause `GetAllLists()` to return unbounded result sets. The `HasAnyLists()` method correctly uses `.Limit(1)`, showing awareness of the pattern. See CS-SQL-2 (INFO). |
| CWE-915 | Improperly Controlled Modification of Dynamically-Determined Object Attributes (Mass Assignment) | **Mitigated** | The ORM's `Cast(allowedFields)` method acts as a field whitelist. Only fields in the `ISafeIdentifier` list are included in the SQL. The app explicitly defines `ListInsertFields` and `TodoInsertFields` as static readonly lists. Handler methods accept only the specific parameters they need (`string name`, `int id`, `string title`) rather than binding to model objects. |
| CWE-943 | Improper Neutralization of Special Elements in Data Query Logic | **Mitigated** | This is the broader category encompassing SQL injection (CWE-89). The ORM's type-dispatch system (`SqlString`, `SqlInt32`, `SqlBoolean`, etc.) ensures each value type is formatted with its appropriate escaping strategy. The `SafeIdentifier` sealed interface ensures identifiers cannot contain SQL metacharacters. |

---

## Verdict

**No SQL injection vulnerabilities found.** This app has the highest ORM coverage of all 6 apps — all CRUD operations including toggle-completed use the ORM's type-safe SQL generation. The only raw SQL is static DDL and pragma statements. No user input ever reaches SQL via string concatenation.

---

## Consolidated Finding Summary

### Application-Level SQL Findings

| # | Severity | CWE | Status | Finding |
|---|----------|-----|--------|---------|
| CS-SQL-1 | INFO | CWE-89 | OPEN | ORM SQL executed as pre-rendered string. ADO.NET `SqliteParameter` would add defense-in-depth. |
| CS-SQL-2 | INFO | CWE-400 | OPEN | SELECT queries lack LIMIT. `SafeToSql(defaultLimit)` available but unused. |

### ORM-Level Findings (Resolved)

| # | Original | Current | CWE | Resolution |
|---|----------|---------|-----|------------|
| ORM-1 | MEDIUM | RESOLVED | CWE-89 | Column names routed through `SafeIdentifier` |
| ORM-2 | LOW | RESOLVED | CWE-89 | `SqlDate.FormatTo()` now escapes quotes |
| ORM-3 | LOW | RESOLVED | CWE-20 | `SqlFloat64.FormatTo()` renders NaN/Infinity as `NULL` |
| ORM-4 | INFO | ACKNOWLEDGED | -- | Design limitation: escaping-based, not parameterized |

### JOIN Feature Findings

| # | Severity | CWE | Status | Finding |
|---|----------|-----|--------|---------|
| JOIN-1 | INFO | -- | ACKNOWLEDGED | Table names validated by `SafeIdentifier` |
| JOIN-2 | INFO | -- | ACKNOWLEDGED | ON conditions use type-safe `SqlFragment` |
| JOIN-3 | INFO | -- | ACKNOWLEDGED | `JoinType` sealed with hardcoded keywords |
| JOIN-4 | INFO | CWE-400 | OPEN | JOINs amplify unbounded-query risk (same as CS-SQL-2) |
| JOIN-5 | INFO | -- | ACKNOWLEDGED | App currently uses N+1 queries; JOINs would maintain security |

### Totals

| Severity | Open | Resolved/Acknowledged | Total |
|----------|------|-----------------------|-------|
| MEDIUM | 0 | 1 | 1 |
| LOW | 0 | 1 | 1 |
| INFO | 2 | 7 | 9 |
| **Total** | **2** | **9** | **11** |
