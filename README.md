# Alloy Todo App -- C#

A fully-functional todo list manager demonstrating **every** Alloy ORM feature. Built with ASP.NET Core Razor Pages + Microsoft.Data.Sqlite, styled with a retro Mac System 6 / Windows 95 hybrid UI.

## Overview

This application exercises the complete surface area of the Alloy ORM -- a compile-to-many-languages SQL query builder and changeset validation system written in Temper. All database operations flow through the ORM via `TodoDb.cs`, and the SQL Viewer page generates and displays the SQL for every single ORM feature (60+ demos).

6 Razor Pages and 1 data access class cover: query building, all 5 join types, all 6 aggregate functions, 4 set operations, subqueries, changeset validation with all 14 validators, pagination, locking, safe identifiers, 7 search modes exercising every WHERE variant, and more.

## Data Model

Four-table schema with foreign key relationships:

```
lists (id, name, description, created_at)
  |
  +--< todos (id, title, completed, priority 1-5, due_date, list_id FK, created_at)
         |
         +--< todo_tags (id, todo_id FK, tag_id FK)
                                           |
                                     tags (id, name) >--+
```

- **lists** -- containers for grouping todos
- **todos** -- individual tasks with priority (1=critical, 5=minimal) and optional due date
- **tags** -- labels (urgent, home, office, health, errand) that can be assigned to any todo
- **todo_tags** -- join table linking todos to tags (many-to-many)

## Complete ORM Feature Coverage

Every ORM export is exercised by at least one page or method in `TodoDb.cs`:

| ORM Feature | Category | Page/Method That Exercises It |
|---|---|---|
| `From` | Query Builder | All data methods |
| `Update` | Query Builder | `ToggleTodo`, `CompleteAllInList`, SQL Viewer |
| `DeleteFrom` | Query Builder | `DeleteTodo`, `DeleteCompletedInList`, `RemoveTag` |
| `Where` | WHERE | `GetList`, `GetTodo`, `GetOverdueTodos`, all CRUD |
| `OrWhere` | WHERE | `GetCriticalOrCompleted`, Search (criticalorcomplete mode), SQL Viewer |
| `WhereNot` | WHERE | SQL Viewer (WhereNot demo) |
| `WhereNull` | WHERE | `GetTodosWithoutDueDate`, Search (nodate mode) |
| `WhereNotNull` | WHERE | `GetTodosByDateRange`, `GetOverdueTodos`, Search (daterange mode) |
| `WhereBetween` | WHERE | `GetTodosByDateRange`, Search (daterange mode), SQL Viewer |
| `WhereLike` | WHERE | `SearchTodos`, `GetTodosInListsLike`, Search (like mode) |
| `WhereILike` | WHERE | SQL Viewer (WhereILike demo) |
| `WhereIn` | WHERE | `GetHighPriorityTodos`, Search (highpri mode), SQL Viewer |
| `WhereInSubquery` | WHERE | `GetTodosInListsLike`, Search (subquery mode), SQL Viewer |
| `Select` | SELECT | `ToggleTodo`, `GetListName`, `GetTodosInListsLike` |
| `SelectExpr` | SELECT | `GetDashboardStats` (aggregates), `GetListSummaries`, `GetPriorityBreakdowns`, `GetTagSummaries`, SQL Viewer |
| `Distinct` | SELECT | SQL Viewer (Distinct demo) |
| `Col` | Column Ref | `GetTagsForTodo`, `GetTagSummaries`, SQL Viewer |
| `CountAll` | Aggregate | `GetDashboardStats`, `GetListSummaries`, `GetPriorityBreakdowns`, SQL Viewer |
| `CountCol` | Aggregate | `GetTagSummaries`, SQL Viewer |
| `SumCol` | Aggregate | `GetListSummaries`, SQL Viewer |
| `AvgCol` | Aggregate | `GetDashboardStats`, `GetListSummaries`, SQL Viewer |
| `MinCol` | Aggregate | `GetDashboardStats`, SQL Viewer |
| `MaxCol` | Aggregate | `GetDashboardStats`, SQL Viewer |
| `CountSql` | Counting | `GetDashboardStats` (lists, todos, completed), `GetTagTodoCount`, SQL Viewer |
| `GroupBy` | Grouping | `GetListSummaries`, `GetPriorityBreakdowns`, `GetTagSummaries`, SQL Viewer |
| `Having` | Grouping | `GetPriorityBreakdowns`, SQL Viewer |
| `OrHaving` | Grouping | SQL Viewer (OrHaving demo) |
| `OrderBy` | Ordering | `GetAllLists`, `GetTodosForList`, `SearchTodos`, `GetAllTags` |
| `OrderByNulls` | Ordering | `GetTodosForList` (NullsLast), SQL Viewer (NullsFirst, NullsLast) |
| `NullsFirst` | Ordering | SQL Viewer |
| `NullsLast` | Ordering | `GetTodosForList` |
| `Limit` | Pagination | `SearchTodos`, `HasAnyLists`, SQL Viewer |
| `Offset` | Pagination | `SearchTodos`, SQL Viewer |
| `SafeToSql` | Pagination | SQL Viewer (SafeToSql demo) |
| `Lock` (ForUpdate) | Locking | SQL Viewer |
| `Lock` (ForShare) | Locking | SQL Viewer |
| `InnerJoin` | Joins | `GetTagsForTodo`, SQL Viewer |
| `LeftJoin` | Joins | `GetTagSummaries`, SQL Viewer |
| `RightJoin` | Joins | SQL Viewer |
| `FullJoin` | Joins | SQL Viewer |
| `CrossJoin` | Joins | SQL Viewer |
| `UnionSql` | Set Operations | SQL Viewer |
| `UnionAllSql` | Set Operations | SQL Viewer |
| `IntersectSql` | Set Operations | SQL Viewer |
| `ExceptSql` | Set Operations | SQL Viewer |
| `Subquery` | Subqueries | SQL Viewer |
| `ExistsSql` | Subqueries | SQL Viewer |
| `Changeset` | Changeset | `InsertList`, `UpdateList`, `InsertTodo`, `UpdateTodo`, `CreateTag`, `AssignTag` |
| `Cast` | Changeset | All changeset methods |
| `ValidateRequired` | Validation | `InsertList`, `InsertTodo`, `CreateTag`, `AssignTag` |
| `ValidateLength` | Validation | `InsertList`, `UpdateList`, `InsertTodo`, `CreateTag` |
| `ValidateInt` | Validation | `InsertTodo`, `UpdateTodo`, `AssignTag`, SQL Viewer |
| `ValidateInt64` | Validation | SQL Viewer |
| `ValidateFloat` | Validation | SQL Viewer |
| `ValidateBool` | Validation | SQL Viewer |
| `ValidateNumber` | Validation | `InsertTodo`, `UpdateTodo`, SQL Viewer |
| `ValidateInclusion` | Validation | `InsertTodo`, SQL Viewer |
| `ValidateExclusion` | Validation | `CreateTag` (reserved names), SQL Viewer |
| `ValidateAcceptance` | Validation | SQL Viewer |
| `ValidateConfirmation` | Validation | SQL Viewer |
| `ValidateContains` | Validation | SQL Viewer |
| `ValidateStartsWith` | Validation | SQL Viewer |
| `ValidateEndsWith` | Validation | SQL Viewer |
| `ToInsertSql` | Changeset SQL | `InsertList`, `InsertTodo`, `CreateTag`, `AssignTag`, SQL Viewer |
| `ToUpdateSql` | Changeset SQL | `UpdateList`, `UpdateTodo`, SQL Viewer |
| `PutChange` | Changeset Mutation | `UpdateList`, SQL Viewer |
| `GetChange` | Changeset Mutation | `UpdateList`, SQL Viewer |
| `DeleteChange` | Changeset Mutation | SQL Viewer |
| `DeleteSql` | Delete Helper | `DeleteList`, `DeleteTag`, SQL Viewer |
| `SafeIdentifier` | Types | All methods (table/field names) |
| `TableDef` | Types | Schema definitions, SQL Viewer |
| `FieldDef` | Types | Schema definitions |
| `StringField` | Types | Schema definitions |
| `IntField` | Types | Schema definitions |
| `SqlBuilder` | Types | `WhereEq`, `WhereEqStr`, `WhereGt`, `WhereLt`, `BuildJoinCondition`, `BuildSafeFragment` |
| `SqlFragment` | Types | All query methods |
| `SqlInt32` | Types | `ToggleTodo`, `CompleteAllInList`, SQL Viewer |
| `SqlString` | Types | `GetTodosByDateRange`, SQL Viewer |
| `NumberValidationOpts` | Types | `InsertTodo`, `UpdateTodo` |
| `ForUpdate` | Types | SQL Viewer |
| `ForShare` | Types | SQL Viewer |

## Page Reference

| Page | URL | Purpose | Key ORM Features |
|---|---|---|---|
| **Index** | `/` | All lists with CRUD (create, edit, delete) | `From`, `OrderBy`, `Where`, `Changeset`, `Cast`, `ValidateRequired`, `ValidateLength`, `ToInsertSql`, `ToUpdateSql`, `PutChange`, `GetChange`, `DeleteSql` |
| **List** | `/List?id=N` | Single list view with todos, tags, bulk actions | `From`, `Where`, `OrderBy`, `OrderByNulls` (NullsLast), `Select`, `Update`, `Set`, `DeleteFrom`, `Changeset`, `ValidateInt`, `ValidateNumber`, `ValidateInclusion`, `InnerJoin`, `Col`, `CountSql`, `ToInsertSql`, `ToUpdateSql` |
| **Search** | `/Search` | 7 search modes exercising WHERE variants | `WhereLike`, `WhereNotNull`, `WhereBetween`, `WhereNull`, `WhereIn`, `WhereInSubquery`, `OrWhere`, `Limit`, `Offset`, `SafeToSql`, `Select` |
| **Dashboard** | `/Dashboard` | Stats with aggregates and breakdowns | `CountSql`, `CountAll`, `CountCol`, `SumCol`, `AvgCol`, `MinCol`, `MaxCol`, `SelectExpr`, `GroupBy`, `Having`, `LeftJoin`, `Col`, `WhereNotNull`, `WhereNull`, `WhereIn` |
| **Tags** | `/Tags` | Tag management with validation | `From`, `OrderBy`, `CountSql`, `Changeset`, `ValidateRequired`, `ValidateLength`, `ValidateExclusion`, `ToInsertSql`, `DeleteSql`, `InnerJoin`, `Col` |
| **SQL Viewer** | `/SqlViewer` | 60+ generated SQL demos for every ORM feature | Every single ORM feature (see `GenerateAllSqlDemos()` in `TodoDb.cs`) |

### Search Page -- 7 Modes

The Search page is a dedicated showcase for WHERE clause variants:

| Mode | ORM Features Used |
|---|---|
| **Title Search** (like) | `From`, `WhereLike`, `OrderBy`, `Limit`, `Offset` |
| **Date Range** (daterange) | `From`, `WhereNotNull`, `WhereBetween` (with SqlString), `OrderBy` |
| **Overdue** (overdue) | `From`, `WhereNotNull`, `Where` (custom SqlBuilder condition), `WhereNot` |
| **No Due Date** (nodate) | `From`, `WhereNull`, `OrderBy` |
| **High Priority** (highpri) | `From`, `WhereIn` (ISqlPart list), `Where`, `OrderBy` |
| **Subquery** (subquery) | `From`, `WhereInSubquery`, `Select`, `WhereLike` |
| **Critical or Complete** (criticalorcomplete) | `From`, `Where`, `OrWhere`, `OrderBy` |

## Code Examples

### Changeset with Validation (Insert Todo)

```csharp
var cs = SrcGlobal.Changeset(TodoTableDef, values)
    .Cast(Listed.CreateReadOnlyList<ISafeIdentifier>(
        TitleField, CompletedField, PriorityField, ListIdField, DueDateField, CreatedAtField))
    .ValidateRequired(TodoInsertFields)
    .ValidateLength(TitleField, 1, 500)
    .ValidateInt(CompletedField)
    .ValidateInt(PriorityField)
    .ValidateInt(ListIdField)
    .ValidateNumber(PriorityField,
        new NumberValidationOpts(null, null, 1.0, 5.0, null))
    .ValidateInclusion(PriorityField,
        Listed.CreateReadOnlyList<string>("1", "2", "3", "4", "5"));

if (!cs.IsValid)
    return (false, cs.Errors.Select(e => $"{e.Field}: {e.Message}").ToList());

string sql = cs.ToInsertSql().ToString();
cmd.CommandText = sql;
cmd.ExecuteNonQuery();
```

### INNER JOIN with Col for Qualified Column References (Get Tags for Todo)

```csharp
var joinCond = SrcGlobal.Col(TagsTable, IdField);
var joinRight = SrcGlobal.Col(TodoTagsTable, TagIdField);

var joinCondBuilder = new SqlBuilder();
joinCondBuilder.AppendFragment(joinCond);
joinCondBuilder.AppendSafe(" = ");
joinCondBuilder.AppendFragment(joinRight);

var query = SrcGlobal.From(TagsTable)
    .InnerJoin(TodoTagsTable, joinCondBuilder.Accumulated)
    .Where(whereFrag)
    .OrderBy(NameField, true);
string sql = query.ToSql().ToString();
```

### Dashboard with All Aggregate Functions

```csharp
// Average priority
var avgQuery = SrcGlobal.From(TodosTable)
    .SelectExpr(Listed.CreateReadOnlyList<SqlFragment>(
        SrcGlobal.AvgCol(PriorityField)));
stats.AvgPriority = ExecuteDouble(conn, avgQuery.ToSql().ToString());

// Per-list breakdown with GroupBy + Having
var query = SrcGlobal.From(TodosTable)
    .SelectExpr(Listed.CreateReadOnlyList<SqlFragment>(
        BuildSafeFragment(PriorityField.SqlValue),
        SrcGlobal.CountAll()))
    .GroupBy(PriorityField)
    .Having(havingFrag)
    .OrderBy(PriorityField, true);
```

### Update Query Builder (Toggle Todo)

```csharp
var updateQuery = SrcGlobal.Update(TodosTable)
    .Set(CompletedField, new SqlInt32(newVal))
    .Where(WhereEq(IdField, todoId));
string updateSql = updateQuery.ToSql().ToString();
cmd.CommandText = updateSql;
cmd.ExecuteNonQuery();
```

## Security Model

The Alloy ORM provides five defense layers against SQL injection:

1. **SafeIdentifier** -- all table and column names are validated at construction via `SrcGlobal.SafeIdentifier()`; only `[a-zA-Z_][a-zA-Z0-9_]*` pass
2. **SqlBuilder** -- typed append methods (`AppendInt32`, `AppendString`, `AppendSafe`) with automatic escaping for string literals (single-quote doubling)
3. **Changeset** -- `Cast()` whitelists only permitted fields; user input never becomes raw SQL
4. **No string interpolation** -- the ORM API makes it structurally impossible to concatenate user input into SQL
5. **Type-safe values** -- `SqlInt32`, `SqlString`, `SqlFloat64` etc. enforce type-correct literal emission

For full details, see [SECURITY_ANALYSIS.md](SECURITY_ANALYSIS.md) and the main repo's [MITRE CWE analysis](https://github.com/notactuallytreyanastasio/alloy).

## Running the App

### Prerequisites

- .NET 10.0 SDK

### Build and Run

```bash
cd TodoApp
dotnet run
```

The app starts at **http://localhost:5002**.

### Dependencies

| Package | Purpose |
|---|---|
| ASP.NET Core (Razor Pages) | Web framework and HTML templating |
| `Microsoft.Data.Sqlite` 10.0.3 | SQLite database driver |
| `Orm` (vendored project reference) | Alloy ORM -- compiled Temper-to-C# |
| `TemperLang.Core` (vendored) | Temper runtime (Map, Listed, Mapped types) |
| `TemperLang.Std` (vendored) | Temper standard library |

### Project Structure

```
TodoApp/
  Program.cs              -- Entry point, DI, pipeline config
  Data/
    TodoDb.cs             -- Data access layer (ALL ORM calls live here)
  Models/
    TodoList.cs           -- List model
    TodoItem.cs           -- Todo model
    Tag.cs                -- Tag model
    TodoTag.cs            -- Join table model
    DashboardStats.cs     -- Stats, summaries, SqlDemo models
  Pages/
    Index.cshtml(.cs)     -- Lists CRUD
    List.cshtml(.cs)      -- Single list with todos, tags, bulk actions
    Search.cshtml(.cs)    -- 7 search modes for WHERE variants
    Dashboard.cshtml(.cs) -- Stats with all aggregates
    Tags.cshtml(.cs)      -- Tag management
    SqlViewer.cshtml(.cs) -- 60+ SQL generation demos
    Shared/
      _Layout.cshtml      -- Retro UI layout
  vendor/
    orm/                  -- Alloy ORM compiled to C#
    std/                  -- Temper standard library
    temper-core/          -- Temper core runtime
  wwwroot/css/
    retro.css             -- Mac System 6 / Win95 hybrid styles
```

## Links

- **Main Alloy ORM repo**: https://github.com/notactuallytreyanastasio/alloy
- **Compiled C# library**: https://github.com/notactuallytreyanastasio/alloy-csharp
