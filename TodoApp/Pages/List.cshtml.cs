using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Pages;

public class ListModel : PageModel
{
    private readonly TodoDb _db;

    public ListModel(TodoDb db)
    {
        _db = db;
    }

    public TodoList? TodoList { get; set; }
    public int? EditingTodoId { get; set; }
    public List<Tag> AllTags { get; set; } = new();
    public List<string> Errors { get; set; } = new();

    public void OnGet(int id, int? editTodoId)
    {
        TodoList = _db.GetList(id);
        EditingTodoId = editTodoId;
        AllTags = _db.GetAllTags();
    }

    /// <summary>
    /// Add a new todo.
    /// ORM: Changeset, Cast, ValidateRequired, ValidateLength, ValidateInt,
    ///      ValidateNumber, ValidateInclusion, NumberValidationOpts, ToInsertSql
    /// </summary>
    public IActionResult OnPostAddTodo(int id, string title, int priority, string? dueDate)
    {
        if (string.IsNullOrWhiteSpace(title))
            return RedirectToPage(new { id });

        var list = _db.GetList(id);
        if (list == null) return RedirectToPage("/Index");

        var (success, errors) = _db.InsertTodo(title.Trim(), id, priority,
            string.IsNullOrWhiteSpace(dueDate) ? null : dueDate);
        if (!success)
        {
            Errors = errors;
            TodoList = _db.GetList(id);
            AllTags = _db.GetAllTags();
            return Page();
        }
        return RedirectToPage(new { id });
    }

    /// <summary>
    /// Toggle todo completed status.
    /// ORM: From, Select, Where, Update, Set, ToSql
    /// </summary>
    public IActionResult OnPostToggleTodo(int id, int todoId)
    {
        _db.ToggleTodo(todoId);
        return RedirectToPage(new { id });
    }

    /// <summary>
    /// Update todo.
    /// ORM: Changeset, Cast, ValidateRequired, ValidateLength, ValidateInt,
    ///      ValidateNumber, ToUpdateSql
    /// </summary>
    public IActionResult OnPostUpdateTodo(int id, int todoId, string title, int priority, string? dueDate)
    {
        if (!string.IsNullOrWhiteSpace(title))
        {
            var (success, errors) = _db.UpdateTodo(todoId, title.Trim(), priority,
                string.IsNullOrWhiteSpace(dueDate) ? null : dueDate);
            if (!success)
            {
                Errors = errors;
                TodoList = _db.GetList(id);
                AllTags = _db.GetAllTags();
                EditingTodoId = todoId;
                return Page();
            }
        }
        return RedirectToPage(new { id });
    }

    /// <summary>
    /// Delete a todo.
    /// ORM: DeleteFrom, Where, ToSql
    /// </summary>
    public IActionResult OnPostDeleteTodo(int id, int todoId)
    {
        _db.DeleteTodo(todoId);
        return RedirectToPage(new { id });
    }

    /// <summary>
    /// Complete all todos in the list.
    /// ORM: Update, Set, Where (compound), ToSql
    /// </summary>
    public IActionResult OnPostCompleteAll(int id)
    {
        _db.CompleteAllInList(id);
        return RedirectToPage(new { id });
    }

    /// <summary>
    /// Delete completed todos.
    /// ORM: DeleteFrom, Where (multiple), ToSql
    /// </summary>
    public IActionResult OnPostDeleteCompleted(int id)
    {
        _db.DeleteCompletedInList(id);
        return RedirectToPage(new { id });
    }

    /// <summary>
    /// Assign a tag to a todo.
    /// ORM: Changeset, Cast, ValidateRequired, ValidateInt, ToInsertSql
    /// </summary>
    public IActionResult OnPostAssignTag(int id, int todoId, int tagId)
    {
        _db.AssignTag(todoId, tagId);
        return RedirectToPage(new { id });
    }

    /// <summary>
    /// Remove a tag from a todo.
    /// ORM: DeleteFrom, Where (multiple AND), ToSql
    /// </summary>
    public IActionResult OnPostRemoveTag(int id, int todoId, int tagId)
    {
        _db.RemoveTag(todoId, tagId);
        return RedirectToPage(new { id });
    }
}
