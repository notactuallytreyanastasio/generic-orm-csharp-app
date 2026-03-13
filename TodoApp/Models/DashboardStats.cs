namespace TodoApp.Models;

public class DashboardStats
{
    public int TotalLists { get; set; }
    public int TotalTodos { get; set; }
    public int CompletedTodos { get; set; }
    public int PendingTodos { get; set; }
    public int TotalTags { get; set; }
    public double AvgPriority { get; set; }
    public int MinPriority { get; set; }
    public int MaxPriority { get; set; }
    public List<ListSummary> ListSummaries { get; set; } = new();
    public List<PriorityBreakdown> PriorityBreakdowns { get; set; } = new();
    public List<TagSummary> TagSummaries { get; set; } = new();
}

public class ListSummary
{
    public string ListName { get; set; } = string.Empty;
    public int TodoCount { get; set; }
    public int CompletedCount { get; set; }
    public double AvgPriority { get; set; }
}

public class PriorityBreakdown
{
    public int Priority { get; set; }
    public int Count { get; set; }
    public string Label => Priority switch
    {
        1 => "Critical",
        2 => "High",
        3 => "Medium",
        4 => "Low",
        5 => "Minimal",
        _ => "Unknown"
    };
}

public class TagSummary
{
    public string TagName { get; set; } = string.Empty;
    public int TodoCount { get; set; }
}

public class SqlDemo
{
    public string Description { get; set; } = string.Empty;
    public string Feature { get; set; } = string.Empty;
    public string GeneratedSql { get; set; } = string.Empty;
}
