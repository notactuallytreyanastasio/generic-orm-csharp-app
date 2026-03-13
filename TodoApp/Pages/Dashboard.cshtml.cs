using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Pages;

/// <summary>
/// Dashboard demonstrating ORM aggregate functions:
/// CountAll, CountCol, SumCol, AvgCol, MinCol, MaxCol,
/// GroupBy, Having, SelectExpr, LeftJoin, Col, CountSql
/// </summary>
public class DashboardModel : PageModel
{
    private readonly TodoDb _db;

    public DashboardModel(TodoDb db)
    {
        _db = db;
    }

    public DashboardStats Stats { get; set; } = new();
    public List<TodoItem> OverdueTodos { get; set; } = new();
    public List<TodoItem> HighPriorityTodos { get; set; } = new();
    public List<TodoItem> NoDueDateTodos { get; set; } = new();

    public void OnGet()
    {
        // ORM: CountSql, SelectExpr, CountAll, SumCol, AvgCol, MinCol, MaxCol,
        //      GroupBy, Having, LeftJoin, Col, CountCol
        Stats = _db.GetDashboardStats();

        // ORM: From, WhereNotNull, Where (custom SqlBuilder condition), OrderBy
        OverdueTodos = _db.GetOverdueTodos();

        // ORM: From, WhereIn (ISqlPart list), Where, OrderBy
        HighPriorityTodos = _db.GetHighPriorityTodos();

        // ORM: From, WhereNull, OrderBy
        NoDueDateTodos = _db.GetTodosWithoutDueDate();
    }
}
