using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Pages;

/// <summary>
/// Search page demonstrating ORM WHERE variants:
/// WhereLike, WhereILike, WhereNull, WhereNotNull, WhereBetween,
/// WhereIn, WhereInSubquery, WhereNot, OrWhere,
/// plus Limit, Offset, SafeToSql, Distinct
/// </summary>
public class SearchModel : PageModel
{
    private readonly TodoDb _db;

    public SearchModel(TodoDb db)
    {
        _db = db;
    }

    [BindProperty(SupportsGet = true)]
    public string? Query { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? SearchType { get; set; } = "like";

    [BindProperty(SupportsGet = true)]
    public string? DateStart { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? DateEnd { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? ListPattern { get; set; }

    public List<TodoItem> Results { get; set; } = new();
    public string SearchDescription { get; set; } = string.Empty;

    public void OnGet()
    {
        if (string.IsNullOrEmpty(SearchType)) SearchType = "like";

        switch (SearchType)
        {
            case "like":
                // ORM: From, WhereLike, OrderBy, Limit, Offset, SafeToSql
                if (!string.IsNullOrEmpty(Query))
                {
                    Results = _db.SearchTodos(Query);
                    SearchDescription = $"WhereLike: Searching for \"{Query}\" (From + WhereLike + Limit + Offset)";
                }
                break;

            case "daterange":
                // ORM: From, WhereNotNull, WhereBetween, OrderBy
                if (!string.IsNullOrEmpty(DateStart) && !string.IsNullOrEmpty(DateEnd))
                {
                    Results = _db.GetTodosByDateRange(DateStart, DateEnd);
                    SearchDescription = $"WhereBetween: Due dates between {DateStart} and {DateEnd} (WhereNotNull + WhereBetween)";
                }
                break;

            case "overdue":
                // ORM: From, WhereNotNull, Where (custom), WhereNot
                Results = _db.GetOverdueTodos();
                SearchDescription = "Overdue: WhereNotNull(due_date) + Where(due_date < today) + Where(completed = 0)";
                break;

            case "nodate":
                // ORM: From, WhereNull
                Results = _db.GetTodosWithoutDueDate();
                SearchDescription = "WhereNull: Todos without a due date (WhereNull)";
                break;

            case "highpri":
                // ORM: From, WhereIn (ISqlPart list)
                Results = _db.GetHighPriorityTodos();
                SearchDescription = "WhereIn: Priority IN (1, 2) - high priority pending todos";
                break;

            case "subquery":
                // ORM: From, WhereInSubquery, Select, WhereLike
                if (!string.IsNullOrEmpty(ListPattern))
                {
                    Results = _db.GetTodosInListsLike(ListPattern);
                    SearchDescription = $"WhereInSubquery: Todos in lists matching \"{ListPattern}\" (WhereInSubquery + Select + WhereLike)";
                }
                break;

            case "criticalorcomplete":
                // ORM: From, Where, OrWhere
                Results = _db.GetCriticalOrCompleted();
                SearchDescription = "OrWhere: Priority = 1 OR Completed = 1 (Where + OrWhere)";
                break;
        }
    }
}
