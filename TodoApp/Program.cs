using TodoApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Configure to listen on port 5002
builder.WebHost.UseUrls("http://localhost:5002");

// Add services
builder.Services.AddRazorPages();
builder.Services.AddSingleton(new TodoDb("Data Source=todos.db"));

var app = builder.Build();

// Auto-create tables and seed data
var db = app.Services.GetRequiredService<TodoDb>();
db.EnsureCreated();

if (!db.HasAnyLists())
{
    db.SeedData();
}

// Configure pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
