using Serilog;

var builder = WebApplication.CreateBuilder(args);

// -- Serilog: read from configuration and write to console
builder.Host.UseSerilog((ctx, services, logConfig) =>
{
    logConfig
        .ReadFrom.Configuration(ctx.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console();
});

// Swagger (already present in the template, but keep it explicit)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Serilog request logging (nice, structured per-request logs)
app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Simple in-memory store
var todos = new List<TodoItem>();
var nextId = 1;

// Convenience: land on Swagger
app.MapGet("/", () => Results.Redirect("/swagger"));

// GET /todos
app.MapGet("/todos", () => Results.Ok(todos));

// GET /todos/{id}
app.MapGet("/todos/{id:int}", (int id) =>
{
    var item = todos.Find(t => t.Id == id);
    return item is null ? Results.NotFound() : Results.Ok(item);
});

// POST /todos
app.MapPost("/todos", (CreateTodoDto dto) =>
{
    if (string.IsNullOrWhiteSpace(dto.Title))
        return Results.BadRequest(new { error = "Title is required." });

    var todo = new TodoItem(nextId++, dto.Title.Trim(), false);
    todos.Add(todo);

    Log.Information("Created todo {@Todo}", todo);
    return Results.Created($"/todos/{todo.Id}", todo);
});

// PUT /todos/{id}
app.MapPut("/todos/{id:int}", (int id, UpdateTodoDto dto) =>
{
    var idx = todos.FindIndex(t => t.Id == id);
    if (idx == -1) return Results.NotFound();

    var current = todos[idx];
    var updated = current with
    {
        Title = string.IsNullOrWhiteSpace(dto.Title) ? current.Title : dto.Title!.Trim(),
        IsDone = dto.IsDone ?? current.IsDone
    };

    todos[idx] = updated;
    Log.Information("Updated todo {Id} -> {@Todo}", id, updated);
    return Results.Ok(updated);
});

// DELETE /todos/{id}
app.MapDelete("/todos/{id:int}", (int id) =>
{
    var idx = todos.FindIndex(t => t.Id == id);
    if (idx == -1) return Results.NotFound();

    var removed = todos[idx];
    todos.RemoveAt(idx);
    Log.Information("Deleted todo {Id}", id);
    return Results.NoContent();
});

app.Run();

// Models/DTOs
public record TodoItem(int Id, string Title, bool IsDone);
public record CreateTodoDto(string Title);
public record UpdateTodoDto(string? Title, bool? IsDone);
