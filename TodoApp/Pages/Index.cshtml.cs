using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Pages;

public class IndexModel : PageModel
{
    private readonly TodoDb _db;

    public IndexModel(TodoDb db)
    {
        _db = db;
    }

    public List<TodoList> Lists { get; set; } = new();
    public int? EditingListId { get; set; }
    public List<string> Errors { get; set; } = new();

    public void OnGet(int? editId)
    {
        Lists = _db.GetAllLists();
        EditingListId = editId;
    }

    public IActionResult OnPostCreate(string name, string? description)
    {
        if (string.IsNullOrWhiteSpace(name))
            return RedirectToPage();

        var (success, errors) = _db.InsertList(name.Trim(), description?.Trim());
        if (!success)
        {
            Errors = errors;
            Lists = _db.GetAllLists();
            return Page();
        }
        return RedirectToPage();
    }

    public IActionResult OnPostUpdate(int id, string name, string? description)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            var (success, errors) = _db.UpdateList(id, name.Trim(), description?.Trim());
            if (!success)
            {
                Errors = errors;
                Lists = _db.GetAllLists();
                EditingListId = id;
                return Page();
            }
        }
        return RedirectToPage();
    }

    public IActionResult OnPostDelete(int id)
    {
        _db.DeleteList(id);
        return RedirectToPage();
    }
}
