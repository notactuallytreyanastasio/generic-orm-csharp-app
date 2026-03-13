namespace TodoApp.Models;

public class TodoItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Completed { get; set; }
    public int Priority { get; set; } = 3;
    public string? DueDate { get; set; }
    public int ListId { get; set; }
    public string CreatedAt { get; set; } = string.Empty;

    // Navigation helpers
    public string ListName { get; set; } = string.Empty;
    public List<Tag> Tags { get; set; } = new();

    public bool IsCompleted => Completed != 0;
    public string PriorityLabel => Priority switch
    {
        1 => "Critical",
        2 => "High",
        3 => "Medium",
        4 => "Low",
        5 => "Minimal",
        _ => "Unknown"
    };
}
