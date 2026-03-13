# SQL Security Analysis: C# Todo App

SQL injection analysis of the C# todo app built on the Generic Temper ORM. This analysis focuses exclusively on SQL generation and execution — the core value proposition of the ORM.

**Analysis Date:** 2026-03-12
**Updated:** 2026-03-13 (MITRE ATT&CK / CWE Top 25 mapping, JOIN feature analysis)
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

---

## MITRE Top 25 CWE Analysis (2024)

Full mapping of the 2024 CWE Top 25 Most Dangerous Software Weaknesses against this application. Each CWE is assessed as **Vulnerable**, **Partially Mitigated**, **Mitigated**, or **N/A** (not applicable to this app's technology or feature set).

### Mapping Table

| Rank | CWE ID | Name | Status | Details |
|------|--------|------|--------|---------|
| 1 | CWE-787 | Out-of-bounds Write | **N/A** | C# is a memory-managed language. The CLR prevents buffer overflows, out-of-bounds writes, and stack corruption at the runtime level. Not applicable. |
| 2 | CWE-79 | Cross-site Scripting (XSS) | **Mitigated** | Razor's `@` syntax HTML-encodes all output by default. All user data (`@list.Name`, `@todo.Title`, `@Model.RequestId`) is rendered via standard Razor expressions. No `@Html.Raw()` or `HtmlString` usage anywhere. One minor concern: the `onclick="return confirm('Delete list &quot;@list.Name&quot;...')"` in `Index.cshtml` inserts `@list.Name` inside a JavaScript string within an HTML attribute. Razor HTML-encodes this, which prevents HTML injection, but a carefully crafted name could still break the JavaScript context (e.g., a name containing `'` would break the JS string literal). In practice, this is low-risk because the confirm dialog is decorative and the form still requires POST submission, but it represents an incomplete encoding context. |
| 3 | CWE-89 | SQL Injection | **Mitigated** | All CRUD operations use the ORM's type-safe SQL generation. User strings go through `SqlString` (`'` -> `''`). Integer IDs go through `SqlBuilder.AppendInt32()`. Table/column names are `SafeIdentifier`-validated (`[a-zA-Z_][a-zA-Z0-9_]*`). Route parameters use `{id:int}` constraints. Only raw SQL is static DDL. No parameterized queries (ADO.NET `@param`), so this is escaping-based rather than bind-variable-based, but the escaping is correct and consistently applied. See CS-SQL-1 (INFO). |
| 4 | CWE-416 | Use After Free | **N/A** | C# is garbage-collected. Use-after-free is not possible in managed code. |
| 5 | CWE-78 | OS Command Injection | **N/A** | The app does not execute any OS commands. No `Process.Start()`, `System.Diagnostics.Process`, or shell invocations. |
| 6 | CWE-20 | Improper Input Validation | **Partially Mitigated** | The app validates that `name`/`title` are not null/whitespace before database operations (`string.IsNullOrWhiteSpace` checks in `IndexModel` and `ListModel`). The ORM's `ValidateRequired()` adds a second layer. However, there is no length validation on user inputs -- names and titles have no maximum length constraint at the application level, though SQLite imposes a practical limit. The ORM provides `ValidateLength()` but it is not used. |
| 7 | CWE-125 | Out-of-bounds Read | **N/A** | C# is memory-managed. Array bounds are checked at runtime by the CLR. |
| 8 | CWE-22 | Path Traversal | **N/A** | The app performs no file system operations. No file uploads, downloads, or file path construction from user input. Static files are served via ASP.NET's `MapStaticAssets()` which has its own path traversal protections. |
| 9 | CWE-352 | Cross-Site Request Forgery (CSRF) | **Mitigated** | ASP.NET Core Razor Pages automatically generates and validates antiforgery tokens for all POST form submissions via the `asp-page-handler` tag helper. The framework injects `__RequestVerificationToken` hidden fields and validates them server-side. The only page with `[IgnoreAntiforgeryToken]` is `Error.cshtml.cs`, which only handles GET requests and displays no sensitive data. All mutating operations (Create, Update, Delete, Toggle) are POST-only and protected. |
| 10 | CWE-434 | Unrestricted Upload of File with Dangerous Type | **N/A** | The app has no file upload functionality. |
| 11 | CWE-862 | Missing Authorization | **Vulnerable** | The app has no authentication or authorization. All data is accessible to any user who can reach the server. `Program.cs` calls `app.UseAuthorization()` but no authorization policies, `[Authorize]` attributes, or authentication middleware are configured. Any user can create, read, update, or delete any list or todo. For a local single-user todo app this is expected behavior, but it would be a critical vulnerability in any multi-user or internet-facing deployment. |
| 12 | CWE-476 | NULL Pointer Dereference | **Mitigated** | C# nullable reference types are enabled (`<Nullable>enable</Nullable>` in `.csproj`). `GetList()` returns `TodoList?` and the Razor template checks `@if (Model.TodoList == null)` before accessing properties. `OnPostAddTodo` checks `if (list == null)` before using the list. The app handles null returns appropriately. |
| 13 | CWE-287 | Improper Authentication | **Vulnerable** | No authentication mechanism exists. No login, no session management, no identity provider integration. Same rationale as CWE-862: acceptable for a local development tool but a vulnerability in production. |
| 14 | CWE-190 | Integer Overflow or Wraparound | **N/A** | C# integers are bounds-checked in checked contexts. The app uses `int` (Int32) for IDs and counts, which is within SQLite's `INTEGER` range. ASP.NET's `{id:int}` route constraint rejects values that don't parse as Int32. |
| 15 | CWE-502 | Deserialization of Untrusted Data | **N/A** | The app does not deserialize any data. No JSON deserialization of user input, no `BinaryFormatter`, no custom deserialization. Form values are accessed as primitive strings/ints via model binding. |
| 16 | CWE-77 | Command Injection | **N/A** | No command execution of any kind. See CWE-78. |
| 17 | CWE-119 | Improper Restriction of Operations within the Bounds of a Memory Buffer | **N/A** | Managed language. Not applicable to C#/.NET. |
| 18 | CWE-798 | Use of Hard-coded Credentials | **N/A** | No credentials in the codebase. The SQLite database (`Data Source=todos.db`) requires no credentials. `appsettings.json` contains only logging configuration and `AllowedHosts: "*"`. |
| 19 | CWE-918 | Server-Side Request Forgery (SSRF) | **N/A** | The app makes no outbound HTTP requests. No `HttpClient`, `WebRequest`, or URL-fetching code. |
| 20 | CWE-306 | Missing Authentication for Critical Function | **Vulnerable** | All CRUD operations (create/update/delete lists and todos) are accessible without authentication. Identical root cause to CWE-862 and CWE-287. |
| 21 | CWE-362 | Concurrent Execution Using Shared Resource with Improper Synchronization | **Partially Mitigated** | `TodoDb` is registered as a Singleton (`builder.Services.AddSingleton`), but each method opens its own `SqliteConnection`, so connections are not shared across threads. SQLite itself serializes writes via its internal locking. However, read-then-write sequences (like `GetList` then `InsertTodo` in `OnPostAddTodo`) are not atomic -- a TOCTOU race is theoretically possible, though the impact is limited to orphaned todos if a list is deleted between the check and the insert. |
| 22 | CWE-269 | Improper Privilege Management | **N/A** | The app has no privilege/role system. There is no concept of admin vs. regular user. |
| 23 | CWE-94 | Code Injection | **N/A** | The app does not evaluate any user-supplied code. No `eval()`, no dynamic compilation, no expression parsing. |
| 24 | CWE-863 | Incorrect Authorization | **N/A (see CWE-862)** | Authorization is entirely absent rather than incorrectly implemented. The root issue is CWE-862 (missing), not CWE-863 (incorrect). |
| 25 | CWE-276 | Incorrect Default Permissions | **N/A** | The app does not create files or set permissions. The SQLite database file is created with OS-default permissions. |

### Additional Relevant CWEs (Not in Top 25)

| CWE ID | Name | Status | Details |
|--------|------|--------|---------|
| CWE-400 | Uncontrolled Resource Consumption | **Partially Mitigated** | SELECT queries use `ToSql()` without `LIMIT`. The ORM provides `SafeToSql(defaultLimit)` specifically for this, but the app uses `ToSql()` everywhere. A malicious user (or data growth) could cause `GetAllLists()` to return unbounded result sets. The `HasAnyLists()` method correctly uses `.Limit(1)`, showing awareness of the pattern. See CS-SQL-2 (INFO). |
| CWE-915 | Improperly Controlled Modification of Dynamically-Determined Object Attributes (Mass Assignment) | **Mitigated** | The ORM's `Cast(allowedFields)` method acts as a field whitelist. Only fields in the `ISafeIdentifier` list are included in the SQL. The app explicitly defines `ListInsertFields` and `TodoInsertFields` as static readonly lists. Handler methods accept only the specific parameters they need (`string name`, `int id`, `string title`) rather than binding to model objects. |
| CWE-943 | Improper Neutralization of Special Elements in Data Query Logic | **Mitigated** | This is the broader category encompassing SQL injection (CWE-89). The ORM's type-dispatch system (`SqlString`, `SqlInt32`, `SqlBoolean`, etc.) ensures each value type is formatted with its appropriate escaping strategy. The `SafeIdentifier` sealed interface ensures identifiers cannot contain SQL metacharacters. |
| CWE-532 | Insertion of Sensitive Information into Log File | **Partially Mitigated** | In Development mode, `DetailedErrors: true` is set in `appsettings.Development.json`. The Error page displays `RequestId` (from `Activity.Current?.Id ?? HttpContext.TraceIdentifier`). While this is standard ASP.NET behavior and only active in Development, it could leak internal identifiers if accidentally deployed with Development configuration. |
| CWE-1021 | Improper Restriction of Rendered UI Layers (Clickjacking) | **Vulnerable** | The app sets no `X-Frame-Options` or `Content-Security-Policy: frame-ancestors` headers. It could be framed by a malicious site for clickjacking attacks on the POST forms. |

### Summary by Status

| Status | Count | CWE IDs |
|--------|-------|---------|
| **Vulnerable** | 3 | CWE-862, CWE-287, CWE-306 (all same root cause: no auth) |
| **Partially Mitigated** | 4 | CWE-20, CWE-362, CWE-400, CWE-532 |
| **Mitigated** | 7 | CWE-79, CWE-89, CWE-352, CWE-476, CWE-915, CWE-943, CWE-1021 noted separately |
| **N/A** | 16 | CWE-787, CWE-416, CWE-78, CWE-125, CWE-22, CWE-434, CWE-190, CWE-502, CWE-77, CWE-119, CWE-798, CWE-918, CWE-306 (counted above), CWE-269, CWE-94, CWE-276 |

### Risk Assessment

The application's primary security concern is the complete absence of authentication and authorization (CWE-862/287/306). This is a shared root cause -- a single mitigation (adding ASP.NET Core Identity or an external auth provider) would resolve all three. For a local development tool, this is acceptable. For any deployment beyond localhost, it is critical.

The SQL injection surface (CWE-89) is well-defended by the ORM's type-safe generation pipeline. The escaping-based approach (vs. parameterized queries) is an acknowledged design limitation (ORM-4) but is correctly implemented.

XSS (CWE-79) is comprehensively mitigated by Razor's default HTML encoding, with one minor edge case in the JavaScript `confirm()` dialog that warrants attention in a hardening pass.

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

## Consolidated Finding Summary

### Application-Level Findings

| # | Severity | CWE | Status | Finding |
|---|----------|-----|--------|---------|
| CS-SQL-1 | INFO | CWE-89 | OPEN | ORM SQL executed as pre-rendered string. ADO.NET `SqliteParameter` would add defense-in-depth. |
| CS-SQL-2 | INFO | CWE-400 | OPEN | SELECT queries lack LIMIT. `SafeToSql(defaultLimit)` available but unused. |
| CS-XSS-1 | LOW | CWE-79 | OPEN | `@list.Name` in `onclick="confirm(...)"` JavaScript context: Razor HTML-encodes but does not JS-encode. |
| CS-AUTH-1 | HIGH | CWE-862 | OPEN | No authentication or authorization. All operations accessible to any user. |
| CS-RACE-1 | LOW | CWE-362 | OPEN | Read-then-write in `OnPostAddTodo` (check list exists, then insert todo) is not atomic. |
| CS-CLICK-1 | LOW | CWE-1021 | OPEN | No `X-Frame-Options` or CSP `frame-ancestors` header. |
| CS-INPUT-1 | LOW | CWE-20 | OPEN | No length validation on user-supplied `name`/`title` strings. |

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
| HIGH | 1 | 0 | 1 |
| MEDIUM | 0 | 1 | 1 |
| LOW | 4 | 1 | 5 |
| INFO | 2 | 7 | 9 |
| **Total** | **7** | **9** | **16** |
