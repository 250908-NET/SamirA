// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.
// // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi();
// }

// app.UseHttpsRedirection();

// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast");

// app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }

using Api.Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
builder.Host.UseSerilog();

// EF Core
builder.Services.AddDbContext<FrenchTutorDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

// Swagger (classic & reliable)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

// Minimal endpoints

// Create Song (POST)
app.MapPost("/songs", async (SongCreateDto dto, FrenchTutorDbContext db) =>
{
    var artist = await db.Artists.FindAsync(dto.ArtistId);
    if (artist is null) return Results.BadRequest($"Artist {dto.ArtistId} not found.");

    var song = new Song
    {
        Title = dto.Title,
        Year = dto.Year,
        ArtistId = dto.ArtistId,
        LyricsFr = dto.LyricsFr,
        TranslationEn = dto.TranslationEn
    };

    db.Songs.Add(song);
    await db.SaveChangesAsync();

    var read = new SongReadDto(song.Id, song.Title, song.Year, artist.Name);
    return Results.Created($"/songs/{song.Id}", read);
})
.WithName("CreateSong")
.Produces<SongReadDto>(201)
.ProducesProblem(400);

// Get Songs (GET)
app.MapGet("/songs", async (FrenchTutorDbContext db) =>
{
    var items = await db.Songs
        .Include(s => s.Artist)
        .Select(s => new SongReadDto(s.Id, s.Title, s.Year, s.Artist.Name))
        .ToListAsync();

    return Results.Ok(items);
})
.WithName("ListSongs")
.Produces<List<SongReadDto>>(200);

// Get Song by Id with translation (GET)
app.MapGet("/songs/{id:int}", async (int id, FrenchTutorDbContext db) =>
{
    var s = await db.Songs.Include(x => x.Artist).FirstOrDefaultAsync(x => x.Id == id);
    return s is null ? Results.NotFound() : Results.Ok(new
    {
        s.Id,
        s.Title,
        s.Year,
        Artist = s.Artist.Name,
        s.LyricsFr,
        s.TranslationEn
    });
})
.WithName("GetSong")
.Produces(200)
.Produces(404);

// Delete Song (DELETE)
app.MapDelete("/songs/{id:int}", async (int id, FrenchTutorDbContext db) =>
{
    var s = await db.Songs.FindAsync(id);
    if (s is null) return Results.NotFound();

    db.Songs.Remove(s);
    await db.SaveChangesAsync();
    return Results.NoContent();
})
.WithName("DeleteSong")
.Produces(204)
.Produces(404);

// Expressions (basic CRUD)
app.MapPost("/expressions", async (ExpressionDto dto, FrenchTutorDbContext db) =>
{
    var e = new Expression { Phrase = dto.Phrase, Meaning = dto.Meaning };
    db.Expressions.Add(e);
    await db.SaveChangesAsync();
    return Results.Created($"/expressions/{e.Id}", new ExpressionDto(e.Id, e.Phrase, e.Meaning));
});

app.MapGet("/expressions", async (FrenchTutorDbContext db) =>
    Results.Ok(await db.Expressions.Select(e => new ExpressionDto(e.Id, e.Phrase, e.Meaning)).ToListAsync()));

app.MapPost("/songs/{id:int}/expressions/{exprId:int}", async (int id, int exprId, FrenchTutorDbContext db) =>
{
    var s = await db.Songs.Include(x => x.Expressions).FirstOrDefaultAsync(x => x.Id == id);
    var e = await db.Expressions.FindAsync(exprId);
    if (s is null || e is null) return Results.NotFound();
    s.Expressions.Add(e);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// Very simple “lookup” by substring
app.MapGet("/songs/{id:int}/lookup", async (int id, string term, FrenchTutorDbContext db) =>
{
    var s = await db.Songs.Include(x => x.Expressions).FirstOrDefaultAsync(x => x.Id == id);
    if (s is null) return Results.NotFound();

    var hits = s.Expressions
        .Where(e => s.LyricsFr.Contains(e.Phrase, StringComparison.OrdinalIgnoreCase) ||
                    e.Phrase.Contains(term, StringComparison.OrdinalIgnoreCase))
        .Select(e => new { e.Phrase, e.Meaning });

    return Results.Ok(hits);
})
.WithDescription("Find expressions present in the song or matching a search term.");

app.Run();
