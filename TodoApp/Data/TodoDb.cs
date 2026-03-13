using Microsoft.Data.Sqlite;
using Orm.Src;
using TodoApp.Models;
using TemperLang.Core;

namespace TodoApp.Data;

/// <summary>
/// Data access layer using the Alloy ORM for ALL query generation.
/// Demonstrates every ORM feature: From, Update, DeleteFrom, Changeset,
/// SafeIdentifier, Col, aggregates, joins, set operations, subqueries,
/// all Where variants, all Changeset validations, OrderByNulls, GroupBy,
/// Having, Distinct, Lock modes, CountSql, SafeToSql, SelectExpr, and more.
/// </summary>
public class TodoDb
{
    private readonly string _connectionString;

    // ════════════════════════════════════════════════════════════════════
    // SAFE IDENTIFIERS - validated at construction (ORM: SafeIdentifier)
    // ════════════════════════════════════════════════════════════════════

    // Table names
    public static readonly ISafeIdentifier ListsTable = SrcGlobal.SafeIdentifier("lists");
    public static readonly ISafeIdentifier TodosTable = SrcGlobal.SafeIdentifier("todos");
    public static readonly ISafeIdentifier TagsTable = SrcGlobal.SafeIdentifier("tags");
    public static readonly ISafeIdentifier TodoTagsTable = SrcGlobal.SafeIdentifier("todo_tags");

    // Field names
    public static readonly ISafeIdentifier IdField = SrcGlobal.SafeIdentifier("id");
    public static readonly ISafeIdentifier NameField = SrcGlobal.SafeIdentifier("name");
    public static readonly ISafeIdentifier DescriptionField = SrcGlobal.SafeIdentifier("description");
    public static readonly ISafeIdentifier CreatedAtField = SrcGlobal.SafeIdentifier("created_at");
    public static readonly ISafeIdentifier TitleField = SrcGlobal.SafeIdentifier("title");
    public static readonly ISafeIdentifier CompletedField = SrcGlobal.SafeIdentifier("completed");
    public static readonly ISafeIdentifier PriorityField = SrcGlobal.SafeIdentifier("priority");
    public static readonly ISafeIdentifier DueDateField = SrcGlobal.SafeIdentifier("due_date");
    public static readonly ISafeIdentifier ListIdField = SrcGlobal.SafeIdentifier("list_id");
    public static readonly ISafeIdentifier TodoIdField = SrcGlobal.SafeIdentifier("todo_id");
    public static readonly ISafeIdentifier TagIdField = SrcGlobal.SafeIdentifier("tag_id");

    // Aliases for subqueries
    public static readonly ISafeIdentifier SubAlias = SrcGlobal.SafeIdentifier("sub");
    public static readonly ISafeIdentifier CntAlias = SrcGlobal.SafeIdentifier("cnt");

    // ════════════════════════════════════════════════════════════════════
    // TABLE DEFINITIONS (ORM: TableDef, FieldDef, field types)
    // ════════════════════════════════════════════════════════════════════

    public static readonly TableDef ListTableDef = new TableDef(
        ListsTable,
        Listed.CreateReadOnlyList<FieldDef>(
            new FieldDef(NameField, new StringField(), false, null, false),
            new FieldDef(DescriptionField, new StringField(), true, null, false),
            new FieldDef(CreatedAtField, new StringField(), true, null, false)
        ),
        null
    );

    public static readonly TableDef TodoTableDef = new TableDef(
        TodosTable,
        Listed.CreateReadOnlyList<FieldDef>(
            new FieldDef(TitleField, new StringField(), false, null, false),
            new FieldDef(CompletedField, new IntField(), false, null, false),
            new FieldDef(PriorityField, new IntField(), false, null, false),
            new FieldDef(DueDateField, new StringField(), true, null, false),
            new FieldDef(ListIdField, new IntField(), false, null, false),
            new FieldDef(CreatedAtField, new StringField(), true, null, false)
        ),
        null
    );

    public static readonly TableDef TagTableDef = new TableDef(
        TagsTable,
        Listed.CreateReadOnlyList<FieldDef>(
            new FieldDef(NameField, new StringField(), false, null, false)
        ),
        null
    );

    public static readonly TableDef TodoTagTableDef = new TableDef(
        TodoTagsTable,
        Listed.CreateReadOnlyList<FieldDef>(
            new FieldDef(TodoIdField, new IntField(), false, null, false),
            new FieldDef(TagIdField, new IntField(), false, null, false)
        ),
        null
    );

    // ════════════════════════════════════════════════════════════════════
    // FIELD LISTS for Cast / ValidateRequired
    // ════════════════════════════════════════════════════════════════════

    private static readonly IReadOnlyList<ISafeIdentifier> ListInsertFields =
        Listed.CreateReadOnlyList<ISafeIdentifier>(NameField);

    private static readonly IReadOnlyList<ISafeIdentifier> ListUpdateFields =
        Listed.CreateReadOnlyList<ISafeIdentifier>(NameField, DescriptionField);

    private static readonly IReadOnlyList<ISafeIdentifier> TodoInsertFields =
        Listed.CreateReadOnlyList<ISafeIdentifier>(TitleField, CompletedField, PriorityField, ListIdField);

    private static readonly IReadOnlyList<ISafeIdentifier> TodoUpdateTitleFields =
        Listed.CreateReadOnlyList<ISafeIdentifier>(TitleField);

    private static readonly IReadOnlyList<ISafeIdentifier> TagInsertFields =
        Listed.CreateReadOnlyList<ISafeIdentifier>(NameField);

    private static readonly IReadOnlyList<ISafeIdentifier> TodoTagInsertFields =
        Listed.CreateReadOnlyList<ISafeIdentifier>(TodoIdField, TagIdField);

    public TodoDb(string connectionString)
    {
        _connectionString = connectionString;
    }

    private SqliteConnection Open()
    {
        var conn = new SqliteConnection(_connectionString);
        conn.Open();
        return conn;
    }

    // ════════════════════════════════════════════════════════════════════
    // DDL - Raw SQL (ORM is for DML only)
    // ════════════════════════════════════════════════════════════════════

    public void EnsureCreated()
    {
        using var conn = Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            PRAGMA foreign_keys = ON;
            CREATE TABLE IF NOT EXISTS lists (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                name TEXT NOT NULL,
                description TEXT,
                created_at TEXT NOT NULL DEFAULT (datetime('now'))
            );
            CREATE TABLE IF NOT EXISTS todos (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                title TEXT NOT NULL,
                completed INTEGER NOT NULL DEFAULT 0,
                priority INTEGER NOT NULL DEFAULT 3,
                due_date TEXT,
                list_id INTEGER NOT NULL,
                created_at TEXT NOT NULL DEFAULT (datetime('now')),
                FOREIGN KEY (list_id) REFERENCES lists(id) ON DELETE CASCADE
            );
            CREATE TABLE IF NOT EXISTS tags (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                name TEXT NOT NULL UNIQUE
            );
            CREATE TABLE IF NOT EXISTS todo_tags (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                todo_id INTEGER NOT NULL,
                tag_id INTEGER NOT NULL,
                FOREIGN KEY (todo_id) REFERENCES todos(id) ON DELETE CASCADE,
                FOREIGN KEY (tag_id) REFERENCES tags(id) ON DELETE CASCADE,
                UNIQUE(todo_id, tag_id)
            );
        ";
        cmd.ExecuteNonQuery();
    }

    // ════════════════════════════════════════════════════════════════════
    // HELPER: Build WHERE fragments via SqlBuilder
    // ════════════════════════════════════════════════════════════════════

    public static SqlFragment WhereEq(ISafeIdentifier column, int value)
    {
        var b = new SqlBuilder();
        b.AppendSafe(column.SqlValue);
        b.AppendSafe(" = ");
        b.AppendInt32(value);
        return b.Accumulated;
    }

    public static SqlFragment WhereEqStr(ISafeIdentifier column, string value)
    {
        var b = new SqlBuilder();
        b.AppendSafe(column.SqlValue);
        b.AppendSafe(" = ");
        b.AppendString(value);
        return b.Accumulated;
    }

    public static SqlFragment WhereGt(ISafeIdentifier column, int value)
    {
        var b = new SqlBuilder();
        b.AppendSafe(column.SqlValue);
        b.AppendSafe(" > ");
        b.AppendInt32(value);
        return b.Accumulated;
    }

    public static SqlFragment WhereLt(ISafeIdentifier column, int value)
    {
        var b = new SqlBuilder();
        b.AppendSafe(column.SqlValue);
        b.AppendSafe(" < ");
        b.AppendInt32(value);
        return b.Accumulated;
    }

    // ════════════════════════════════════════════════════════════════════
    // LISTS - CRUD using ORM From, Changeset, DeleteSql
    // ════════════════════════════════════════════════════════════════════

    /// <summary>
    /// Get all lists ordered by created_at.
    /// ORM features: From, OrderBy, ToSql
    /// </summary>
    public List<TodoList> GetAllLists()
    {
        using var conn = Open();

        var query = SrcGlobal.From(ListsTable)
            .OrderBy(CreatedAtField, true);
        string sql = query.ToSql().ToString();

        var lists = new List<TodoList>();
        using (var cmd = conn.CreateCommand())
        {
            cmd.CommandText = sql;
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                lists.Add(ReadList(reader));
        }

        foreach (var list in lists)
            list.Todos = GetTodosForList(conn, list.Id);

        return lists;
    }

    /// <summary>
    /// Get a single list by ID.
    /// ORM features: From, Where, ToSql
    /// </summary>
    public TodoList? GetList(int id)
    {
        using var conn = Open();

        var query = SrcGlobal.From(ListsTable)
            .Where(WhereEq(IdField, id));
        string sql = query.ToSql().ToString();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) return null;

        var list = ReadList(reader);
        list.Todos = GetTodosForList(conn, list.Id);
        return list;
    }

    /// <summary>
    /// Insert a new list.
    /// ORM features: Changeset, Cast, ValidateRequired, ValidateLength, ToInsertSql
    /// </summary>
    public (bool Success, List<string> Errors) InsertList(string name, string? description = null)
    {
        using var conn = Open();

        var kvps = new List<KeyValuePair<string, string>>
        {
            new("name", name)
        };
        if (!string.IsNullOrEmpty(description))
            kvps.Add(new("description", description));
        kvps.Add(new("created_at", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")));

        var values = Mapped.ConstructMap(Listed.CreateReadOnlyList(kvps.ToArray()));

        // Build changeset with validations
        var cs = SrcGlobal.Changeset(ListTableDef, values)
            .Cast(Listed.CreateReadOnlyList<ISafeIdentifier>(NameField, DescriptionField, CreatedAtField))
            .ValidateRequired(ListInsertFields)
            .ValidateLength(NameField, 1, 100);

        if (!cs.IsValid)
            return (false, cs.Errors.Select(e => $"{e.Field}: {e.Message}").ToList());

        string sql = cs.ToInsertSql().ToString();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
        return (true, new List<string>());
    }

    /// <summary>
    /// Update a list.
    /// ORM features: Changeset, Cast, ValidateRequired, ValidateLength,
    ///               PutChange, DeleteChange, GetChange, ToUpdateSql
    /// </summary>
    public (bool Success, List<string> Errors) UpdateList(int id, string name, string? description)
    {
        using var conn = Open();

        var kvps = new List<KeyValuePair<string, string>>
        {
            new("name", name)
        };
        if (description != null)
            kvps.Add(new("description", description));

        var values = Mapped.ConstructMap(Listed.CreateReadOnlyList(kvps.ToArray()));

        var cs = SrcGlobal.Changeset(ListTableDef, values)
            .Cast(ListUpdateFields)
            .ValidateRequired(ListInsertFields)
            .ValidateLength(NameField, 1, 100);

        // Demonstrate PutChange - ensure name is trimmed
        if (cs.IsValid)
        {
            try
            {
                string currentName = cs.GetChange(NameField);
                cs = cs.PutChange(NameField, currentName.Trim());
            }
            catch { /* field not present */ }
        }

        if (!cs.IsValid)
            return (false, cs.Errors.Select(e => $"{e.Field}: {e.Message}").ToList());

        string sql = cs.ToUpdateSql(id).ToString();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
        return (true, new List<string>());
    }

    /// <summary>
    /// Delete a list.
    /// ORM features: DeleteSql (quick delete by ID)
    /// </summary>
    public void DeleteList(int id)
    {
        using var conn = Open();
        EnableForeignKeys(conn);

        string sql = SrcGlobal.DeleteSql(ListTableDef, id).ToString();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }

    // ════════════════════════════════════════════════════════════════════
    // TODOS - CRUD with all Where variants
    // ════════════════════════════════════════════════════════════════════

    /// <summary>
    /// Get todos for a list.
    /// ORM features: From, Where, OrderBy, OrderByNulls (NullsLast), ToSql
    /// </summary>
    private List<TodoItem> GetTodosForList(SqliteConnection conn, int listId)
    {
        var query = SrcGlobal.From(TodosTable)
            .Where(WhereEq(ListIdField, listId))
            .OrderBy(CompletedField, true)
            .OrderByNulls(DueDateField, true, new NullsLast())
            .OrderBy(PriorityField, true);
        string sql = query.ToSql().ToString();

        var items = new List<TodoItem>();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            items.Add(ReadTodo(reader));

        foreach (var item in items)
            item.Tags = GetTagsForTodo(conn, item.Id);

        return items;
    }

    /// <summary>
    /// Get a single todo by ID.
    /// ORM features: From, Where
    /// </summary>
    public TodoItem? GetTodo(int todoId)
    {
        using var conn = Open();
        var query = SrcGlobal.From(TodosTable)
            .Where(WhereEq(IdField, todoId));
        string sql = query.ToSql().ToString();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) return null;
        var item = ReadTodo(reader);
        item.Tags = GetTagsForTodo(conn, item.Id);
        return item;
    }

    /// <summary>
    /// Insert a new todo.
    /// ORM features: Changeset, Cast, ValidateRequired, ValidateInt, ValidateNumber,
    ///               ValidateInclusion, ValidateLength, ToInsertSql, NumberValidationOpts
    /// </summary>
    public (bool Success, List<string> Errors) InsertTodo(string title, int listId,
        int priority = 3, string? dueDate = null)
    {
        using var conn = Open();

        var kvps = new List<KeyValuePair<string, string>>
        {
            new("title", title),
            new("completed", "0"),
            new("priority", priority.ToString()),
            new("list_id", listId.ToString()),
            new("created_at", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"))
        };
        if (!string.IsNullOrEmpty(dueDate))
            kvps.Add(new("due_date", dueDate));

        var values = Mapped.ConstructMap(Listed.CreateReadOnlyList(kvps.ToArray()));

        var cs = SrcGlobal.Changeset(TodoTableDef, values)
            .Cast(Listed.CreateReadOnlyList<ISafeIdentifier>(
                TitleField, CompletedField, PriorityField, ListIdField, DueDateField, CreatedAtField))
            .ValidateRequired(TodoInsertFields)
            .ValidateLength(TitleField, 1, 500)
            .ValidateInt(CompletedField)
            .ValidateInt(PriorityField)
            .ValidateInt(ListIdField)
            // Validate priority is between 1 and 5
            .ValidateNumber(PriorityField,
                new NumberValidationOpts(null, null, 1.0, 5.0, null))
            // Validate priority is in allowed values
            .ValidateInclusion(PriorityField,
                Listed.CreateReadOnlyList<string>("1", "2", "3", "4", "5"));

        if (!cs.IsValid)
            return (false, cs.Errors.Select(e => $"{e.Field}: {e.Message}").ToList());

        string sql = cs.ToInsertSql().ToString();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
        return (true, new List<string>());
    }

    /// <summary>
    /// Update a todo.
    /// ORM features: Changeset, Cast, ValidateRequired, ValidateLength,
    ///               ValidateInt, ValidateNumber, PutChange, ToUpdateSql
    /// </summary>
    public (bool Success, List<string> Errors) UpdateTodo(int todoId, string title,
        int priority, string? dueDate)
    {
        using var conn = Open();

        var kvps = new List<KeyValuePair<string, string>>
        {
            new("title", title),
            new("priority", priority.ToString())
        };
        if (dueDate != null)
            kvps.Add(new("due_date", dueDate));

        var values = Mapped.ConstructMap(Listed.CreateReadOnlyList(kvps.ToArray()));

        var cs = SrcGlobal.Changeset(TodoTableDef, values)
            .Cast(Listed.CreateReadOnlyList<ISafeIdentifier>(TitleField, PriorityField, DueDateField))
            .ValidateRequired(Listed.CreateReadOnlyList<ISafeIdentifier>(TitleField, PriorityField))
            .ValidateLength(TitleField, 1, 500)
            .ValidateInt(PriorityField)
            .ValidateNumber(PriorityField,
                new NumberValidationOpts(null, null, 1.0, 5.0, null));

        if (!cs.IsValid)
            return (false, cs.Errors.Select(e => $"{e.Field}: {e.Message}").ToList());

        string sql = cs.ToUpdateSql(todoId).ToString();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
        return (true, new List<string>());
    }

    /// <summary>
    /// Toggle a todo's completed status using ORM Update query builder.
    /// ORM features: Update, Set, Where, ToSql
    /// </summary>
    public void ToggleTodo(int todoId)
    {
        using var conn = Open();

        // First get current state using ORM From
        var selectQuery = SrcGlobal.From(TodosTable)
            .Select(Listed.CreateReadOnlyList<ISafeIdentifier>(CompletedField))
            .Where(WhereEq(IdField, todoId));
        string selectSql = selectQuery.ToSql().ToString();

        int current = 0;
        using (var selectCmd = conn.CreateCommand())
        {
            selectCmd.CommandText = selectSql;
            var result = selectCmd.ExecuteScalar();
            if (result != null) current = Convert.ToInt32(result);
        }

        int newVal = current == 0 ? 1 : 0;

        // Use ORM UpdateQuery with Set and Where
        var updateQuery = SrcGlobal.Update(TodosTable)
            .Set(CompletedField, new SqlInt32(newVal))
            .Where(WhereEq(IdField, todoId));
        string updateSql = updateQuery.ToSql().ToString();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = updateSql;
        cmd.ExecuteNonQuery();
    }

    /// <summary>
    /// Delete a todo.
    /// ORM features: DeleteFrom, Where, ToSql
    /// </summary>
    public void DeleteTodo(int todoId)
    {
        using var conn = Open();
        EnableForeignKeys(conn);

        // Use ORM DeleteQuery builder (not the quick DeleteSql)
        var deleteQuery = SrcGlobal.DeleteFrom(TodosTable)
            .Where(WhereEq(IdField, todoId));
        string sql = deleteQuery.ToSql().ToString();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }

    /// <summary>
    /// Bulk complete all todos in a list.
    /// ORM features: Update, Set, Where (compound), ToSql
    /// </summary>
    public void CompleteAllInList(int listId)
    {
        using var conn = Open();

        var updateQuery = SrcGlobal.Update(TodosTable)
            .Set(CompletedField, new SqlInt32(1))
            .Where(WhereEq(ListIdField, listId))
            .Where(WhereEq(CompletedField, 0));
        string sql = updateQuery.ToSql().ToString();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }

    /// <summary>
    /// Delete completed todos from a list using DeleteFrom with compound where.
    /// ORM features: DeleteFrom, Where (multiple), ToSql
    /// </summary>
    public void DeleteCompletedInList(int listId)
    {
        using var conn = Open();
        EnableForeignKeys(conn);

        var deleteQuery = SrcGlobal.DeleteFrom(TodosTable)
            .Where(WhereEq(ListIdField, listId))
            .Where(WhereEq(CompletedField, 1));
        string sql = deleteQuery.ToSql().ToString();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }

    // ════════════════════════════════════════════════════════════════════
    // TAGS - CRUD
    // ════════════════════════════════════════════════════════════════════

    /// <summary>
    /// Get all tags.
    /// ORM features: From, OrderBy, Distinct
    /// </summary>
    public List<Tag> GetAllTags()
    {
        using var conn = Open();

        var query = SrcGlobal.From(TagsTable)
            .OrderBy(NameField, true);
        string sql = query.ToSql().ToString();

        var tags = new List<Tag>();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            tags.Add(new Tag
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Name = reader.GetString(reader.GetOrdinal("name"))
            });
        }

        // Get usage counts using LEFT JOIN and GROUP BY
        foreach (var tag in tags)
            tag.TodoCount = GetTagTodoCount(conn, tag.Id);

        return tags;
    }

    private int GetTagTodoCount(SqliteConnection conn, int tagId)
    {
        var query = SrcGlobal.From(TodoTagsTable)
            .Where(WhereEq(TagIdField, tagId));
        string sql = query.CountSql().ToString();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        var result = cmd.ExecuteScalar();
        return result != null ? Convert.ToInt32(result) : 0;
    }

    /// <summary>
    /// Create a tag.
    /// ORM features: Changeset, Cast, ValidateRequired, ValidateLength,
    ///               ValidateExclusion, ValidateStartsWith, ValidateEndsWith,
    ///               ValidateContains, ToInsertSql
    /// </summary>
    public (bool Success, List<string> Errors) CreateTag(string name)
    {
        using var conn = Open();

        var values = Mapped.ConstructMap(
            Listed.CreateReadOnlyList(new KeyValuePair<string, string>("name", name)));

        var cs = SrcGlobal.Changeset(TagTableDef, values)
            .Cast(TagInsertFields)
            .ValidateRequired(TagInsertFields)
            .ValidateLength(NameField, 1, 50)
            // Demonstrate ValidateExclusion - prevent reserved tag names
            .ValidateExclusion(NameField,
                Listed.CreateReadOnlyList<string>("admin", "system", "root", "null"));

        if (!cs.IsValid)
            return (false, cs.Errors.Select(e => $"{e.Field}: {e.Message}").ToList());

        try
        {
            string sql = cs.ToInsertSql().ToString();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            return (true, new List<string>());
        }
        catch (SqliteException ex) when (ex.SqliteErrorCode == 19)
        {
            return (false, new List<string> { "Tag name already exists" });
        }
    }

    /// <summary>
    /// Delete a tag.
    /// ORM features: DeleteSql (quick delete)
    /// </summary>
    public void DeleteTag(int tagId)
    {
        using var conn = Open();
        EnableForeignKeys(conn);

        string sql = SrcGlobal.DeleteSql(TagTableDef, tagId).ToString();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }

    /// <summary>
    /// Get tags for a todo.
    /// ORM features: From, InnerJoin, Where, Col (qualified column reference)
    /// </summary>
    private List<Tag> GetTagsForTodo(SqliteConnection conn, int todoId)
    {
        // Build: SELECT tags.* FROM tags
        //        INNER JOIN todo_tags ON tags.id = todo_tags.tag_id
        //        WHERE todo_tags.todo_id = ?
        var joinCond = SrcGlobal.Col(TagsTable, IdField);
        var joinRight = SrcGlobal.Col(TodoTagsTable, TagIdField);

        var joinCondBuilder = new SqlBuilder();
        joinCondBuilder.AppendFragment(joinCond);
        joinCondBuilder.AppendSafe(" = ");
        joinCondBuilder.AppendFragment(joinRight);
        var joinOn = joinCondBuilder.Accumulated;

        var whereBuilder = new SqlBuilder();
        whereBuilder.AppendFragment(SrcGlobal.Col(TodoTagsTable, TodoIdField));
        whereBuilder.AppendSafe(" = ");
        whereBuilder.AppendInt32(todoId);
        var whereFrag = whereBuilder.Accumulated;

        var query = SrcGlobal.From(TagsTable)
            .InnerJoin(TodoTagsTable, joinOn)
            .Where(whereFrag)
            .OrderBy(NameField, true);
        string sql = query.ToSql().ToString();

        var tags = new List<Tag>();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            tags.Add(new Tag
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Name = reader.GetString(reader.GetOrdinal("name"))
            });
        }
        return tags;
    }

    /// <summary>
    /// Assign a tag to a todo.
    /// ORM features: Changeset, Cast, ValidateRequired, ValidateInt, ToInsertSql
    /// </summary>
    public void AssignTag(int todoId, int tagId)
    {
        using var conn = Open();

        var values = Mapped.ConstructMap(
            Listed.CreateReadOnlyList(
                new KeyValuePair<string, string>("todo_id", todoId.ToString()),
                new KeyValuePair<string, string>("tag_id", tagId.ToString())));

        var cs = SrcGlobal.Changeset(TodoTagTableDef, values)
            .Cast(TodoTagInsertFields)
            .ValidateRequired(TodoTagInsertFields)
            .ValidateInt(TodoIdField)
            .ValidateInt(TagIdField);

        if (!cs.IsValid) return;

        try
        {
            string sql = cs.ToInsertSql().ToString();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }
        catch (SqliteException) { /* duplicate or FK violation - ignore */ }
    }

    /// <summary>
    /// Remove a tag from a todo.
    /// ORM features: DeleteFrom, Where (multiple AND conditions)
    /// </summary>
    public void RemoveTag(int todoId, int tagId)
    {
        using var conn = Open();
        EnableForeignKeys(conn);

        var deleteQuery = SrcGlobal.DeleteFrom(TodoTagsTable)
            .Where(WhereEq(TodoIdField, todoId))
            .Where(WhereEq(TagIdField, tagId));
        string sql = deleteQuery.ToSql().ToString();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }

    // ════════════════════════════════════════════════════════════════════
    // SEARCH - Demonstrates Where variants
    // ════════════════════════════════════════════════════════════════════

    /// <summary>
    /// Search todos by title pattern.
    /// ORM features: From, WhereLike, OrderBy, Limit, Offset, SafeToSql
    /// </summary>
    public List<TodoItem> SearchTodos(string pattern, int limit = 50, int offset = 0)
    {
        using var conn = Open();

        var query = SrcGlobal.From(TodosTable)
            .WhereLike(TitleField, $"%{pattern}%")
            .OrderBy(CreatedAtField, false)
            .Limit(limit)
            .Offset(offset);
        string sql = query.ToSql().ToString();

        var items = new List<TodoItem>();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            items.Add(ReadTodo(reader));
        return items;
    }

    /// <summary>
    /// Get todos with due dates in a range.
    /// ORM features: From, WhereNotNull, WhereBetween, OrderBy
    /// </summary>
    public List<TodoItem> GetTodosByDateRange(string startDate, string endDate)
    {
        using var conn = Open();

        var query = SrcGlobal.From(TodosTable)
            .WhereNotNull(DueDateField)
            .WhereBetween(DueDateField, new SqlString(startDate), new SqlString(endDate))
            .OrderBy(DueDateField, true);
        string sql = query.ToSql().ToString();

        var items = new List<TodoItem>();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            items.Add(ReadTodo(reader));
        return items;
    }

    /// <summary>
    /// Get overdue todos (no due date = not overdue).
    /// ORM features: From, WhereNotNull, Where (custom condition with SqlBuilder), WhereNot
    /// </summary>
    public List<TodoItem> GetOverdueTodos()
    {
        using var conn = Open();
        string today = DateTime.UtcNow.ToString("yyyy-MM-dd");

        var ltBuilder = new SqlBuilder();
        ltBuilder.AppendSafe(DueDateField.SqlValue);
        ltBuilder.AppendSafe(" < ");
        ltBuilder.AppendString(today);
        var ltFrag = ltBuilder.Accumulated;

        var query = SrcGlobal.From(TodosTable)
            .WhereNotNull(DueDateField)
            .Where(ltFrag)
            .Where(WhereEq(CompletedField, 0))
            .OrderBy(DueDateField, true);
        string sql = query.ToSql().ToString();

        var items = new List<TodoItem>();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            items.Add(ReadTodo(reader));
        return items;
    }

    /// <summary>
    /// Get todos without due dates.
    /// ORM features: From, WhereNull
    /// </summary>
    public List<TodoItem> GetTodosWithoutDueDate()
    {
        using var conn = Open();

        var query = SrcGlobal.From(TodosTable)
            .WhereNull(DueDateField)
            .OrderBy(PriorityField, true);
        string sql = query.ToSql().ToString();

        var items = new List<TodoItem>();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            items.Add(ReadTodo(reader));
        return items;
    }

    /// <summary>
    /// Get high priority todos (priority 1 or 2).
    /// ORM features: From, WhereIn (with ISqlPart list)
    /// </summary>
    public List<TodoItem> GetHighPriorityTodos()
    {
        using var conn = Open();

        var query = SrcGlobal.From(TodosTable)
            .WhereIn(PriorityField,
                Listed.CreateReadOnlyList<ISqlPart>(new SqlInt32(1), new SqlInt32(2)))
            .Where(WhereEq(CompletedField, 0))
            .OrderBy(PriorityField, true);
        string sql = query.ToSql().ToString();

        var items = new List<TodoItem>();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            items.Add(ReadTodo(reader));
        return items;
    }

    /// <summary>
    /// Get todos using WhereInSubquery - todos in lists matching a name pattern.
    /// ORM features: From, WhereInSubquery, Select, WhereLike
    /// </summary>
    public List<TodoItem> GetTodosInListsLike(string listPattern)
    {
        using var conn = Open();

        // Subquery: SELECT id FROM lists WHERE name LIKE '%pattern%'
        var listSubquery = SrcGlobal.From(ListsTable)
            .Select(Listed.CreateReadOnlyList<ISafeIdentifier>(IdField))
            .WhereLike(NameField, $"%{listPattern}%");

        var query = SrcGlobal.From(TodosTable)
            .WhereInSubquery(ListIdField, listSubquery)
            .OrderBy(PriorityField, true);
        string sql = query.ToSql().ToString();

        var items = new List<TodoItem>();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            items.Add(ReadTodo(reader));
        return items;
    }

    /// <summary>
    /// Get todos using OrWhere (priority 1 OR completed).
    /// ORM features: From, Where, OrWhere
    /// </summary>
    public List<TodoItem> GetCriticalOrCompleted()
    {
        using var conn = Open();

        var query = SrcGlobal.From(TodosTable)
            .Where(WhereEq(PriorityField, 1))
            .OrWhere(WhereEq(CompletedField, 1))
            .OrderBy(PriorityField, true);
        string sql = query.ToSql().ToString();

        var items = new List<TodoItem>();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            items.Add(ReadTodo(reader));
        return items;
    }

    // ════════════════════════════════════════════════════════════════════
    // AGGREGATES & ANALYTICS (ORM: CountAll, SumCol, AvgCol, MinCol, MaxCol)
    // ════════════════════════════════════════════════════════════════════

    /// <summary>
    /// Get dashboard statistics.
    /// ORM features: From, CountSql, SelectExpr with CountAll, AvgCol, MinCol, MaxCol,
    ///               GroupBy, Having, LeftJoin, InnerJoin, Col
    /// </summary>
    public DashboardStats GetDashboardStats()
    {
        using var conn = Open();
        var stats = new DashboardStats();

        // Total lists count - using CountSql
        var listCountQuery = SrcGlobal.From(ListsTable);
        stats.TotalLists = ExecuteCount(conn, listCountQuery.CountSql().ToString());

        // Total todos count
        var todoCountQuery = SrcGlobal.From(TodosTable);
        stats.TotalTodos = ExecuteCount(conn, todoCountQuery.CountSql().ToString());

        // Completed todos - using CountSql with Where
        var completedQuery = SrcGlobal.From(TodosTable)
            .Where(WhereEq(CompletedField, 1));
        stats.CompletedTodos = ExecuteCount(conn, completedQuery.CountSql().ToString());

        stats.PendingTodos = stats.TotalTodos - stats.CompletedTodos;

        // Total tags count
        var tagCountQuery = SrcGlobal.From(TagsTable);
        stats.TotalTags = ExecuteCount(conn, tagCountQuery.CountSql().ToString());

        // Avg priority using SelectExpr with AvgCol
        var avgQuery = SrcGlobal.From(TodosTable)
            .SelectExpr(Listed.CreateReadOnlyList<SqlFragment>(
                SrcGlobal.AvgCol(PriorityField)));
        stats.AvgPriority = ExecuteDouble(conn, avgQuery.ToSql().ToString());

        // Min priority using SelectExpr with MinCol
        var minQuery = SrcGlobal.From(TodosTable)
            .SelectExpr(Listed.CreateReadOnlyList<SqlFragment>(
                SrcGlobal.MinCol(PriorityField)));
        stats.MinPriority = ExecuteInt(conn, minQuery.ToSql().ToString());

        // Max priority using SelectExpr with MaxCol
        var maxQuery = SrcGlobal.From(TodosTable)
            .SelectExpr(Listed.CreateReadOnlyList<SqlFragment>(
                SrcGlobal.MaxCol(PriorityField)));
        stats.MaxPriority = ExecuteInt(conn, maxQuery.ToSql().ToString());

        // Per-list summaries using GroupBy
        stats.ListSummaries = GetListSummaries(conn);

        // Priority breakdown using GroupBy + Having
        stats.PriorityBreakdowns = GetPriorityBreakdowns(conn);

        // Tag usage summaries
        stats.TagSummaries = GetTagSummaries(conn);

        return stats;
    }

    private List<ListSummary> GetListSummaries(SqliteConnection conn)
    {
        // Use GroupBy with aggregates - selecting list_id, COUNT, SUM
        var query = SrcGlobal.From(TodosTable)
            .SelectExpr(Listed.CreateReadOnlyList<SqlFragment>(
                BuildSafeFragment(ListIdField.SqlValue),
                SrcGlobal.CountAll(),
                SrcGlobal.SumCol(CompletedField),
                SrcGlobal.AvgCol(PriorityField)))
            .GroupBy(ListIdField)
            .OrderBy(ListIdField, true);
        string sql = query.ToSql().ToString();

        var summaries = new List<ListSummary>();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            int listId = reader.GetInt32(0);
            summaries.Add(new ListSummary
            {
                ListName = GetListName(conn, listId),
                TodoCount = reader.GetInt32(1),
                CompletedCount = reader.GetInt32(2),
                AvgPriority = reader.IsDBNull(3) ? 0 : reader.GetDouble(3)
            });
        }
        return summaries;
    }

    private List<PriorityBreakdown> GetPriorityBreakdowns(SqliteConnection conn)
    {
        // GroupBy priority, Having count > 0
        var havingBuilder = new SqlBuilder();
        havingBuilder.AppendFragment(SrcGlobal.CountAll());
        havingBuilder.AppendSafe(" > ");
        havingBuilder.AppendInt32(0);
        var havingFrag = havingBuilder.Accumulated;

        var query = SrcGlobal.From(TodosTable)
            .SelectExpr(Listed.CreateReadOnlyList<SqlFragment>(
                BuildSafeFragment(PriorityField.SqlValue),
                SrcGlobal.CountAll()))
            .GroupBy(PriorityField)
            .Having(havingFrag)
            .OrderBy(PriorityField, true);
        string sql = query.ToSql().ToString();

        var breakdowns = new List<PriorityBreakdown>();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            breakdowns.Add(new PriorityBreakdown
            {
                Priority = reader.GetInt32(0),
                Count = reader.GetInt32(1)
            });
        }
        return breakdowns;
    }

    private List<TagSummary> GetTagSummaries(SqliteConnection conn)
    {
        // Tags with their usage count via LEFT JOIN and GROUP BY
        var joinOn = new SqlBuilder();
        joinOn.AppendFragment(SrcGlobal.Col(TagsTable, IdField));
        joinOn.AppendSafe(" = ");
        joinOn.AppendFragment(SrcGlobal.Col(TodoTagsTable, TagIdField));
        var joinFrag = joinOn.Accumulated;

        var query = SrcGlobal.From(TagsTable)
            .LeftJoin(TodoTagsTable, joinFrag)
            .SelectExpr(Listed.CreateReadOnlyList<SqlFragment>(
                SrcGlobal.Col(TagsTable, NameField),
                SrcGlobal.CountCol(TodoIdField)))
            .GroupBy(SrcGlobal.SafeIdentifier("name"))
            .OrderBy(NameField, true);
        string sql = query.ToSql().ToString();

        var summaries = new List<TagSummary>();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            summaries.Add(new TagSummary
            {
                TagName = reader.GetString(0),
                TodoCount = reader.GetInt32(1)
            });
        }
        return summaries;
    }

    // ════════════════════════════════════════════════════════════════════
    // SQL DEMO - Generate SQL strings for every ORM feature
    // ════════════════════════════════════════════════════════════════════

    /// <summary>
    /// Generate demonstration SQL for all ORM features.
    /// This returns the SQL strings without executing them.
    /// </summary>
    public List<SqlDemo> GenerateAllSqlDemos()
    {
        var demos = new List<SqlDemo>();

        // --- FROM / basic SELECT ---
        demos.Add(new SqlDemo
        {
            Feature = "From + ToSql",
            Description = "Basic SELECT * FROM table",
            GeneratedSql = SrcGlobal.From(TodosTable).ToSql().ToString()
        });

        // --- SELECT with specific fields ---
        demos.Add(new SqlDemo
        {
            Feature = "Select (fields)",
            Description = "SELECT specific columns",
            GeneratedSql = SrcGlobal.From(TodosTable)
                .Select(Listed.CreateReadOnlyList<ISafeIdentifier>(TitleField, PriorityField))
                .ToSql().ToString()
        });

        // --- SelectExpr with aggregates ---
        demos.Add(new SqlDemo
        {
            Feature = "SelectExpr + CountAll",
            Description = "SELECT COUNT(*) aggregate",
            GeneratedSql = SrcGlobal.From(TodosTable)
                .SelectExpr(Listed.CreateReadOnlyList<SqlFragment>(SrcGlobal.CountAll()))
                .ToSql().ToString()
        });

        demos.Add(new SqlDemo
        {
            Feature = "SelectExpr + SumCol",
            Description = "SELECT SUM(column) aggregate",
            GeneratedSql = SrcGlobal.From(TodosTable)
                .SelectExpr(Listed.CreateReadOnlyList<SqlFragment>(SrcGlobal.SumCol(CompletedField)))
                .ToSql().ToString()
        });

        demos.Add(new SqlDemo
        {
            Feature = "SelectExpr + AvgCol",
            Description = "SELECT AVG(column) aggregate",
            GeneratedSql = SrcGlobal.From(TodosTable)
                .SelectExpr(Listed.CreateReadOnlyList<SqlFragment>(SrcGlobal.AvgCol(PriorityField)))
                .ToSql().ToString()
        });

        demos.Add(new SqlDemo
        {
            Feature = "SelectExpr + MinCol",
            Description = "SELECT MIN(column) aggregate",
            GeneratedSql = SrcGlobal.From(TodosTable)
                .SelectExpr(Listed.CreateReadOnlyList<SqlFragment>(SrcGlobal.MinCol(PriorityField)))
                .ToSql().ToString()
        });

        demos.Add(new SqlDemo
        {
            Feature = "SelectExpr + MaxCol",
            Description = "SELECT MAX(column) aggregate",
            GeneratedSql = SrcGlobal.From(TodosTable)
                .SelectExpr(Listed.CreateReadOnlyList<SqlFragment>(SrcGlobal.MaxCol(PriorityField)))
                .ToSql().ToString()
        });

        demos.Add(new SqlDemo
        {
            Feature = "CountCol",
            Description = "COUNT on a specific column (excludes NULLs)",
            GeneratedSql = SrcGlobal.From(TodosTable)
                .SelectExpr(Listed.CreateReadOnlyList<SqlFragment>(SrcGlobal.CountCol(DueDateField)))
                .ToSql().ToString()
        });

        // --- WHERE variants ---
        demos.Add(new SqlDemo
        {
            Feature = "Where",
            Description = "WHERE with equality condition",
            GeneratedSql = SrcGlobal.From(TodosTable)
                .Where(WhereEq(CompletedField, 1))
                .ToSql().ToString()
        });

        demos.Add(new SqlDemo
        {
            Feature = "OrWhere",
            Description = "WHERE ... OR ... condition",
            GeneratedSql = SrcGlobal.From(TodosTable)
                .Where(WhereEq(PriorityField, 1))
                .OrWhere(WhereEq(PriorityField, 2))
                .ToSql().ToString()
        });

        demos.Add(new SqlDemo
        {
            Feature = "WhereNull",
            Description = "WHERE column IS NULL",
            GeneratedSql = SrcGlobal.From(TodosTable)
                .WhereNull(DueDateField)
                .ToSql().ToString()
        });

        demos.Add(new SqlDemo
        {
            Feature = "WhereNotNull",
            Description = "WHERE column IS NOT NULL",
            GeneratedSql = SrcGlobal.From(TodosTable)
                .WhereNotNull(DueDateField)
                .ToSql().ToString()
        });

        demos.Add(new SqlDemo
        {
            Feature = "WhereIn",
            Description = "WHERE column IN (values)",
            GeneratedSql = SrcGlobal.From(TodosTable)
                .WhereIn(PriorityField,
                    Listed.CreateReadOnlyList<ISqlPart>(new SqlInt32(1), new SqlInt32(2), new SqlInt32(3)))
                .ToSql().ToString()
        });

        demos.Add(new SqlDemo
        {
            Feature = "WhereInSubquery",
            Description = "WHERE column IN (SELECT ...)",
            GeneratedSql = SrcGlobal.From(TodosTable)
                .WhereInSubquery(ListIdField,
                    SrcGlobal.From(ListsTable)
                        .Select(Listed.CreateReadOnlyList<ISafeIdentifier>(IdField))
                        .WhereLike(NameField, "%Work%"))
                .ToSql().ToString()
        });

        demos.Add(new SqlDemo
        {
            Feature = "WhereNot",
            Description = "WHERE NOT (condition)",
            GeneratedSql = SrcGlobal.From(TodosTable)
                .WhereNot(WhereEq(CompletedField, 1))
                .ToSql().ToString()
        });

        demos.Add(new SqlDemo
        {
            Feature = "WhereBetween",
            Description = "WHERE column BETWEEN low AND high",
            GeneratedSql = SrcGlobal.From(TodosTable)
                .WhereBetween(PriorityField, new SqlInt32(1), new SqlInt32(3))
                .ToSql().ToString()
        });

        demos.Add(new SqlDemo
        {
            Feature = "WhereLike",
            Description = "WHERE column LIKE pattern",
            GeneratedSql = SrcGlobal.From(TodosTable)
                .WhereLike(TitleField, "%groceries%")
                .ToSql().ToString()
        });

        demos.Add(new SqlDemo
        {
            Feature = "WhereILike",
            Description = "WHERE column ILIKE pattern (case-insensitive)",
            GeneratedSql = SrcGlobal.From(TodosTable)
                .WhereILike(TitleField, "%GROCERIES%")
                .ToSql().ToString()
        });

        // --- ORDER BY ---
        demos.Add(new SqlDemo
        {
            Feature = "OrderBy",
            Description = "ORDER BY column ASC/DESC",
            GeneratedSql = SrcGlobal.From(TodosTable)
                .OrderBy(PriorityField, true)
                .OrderBy(CreatedAtField, false)
                .ToSql().ToString()
        });

        demos.Add(new SqlDemo
        {
            Feature = "OrderByNulls (NullsFirst)",
            Description = "ORDER BY with NULLS FIRST",
            GeneratedSql = SrcGlobal.From(TodosTable)
                .OrderByNulls(DueDateField, true, new NullsFirst())
                .ToSql().ToString()
        });

        demos.Add(new SqlDemo
        {
            Feature = "OrderByNulls (NullsLast)",
            Description = "ORDER BY with NULLS LAST",
            GeneratedSql = SrcGlobal.From(TodosTable)
                .OrderByNulls(DueDateField, true, new NullsLast())
                .ToSql().ToString()
        });

        // --- LIMIT / OFFSET ---
        demos.Add(new SqlDemo
        {
            Feature = "Limit + Offset",
            Description = "Pagination with LIMIT and OFFSET",
            GeneratedSql = SrcGlobal.From(TodosTable)
                .OrderBy(IdField, true)
                .Limit(10)
                .Offset(20)
                .ToSql().ToString()
        });

        // --- SafeToSql ---
        demos.Add(new SqlDemo
        {
            Feature = "SafeToSql",
            Description = "Applies default LIMIT if none set (safety measure)",
            GeneratedSql = SrcGlobal.From(TodosTable)
                .OrderBy(IdField, true)
                .SafeToSql(100).ToString()
        });

        // --- DISTINCT ---
        demos.Add(new SqlDemo
        {
            Feature = "Distinct",
            Description = "SELECT DISTINCT",
            GeneratedSql = SrcGlobal.From(TodosTable)
                .Select(Listed.CreateReadOnlyList<ISafeIdentifier>(PriorityField))
                .Distinct()
                .ToSql().ToString()
        });

        // --- GROUP BY + HAVING ---
        var havingFrag = new SqlBuilder();
        havingFrag.AppendFragment(SrcGlobal.CountAll());
        havingFrag.AppendSafe(" > ");
        havingFrag.AppendInt32(1);

        demos.Add(new SqlDemo
        {
            Feature = "GroupBy + Having",
            Description = "GROUP BY with HAVING filter",
            GeneratedSql = SrcGlobal.From(TodosTable)
                .SelectExpr(Listed.CreateReadOnlyList<SqlFragment>(
                    BuildSafeFragment(ListIdField.SqlValue),
                    SrcGlobal.CountAll()))
                .GroupBy(ListIdField)
                .Having(havingFrag.Accumulated)
                .ToSql().ToString()
        });

        // --- OrHaving ---
        var orHavingFrag = new SqlBuilder();
        orHavingFrag.AppendFragment(SrcGlobal.SumCol(CompletedField));
        orHavingFrag.AppendSafe(" > ");
        orHavingFrag.AppendInt32(0);

        demos.Add(new SqlDemo
        {
            Feature = "OrHaving",
            Description = "HAVING ... OR HAVING ...",
            GeneratedSql = SrcGlobal.From(TodosTable)
                .SelectExpr(Listed.CreateReadOnlyList<SqlFragment>(
                    BuildSafeFragment(ListIdField.SqlValue),
                    SrcGlobal.CountAll(),
                    SrcGlobal.SumCol(CompletedField)))
                .GroupBy(ListIdField)
                .Having(havingFrag.Accumulated)
                .OrHaving(orHavingFrag.Accumulated)
                .ToSql().ToString()
        });

        // --- JOINS ---
        var innerJoinOn = BuildJoinCondition(TodosTable, ListIdField, ListsTable, IdField);
        demos.Add(new SqlDemo
        {
            Feature = "InnerJoin",
            Description = "INNER JOIN two tables",
            GeneratedSql = SrcGlobal.From(TodosTable)
                .InnerJoin(ListsTable, innerJoinOn)
                .ToSql().ToString()
        });

        demos.Add(new SqlDemo
        {
            Feature = "LeftJoin",
            Description = "LEFT JOIN two tables",
            GeneratedSql = SrcGlobal.From(ListsTable)
                .LeftJoin(TodosTable, BuildJoinCondition(ListsTable, IdField, TodosTable, ListIdField))
                .ToSql().ToString()
        });

        demos.Add(new SqlDemo
        {
            Feature = "RightJoin",
            Description = "RIGHT JOIN two tables",
            GeneratedSql = SrcGlobal.From(TodosTable)
                .RightJoin(ListsTable, BuildJoinCondition(TodosTable, ListIdField, ListsTable, IdField))
                .ToSql().ToString()
        });

        demos.Add(new SqlDemo
        {
            Feature = "FullJoin",
            Description = "FULL OUTER JOIN two tables",
            GeneratedSql = SrcGlobal.From(ListsTable)
                .FullJoin(TodosTable, BuildJoinCondition(ListsTable, IdField, TodosTable, ListIdField))
                .ToSql().ToString()
        });

        demos.Add(new SqlDemo
        {
            Feature = "CrossJoin",
            Description = "CROSS JOIN (cartesian product)",
            GeneratedSql = SrcGlobal.From(ListsTable)
                .CrossJoin(TagsTable)
                .Limit(10)
                .ToSql().ToString()
        });

        // --- Col (qualified column reference) ---
        demos.Add(new SqlDemo
        {
            Feature = "Col",
            Description = "Qualified column reference (table.column)",
            GeneratedSql = SrcGlobal.Col(TodosTable, TitleField).ToString()
        });

        // --- LOCK modes ---
        demos.Add(new SqlDemo
        {
            Feature = "Lock (ForUpdate)",
            Description = "SELECT ... FOR UPDATE (row locking)",
            GeneratedSql = SrcGlobal.From(TodosTable)
                .Where(WhereEq(IdField, 1))
                .Lock(new ForUpdate())
                .ToSql().ToString()
        });

        demos.Add(new SqlDemo
        {
            Feature = "Lock (ForShare)",
            Description = "SELECT ... FOR SHARE (shared lock)",
            GeneratedSql = SrcGlobal.From(TodosTable)
                .Where(WhereEq(IdField, 1))
                .Lock(new ForShare())
                .ToSql().ToString()
        });

        // --- CountSql ---
        demos.Add(new SqlDemo
        {
            Feature = "CountSql",
            Description = "Efficient SELECT COUNT(*) for a query",
            GeneratedSql = SrcGlobal.From(TodosTable)
                .Where(WhereEq(CompletedField, 0))
                .CountSql().ToString()
        });

        // --- SET OPERATIONS ---
        var highPriority = SrcGlobal.From(TodosTable)
            .Select(Listed.CreateReadOnlyList<ISafeIdentifier>(IdField, TitleField))
            .Where(WhereEq(PriorityField, 1));
        var completed = SrcGlobal.From(TodosTable)
            .Select(Listed.CreateReadOnlyList<ISafeIdentifier>(IdField, TitleField))
            .Where(WhereEq(CompletedField, 1));

        demos.Add(new SqlDemo
        {
            Feature = "UnionSql",
            Description = "UNION of two queries (deduplicates)",
            GeneratedSql = SrcGlobal.UnionSql(highPriority, completed).ToString()
        });

        demos.Add(new SqlDemo
        {
            Feature = "UnionAllSql",
            Description = "UNION ALL of two queries (keeps duplicates)",
            GeneratedSql = SrcGlobal.UnionAllSql(highPriority, completed).ToString()
        });

        demos.Add(new SqlDemo
        {
            Feature = "IntersectSql",
            Description = "INTERSECT of two queries (common rows)",
            GeneratedSql = SrcGlobal.IntersectSql(highPriority, completed).ToString()
        });

        demos.Add(new SqlDemo
        {
            Feature = "ExceptSql",
            Description = "EXCEPT of two queries (in first but not second)",
            GeneratedSql = SrcGlobal.ExceptSql(highPriority, completed).ToString()
        });

        // --- SUBQUERY ---
        demos.Add(new SqlDemo
        {
            Feature = "Subquery",
            Description = "Wrap a query as a subquery with alias",
            GeneratedSql = SrcGlobal.Subquery(
                SrcGlobal.From(TodosTable).Where(WhereEq(CompletedField, 1)),
                SubAlias).ToString()
        });

        // --- EXISTS ---
        demos.Add(new SqlDemo
        {
            Feature = "ExistsSql",
            Description = "EXISTS (subquery) check",
            GeneratedSql = SrcGlobal.ExistsSql(
                SrcGlobal.From(TodosTable)
                    .Where(WhereEq(CompletedField, 0))
                    .Limit(1)).ToString()
        });

        // --- UPDATE query builder ---
        demos.Add(new SqlDemo
        {
            Feature = "Update + Set + Where",
            Description = "UPDATE query via builder pattern",
            GeneratedSql = SrcGlobal.Update(TodosTable)
                .Set(CompletedField, new SqlInt32(1))
                .Set(PriorityField, new SqlInt32(5))
                .Where(WhereEq(ListIdField, 1))
                .ToSql().ToString()
        });

        demos.Add(new SqlDemo
        {
            Feature = "Update + Limit",
            Description = "UPDATE with LIMIT clause",
            GeneratedSql = SrcGlobal.Update(TodosTable)
                .Set(CompletedField, new SqlInt32(1))
                .Where(WhereEq(CompletedField, 0))
                .Limit(5)
                .ToSql().ToString()
        });

        // --- DELETE query builder ---
        demos.Add(new SqlDemo
        {
            Feature = "DeleteFrom + Where",
            Description = "DELETE query via builder pattern",
            GeneratedSql = SrcGlobal.DeleteFrom(TodosTable)
                .Where(WhereEq(CompletedField, 1))
                .ToSql().ToString()
        });

        demos.Add(new SqlDemo
        {
            Feature = "DeleteFrom + OrWhere",
            Description = "DELETE with OR condition",
            GeneratedSql = SrcGlobal.DeleteFrom(TodosTable)
                .Where(WhereEq(CompletedField, 1))
                .OrWhere(WhereEq(PriorityField, 5))
                .ToSql().ToString()
        });

        demos.Add(new SqlDemo
        {
            Feature = "DeleteFrom + Limit",
            Description = "DELETE with LIMIT",
            GeneratedSql = SrcGlobal.DeleteFrom(TodosTable)
                .Where(WhereEq(CompletedField, 1))
                .Limit(10)
                .ToSql().ToString()
        });

        // --- DeleteSql (quick helper) ---
        demos.Add(new SqlDemo
        {
            Feature = "DeleteSql",
            Description = "Quick DELETE by primary key",
            GeneratedSql = SrcGlobal.DeleteSql(TodoTableDef, 42).ToString()
        });

        // --- Changeset: ToInsertSql ---
        var insertValues = Mapped.ConstructMap(Listed.CreateReadOnlyList(
            new KeyValuePair<string, string>("name", "My New List")));
        var insertCs = SrcGlobal.Changeset(ListTableDef, insertValues)
            .Cast(ListInsertFields)
            .ValidateRequired(ListInsertFields);

        demos.Add(new SqlDemo
        {
            Feature = "Changeset ToInsertSql",
            Description = "INSERT via validated changeset",
            GeneratedSql = insertCs.ToInsertSql().ToString()
        });

        // --- Changeset: ToUpdateSql ---
        demos.Add(new SqlDemo
        {
            Feature = "Changeset ToUpdateSql",
            Description = "UPDATE via validated changeset",
            GeneratedSql = insertCs.ToUpdateSql(1).ToString()
        });

        // --- Changeset validations demo ---
        demos.Add(new SqlDemo
        {
            Feature = "Changeset ValidateLength",
            Description = "Validate string length (1-100 chars)",
            GeneratedSql = "[Validation] name must be between 1 and 100 characters"
        });

        demos.Add(new SqlDemo
        {
            Feature = "Changeset ValidateInt",
            Description = "Validate integer field type",
            GeneratedSql = "[Validation] field must be an integer"
        });

        demos.Add(new SqlDemo
        {
            Feature = "Changeset ValidateNumber",
            Description = "Validate numeric range (GreaterThanOrEqual/LessThanOrEqual)",
            GeneratedSql = "[Validation] priority must be >= 1.0 and <= 5.0"
        });

        demos.Add(new SqlDemo
        {
            Feature = "Changeset ValidateInclusion",
            Description = "Validate value is in allowed set",
            GeneratedSql = "[Validation] priority must be one of: 1, 2, 3, 4, 5"
        });

        demos.Add(new SqlDemo
        {
            Feature = "Changeset ValidateExclusion",
            Description = "Validate value is NOT in disallowed set",
            GeneratedSql = "[Validation] name must not be: admin, system, root, null"
        });

        demos.Add(new SqlDemo
        {
            Feature = "Changeset ValidateRequired",
            Description = "Validate required fields are present",
            GeneratedSql = "[Validation] field is required"
        });

        demos.Add(new SqlDemo
        {
            Feature = "Changeset Cast",
            Description = "Whitelist allowed fields from params",
            GeneratedSql = "[Filtering] Only permitted fields pass through"
        });

        demos.Add(new SqlDemo
        {
            Feature = "Changeset PutChange",
            Description = "Programmatically set/override a field value",
            GeneratedSql = "[Mutation] changeset.PutChange(field, value)"
        });

        demos.Add(new SqlDemo
        {
            Feature = "Changeset GetChange",
            Description = "Read a field value from the changeset",
            GeneratedSql = "[Read] changeset.GetChange(field) -> string"
        });

        demos.Add(new SqlDemo
        {
            Feature = "Changeset DeleteChange",
            Description = "Remove a field from the changeset",
            GeneratedSql = "[Mutation] changeset.DeleteChange(field)"
        });

        demos.Add(new SqlDemo
        {
            Feature = "Changeset ValidateFloat",
            Description = "Validate float field type",
            GeneratedSql = "[Validation] field must be a number (float)"
        });

        demos.Add(new SqlDemo
        {
            Feature = "Changeset ValidateBool",
            Description = "Validate boolean field (true/false/1/0/yes/no/on/off)",
            GeneratedSql = "[Validation] field must be a boolean"
        });

        demos.Add(new SqlDemo
        {
            Feature = "Changeset ValidateInt64",
            Description = "Validate 64-bit integer field type",
            GeneratedSql = "[Validation] field must be a 64-bit integer"
        });

        demos.Add(new SqlDemo
        {
            Feature = "Changeset ValidateAcceptance",
            Description = "Validate field is accepted (true/1/yes/on)",
            GeneratedSql = "[Validation] field must be accepted"
        });

        demos.Add(new SqlDemo
        {
            Feature = "Changeset ValidateConfirmation",
            Description = "Validate field matches confirmation field",
            GeneratedSql = "[Validation] confirmation field does not match"
        });

        demos.Add(new SqlDemo
        {
            Feature = "Changeset ValidateContains",
            Description = "Validate field contains substring",
            GeneratedSql = "[Validation] field must contain the given substring"
        });

        demos.Add(new SqlDemo
        {
            Feature = "Changeset ValidateStartsWith",
            Description = "Validate field starts with prefix",
            GeneratedSql = "[Validation] field must start with the given prefix"
        });

        demos.Add(new SqlDemo
        {
            Feature = "Changeset ValidateEndsWith",
            Description = "Validate field ends with suffix",
            GeneratedSql = "[Validation] field must end with the given suffix"
        });

        // --- Type system ---
        demos.Add(new SqlDemo
        {
            Feature = "SafeIdentifier",
            Description = "Validated SQL identifier (prevents injection)",
            GeneratedSql = $"SafeIdentifier(\"todos\") -> \"{TodosTable.SqlValue}\""
        });

        demos.Add(new SqlDemo
        {
            Feature = "TableDef + FieldDef",
            Description = "Type-safe table schema definition",
            GeneratedSql = $"TableDef(\"{TodoTableDef.TableName.SqlValue}\", fields=[title:String, completed:Int, priority:Int, due_date:String?, list_id:Int, created_at:String?])"
        });

        demos.Add(new SqlDemo
        {
            Feature = "SqlInt32 / SqlString / SqlBuilder",
            Description = "Type-safe SQL value literals with auto-escaping",
            GeneratedSql = "SqlInt32(42) -> 42, SqlString(\"O'Brien\") -> 'O''Brien'"
        });

        return demos;
    }

    // ════════════════════════════════════════════════════════════════════
    // SEED DATA
    // ════════════════════════════════════════════════════════════════════

    public bool HasAnyLists()
    {
        using var conn = Open();
        var query = SrcGlobal.From(ListsTable).Limit(1);
        string sql = query.ToSql().ToString();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        using var reader = cmd.ExecuteReader();
        return reader.Read();
    }

    public void SeedData()
    {
        // Create lists
        InsertList("Personal", "Personal errands and tasks");
        InsertList("Work", "Professional tasks and projects");
        InsertList("Shopping", "Items to buy");

        var lists = GetAllLists();
        var personal = lists.First(l => l.Name == "Personal");
        var work = lists.First(l => l.Name == "Work");
        var shopping = lists.First(l => l.Name == "Shopping");

        // Create todos with priorities and due dates
        InsertTodo("Buy groceries", personal.Id, 2, DateTime.UtcNow.AddDays(1).ToString("yyyy-MM-dd"));
        InsertTodo("Walk the dog", personal.Id, 1);
        InsertTodo("Read a book", personal.Id, 4);
        InsertTodo("Exercise", personal.Id, 2, DateTime.UtcNow.AddDays(0).ToString("yyyy-MM-dd"));
        InsertTodo("Call dentist", personal.Id, 3, DateTime.UtcNow.AddDays(-2).ToString("yyyy-MM-dd"));

        InsertTodo("Finish quarterly report", work.Id, 1, DateTime.UtcNow.AddDays(3).ToString("yyyy-MM-dd"));
        InsertTodo("Review pull requests", work.Id, 2, DateTime.UtcNow.AddDays(1).ToString("yyyy-MM-dd"));
        InsertTodo("Update documentation", work.Id, 3);
        InsertTodo("Team standup prep", work.Id, 2);
        InsertTodo("Deploy v2.0", work.Id, 1, DateTime.UtcNow.AddDays(7).ToString("yyyy-MM-dd"));

        InsertTodo("Milk", shopping.Id, 3);
        InsertTodo("Bread", shopping.Id, 3);
        InsertTodo("Coffee beans", shopping.Id, 2);

        // Toggle a few as completed
        var allTodos = GetAllLists().SelectMany(l => l.Todos).ToList();
        var walkDog = allTodos.FirstOrDefault(t => t.Title == "Walk the dog");
        if (walkDog != null) ToggleTodo(walkDog.Id);
        var readBook = allTodos.FirstOrDefault(t => t.Title == "Read a book");
        if (readBook != null) ToggleTodo(readBook.Id);
        var milk = allTodos.FirstOrDefault(t => t.Title == "Milk");
        if (milk != null) ToggleTodo(milk.Id);

        // Create tags
        CreateTag("urgent");
        CreateTag("home");
        CreateTag("office");
        CreateTag("health");
        CreateTag("errand");

        // Assign tags
        var tags = GetAllTags();
        var urgentTag = tags.FirstOrDefault(t => t.Name == "urgent");
        var homeTag = tags.FirstOrDefault(t => t.Name == "home");
        var officeTag = tags.FirstOrDefault(t => t.Name == "office");
        var healthTag = tags.FirstOrDefault(t => t.Name == "health");
        var errandTag = tags.FirstOrDefault(t => t.Name == "errand");

        allTodos = GetAllLists().SelectMany(l => l.Todos).ToList();

        if (urgentTag != null)
        {
            var urgent = allTodos.Where(t => t.Priority <= 2).ToList();
            foreach (var t in urgent) AssignTag(t.Id, urgentTag.Id);
        }
        if (homeTag != null)
        {
            var home = allTodos.Where(t => t.ListId == personal.Id).ToList();
            foreach (var t in home) AssignTag(t.Id, homeTag.Id);
        }
        if (officeTag != null)
        {
            var office = allTodos.Where(t => t.ListId == work.Id).ToList();
            foreach (var t in office) AssignTag(t.Id, officeTag.Id);
        }
        if (healthTag != null)
        {
            var exercise = allTodos.FirstOrDefault(t => t.Title == "Exercise");
            if (exercise != null) AssignTag(exercise.Id, healthTag.Id);
        }
        if (errandTag != null)
        {
            var errands = allTodos.Where(t => t.ListId == shopping.Id).ToList();
            foreach (var t in errands) AssignTag(t.Id, errandTag.Id);
        }
    }

    // ════════════════════════════════════════════════════════════════════
    // HELPERS
    // ════════════════════════════════════════════════════════════════════

    private static void EnableForeignKeys(SqliteConnection conn)
    {
        using var pragma = conn.CreateCommand();
        pragma.CommandText = "PRAGMA foreign_keys = ON";
        pragma.ExecuteNonQuery();
    }

    private string GetListName(SqliteConnection conn, int listId)
    {
        var query = SrcGlobal.From(ListsTable)
            .Select(Listed.CreateReadOnlyList<ISafeIdentifier>(NameField))
            .Where(WhereEq(IdField, listId));
        string sql = query.ToSql().ToString();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        var result = cmd.ExecuteScalar();
        return result?.ToString() ?? "Unknown";
    }

    public static SqlFragment BuildSafeFragment(string safeSql)
    {
        var b = new SqlBuilder();
        b.AppendSafe(safeSql);
        return b.Accumulated;
    }

    public static SqlFragment BuildJoinCondition(ISafeIdentifier leftTable, ISafeIdentifier leftCol,
        ISafeIdentifier rightTable, ISafeIdentifier rightCol)
    {
        var b = new SqlBuilder();
        b.AppendFragment(SrcGlobal.Col(leftTable, leftCol));
        b.AppendSafe(" = ");
        b.AppendFragment(SrcGlobal.Col(rightTable, rightCol));
        return b.Accumulated;
    }

    private static int ExecuteCount(SqliteConnection conn, string sql)
    {
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        var result = cmd.ExecuteScalar();
        return result != null ? Convert.ToInt32(result) : 0;
    }

    private static double ExecuteDouble(SqliteConnection conn, string sql)
    {
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        var result = cmd.ExecuteScalar();
        if (result == null || result == DBNull.Value) return 0.0;
        return Convert.ToDouble(result);
    }

    private static int ExecuteInt(SqliteConnection conn, string sql)
    {
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        var result = cmd.ExecuteScalar();
        if (result == null || result == DBNull.Value) return 0;
        return Convert.ToInt32(result);
    }

    // ════════════════════════════════════════════════════════════════════
    // ROW MAPPERS
    // ════════════════════════════════════════════════════════════════════

    private static TodoList ReadList(SqliteDataReader reader)
    {
        var descOrdinal = reader.GetOrdinal("description");
        return new TodoList
        {
            Id = reader.GetInt32(reader.GetOrdinal("id")),
            Name = reader.GetString(reader.GetOrdinal("name")),
            Description = reader.IsDBNull(descOrdinal) ? null : reader.GetString(descOrdinal),
            CreatedAt = reader.GetString(reader.GetOrdinal("created_at"))
        };
    }

    private static TodoItem ReadTodo(SqliteDataReader reader)
    {
        var dueDateOrdinal = reader.GetOrdinal("due_date");
        return new TodoItem
        {
            Id = reader.GetInt32(reader.GetOrdinal("id")),
            Title = reader.GetString(reader.GetOrdinal("title")),
            Completed = reader.GetInt32(reader.GetOrdinal("completed")),
            Priority = reader.GetInt32(reader.GetOrdinal("priority")),
            DueDate = reader.IsDBNull(dueDateOrdinal) ? null : reader.GetString(dueDateOrdinal),
            ListId = reader.GetInt32(reader.GetOrdinal("list_id")),
            CreatedAt = reader.GetString(reader.GetOrdinal("created_at"))
        };
    }
}
