using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Pages;

/// <summary>
/// Tags page demonstrating ORM join features and changeset validations:
/// InnerJoin, LeftJoin, Col, Changeset ValidateExclusion, ValidateLength,
/// DeleteSql, DeleteFrom with multiple Where conditions
/// </summary>
public class TagsModel : PageModel
{
    private readonly TodoDb _db;

    public TagsModel(TodoDb db)
    {
        _db = db;
    }

    public List<Tag> Tags { get; set; } = new();
    public List<string> Errors { get; set; } = new();

    public void OnGet()
    {
        // ORM: From, OrderBy, CountSql (for tag counts)
        Tags = _db.GetAllTags();
    }

    /// <summary>
    /// Create a new tag.
    /// ORM: Changeset, Cast, ValidateRequired, ValidateLength, ValidateExclusion, ToInsertSql
    /// </summary>
    public IActionResult OnPostCreate(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            Tags = _db.GetAllTags();
            return Page();
        }

        var (success, errors) = _db.CreateTag(name.Trim().ToLowerInvariant());
        if (!success)
        {
            Errors = errors;
            Tags = _db.GetAllTags();
            return Page();
        }
        return RedirectToPage();
    }

    /// <summary>
    /// Delete a tag.
    /// ORM: DeleteSql (quick delete by primary key)
    /// </summary>
    public IActionResult OnPostDelete(int tagId)
    {
        _db.DeleteTag(tagId);
        return RedirectToPage();
    }
}
