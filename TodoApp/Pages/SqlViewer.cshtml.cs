using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Pages;

/// <summary>
/// SQL Viewer page - shows ALL generated SQL from every ORM feature.
/// This page is the comprehensive showcase demonstrating:
/// - Query builders: From, Update, DeleteFrom
/// - Where variants: Where, OrWhere, WhereNull, WhereNotNull, WhereIn,
///   WhereInSubquery, WhereNot, WhereBetween, WhereLike, WhereILike
/// - Select: Select, SelectExpr, Distinct
/// - Joins: InnerJoin, LeftJoin, RightJoin, FullJoin, CrossJoin
/// - Ordering: OrderBy, OrderByNulls (NullsFirst, NullsLast)
/// - Pagination: Limit, Offset, SafeToSql
/// - Grouping: GroupBy, Having, OrHaving
/// - Locking: Lock (ForUpdate, ForShare)
/// - Counting: CountSql, CountAll, CountCol
/// - Aggregates: SumCol, AvgCol, MinCol, MaxCol
/// - Set operations: UnionSql, UnionAllSql, IntersectSql, ExceptSql
/// - Subqueries: Subquery, ExistsSql
/// - Column references: Col
/// - Delete helpers: DeleteSql
/// - Update builder: Set, Where, Limit
/// - Changeset: Cast, ValidateRequired, ValidateLength, ValidateInt,
///   ValidateInt64, ValidateFloat, ValidateBool, ValidateInclusion,
///   ValidateExclusion, ValidateNumber, ValidateAcceptance,
///   ValidateConfirmation, ValidateContains, ValidateStartsWith,
///   ValidateEndsWith, PutChange, GetChange, DeleteChange,
///   ToInsertSql, ToUpdateSql
/// - Types: SafeIdentifier, TableDef, FieldDef, StringField, IntField,
///   Int64Field, FloatField, BoolField, DateField, SqlBuilder,
///   SqlFragment, SqlInt32, SqlString, NumberValidationOpts,
///   NullsFirst, NullsLast, ForUpdate, ForShare
/// </summary>
public class SqlViewerModel : PageModel
{
    private readonly TodoDb _db;

    public SqlViewerModel(TodoDb db)
    {
        _db = db;
    }

    public List<SqlDemo> Demos { get; set; } = new();

    public void OnGet()
    {
        Demos = _db.GenerateAllSqlDemos();
    }
}
