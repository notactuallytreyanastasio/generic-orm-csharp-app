# Generic ORM — C# Demo App

A todo-list application built with ASP.NET Core Razor Pages and SQLite, demonstrating the [Generic ORM](https://github.com/notactuallytreyanastasio/generic_orm) compiled from Temper to C#.

## Stack

| Component | Technology |
|-----------|-----------|
| Framework | ASP.NET Core (Razor Pages) |
| Templates | Razor Pages |
| Database | SQLite via Microsoft.Data.Sqlite |
| ORM | [Generic ORM](https://github.com/notactuallytreyanastasio/generic_orm) (vendored) |
| Port | 5002 |

## ORM Usage

```csharp
using Orm.Src;

// SELECT — type-safe query builder
var q = SrcGlobal.From(SrcGlobal.SafeIdentifier("todos"))
    .Where(whereFragment)
    .OrderBy(SrcGlobal.SafeIdentifier("created_at"), true)
    .ToSql().ToString();

// INSERT — changeset pipeline with field whitelisting
var cs = SrcGlobal.Changeset(tableDef, parms)
    .Cast(new[] { SrcGlobal.SafeIdentifier("title") })
    .ValidateRequired(new[] { SrcGlobal.SafeIdentifier("title") });
var sql = cs.ToInsertSql().ToString();

// DELETE — validated identifier
var sql = SrcGlobal.DeleteSql(tableDef, id).ToString();
```

## Security Model

- **Zero SQL injection vulnerabilities** — all queries generated through the ORM
- `SrcGlobal.SafeIdentifier()` validates table/column names against `[a-zA-Z_][a-zA-Z0-9_]*`
- Sealed `SqlPart` hierarchy handles per-type value escaping
- `Changeset.Cast()` enforces field whitelisting (CWE-915 prevention)
- .NET 10.0 with nullable reference types enabled
- DDL (`CREATE TABLE`) is the only raw SQL — static strings with no user input

## Running

```bash
cd TodoApp
dotnet run
# Open http://localhost:5002
```

## Vendored ORM

The `TodoApp/vendor/` directory contains the ORM compiled from Temper to .NET source files. Referenced as a project dependency in the .csproj. Updated automatically by CI when the ORM source changes.

---

Part of the [Generic ORM](https://github.com/notactuallytreyanastasio/generic_orm) project — write once in Temper, secure everywhere.
