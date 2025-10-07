using Microsoft.EntityFrameworkCore;
using Serilog;
using FrenchTutor.Api.Data;
using FrenchTutor.Api.Models;
using FrenchTutor.Api.Repository.Interface;
using FrenchTutor.Api.Repository.Implementation;
using FrenchTutor.Api.Services.Interface;
using FrenchTutor.Api.Services.Implementation;

var builder = WebApplication.CreateBuilder(args);

// ---------- Connection string (ENV first, then file fallbacks) ----------
string? cs = Environment.GetEnvironmentVariable("CONNECTION_STRING");
if (string.IsNullOrWhiteSpace(cs))
{
    var candidates = new[]
    {
        "../../connection_string.env",  // when content root is src/FrenchTutor.Api
        "../connection_string.env",    // when content root is src/
        "connection_string.env"        // last resort (project root)
    };
    foreach (var path in candidates)
    {
        if (File.Exists(path)) { cs = File.ReadAllText(path); break; }
    }
}
if (string.IsNullOrWhiteSpace(cs))
    throw new FileNotFoundException("CONNECTION_STRING env var not set and connection_string.env not found");

// ---------- Services ----------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FrenchTutorDbContext>(opts => opts.UseSqlServer(cs));

// Repositories
builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
builder.Services.AddScoped<ISongRepository,   SongRepository>();
builder.Services.AddScoped<ITermRepository,   TermRepository>();

// Services
builder.Services.AddScoped<IArtistService, ArtistService>();
builder.Services.AddScoped<ISongService,   SongService>();
builder.Services.AddScoped<ITermService,   TermService>();

// ---------- Serilog ----------
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
builder.Host.UseSerilog();

var app = builder.Build();

// ---------- Migrate + Seed ----------
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<FrenchTutorDbContext>();
    db.Database.Migrate();
    await SeedData.SeedAsync(db);   // make sure you have Data/SeedData.cs
}

// ---------- Pipeline ----------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Bienvenue à french_tutor2.0 — open /swagger");

// ======================================================================
// ARTISTS (projected GETs to avoid cycles; POST via service)
// ======================================================================
app.MapGet("/artists", async (FrenchTutorDbContext db) =>
{
    var list = await db.Artists
        .AsNoTracking()
        .Select(a => new
        {
            a.Id, a.Name, a.Country, a.Bio,
            SongCount = a.Songs.Count
        })
        .ToListAsync();

    return Results.Ok(list);
});

app.MapGet("/artists/{id:int}", async (int id, FrenchTutorDbContext db) =>
{
    var dto = await db.Artists
        .AsNoTracking()
        .Where(a => a.Id == id)
        .Select(a => new
        {
            a.Id, a.Name, a.Country, a.Bio,
            Songs = a.Songs.Select(s => new { s.Id, s.Title, s.Year }).ToList()
        })
        .FirstOrDefaultAsync();

    return dto is null ? Results.NotFound() : Results.Ok(dto);
});

app.MapPost("/artists", async (Artist a, IArtistService svc) =>
{
    await svc.CreateAsync(a);
    return Results.Created($"/artists/{a.Id}", new { a.Id, a.Name, a.Country, a.Bio });
});

// ======================================================================
// SONGS (DTO projections — Option C)
// ======================================================================
var songs = app.MapGroup("/songs");

// list (lightweight)
songs.MapGet("/", async (FrenchTutorDbContext db) =>
{
    var list = await db.Songs
        .AsNoTracking()
        .Include(s => s.Artist)
        .Select(s => new SongListDto(
            s.Id,
            s.Title,
            s.Year,
            s.ArtistId,
            s.Artist.Name
        ))
        .ToListAsync();

    return Results.Ok(list);
});

// detail (with Artist + Terms)
songs.MapGet("/{id:int}", async (int id, FrenchTutorDbContext db) =>
{
    var dto = await db.Songs
        .AsNoTracking()
        .Where(s => s.Id == id)
        .Include(s => s.Artist)
        .Include(s => s.Terms)
        .Select(s => new SongDetailDto(
            s.Id,
            s.Title,
            s.Year,
            s.Lyrics,
            s.Translation,
            new ArtistBriefDto(s.Artist.Id, s.Artist.Name, s.Artist.Country),
            s.Terms.Select(t => new TermBriefDto(t.Id, t.French, t.English)).ToList()
        ))
        .FirstOrDefaultAsync();

    return dto is null ? Results.NotFound() : Results.Ok(dto);
});

// create (returns the lightweight DTO)
songs.MapPost("/", async (Song input, FrenchTutorDbContext db) =>
{
    db.Songs.Add(input);
    await db.SaveChangesAsync();

    var dto = await db.Songs
        .AsNoTracking()
        .Where(s => s.Id == input.Id)
        .Include(s => s.Artist)
        .Select(s => new SongListDto(
            s.Id, s.Title, s.Year, s.ArtistId, s.Artist.Name
        ))
        .FirstAsync();

    return Results.Created($"/songs/{input.Id}", dto);
});

// ======================================================================
// TERMS (projected GETs; POST via service)
// ======================================================================
app.MapGet("/terms", async (FrenchTutorDbContext db) =>
{
    var list = await db.Terms
        .AsNoTracking()
        .Select(t => new { t.Id, t.French, t.English, t.Notes })
        .ToListAsync();

    return Results.Ok(list);
});

app.MapGet("/terms/{id:int}", async (int id, FrenchTutorDbContext db) =>
{
    var dto = await db.Terms
        .AsNoTracking()
        .Where(t => t.Id == id)
        .Select(t => new
        {
            t.Id, t.French, t.English, t.Notes,
            Songs = t.Songs.Select(s => new { s.Id, s.Title, s.Year }).ToList()
        })
        .FirstOrDefaultAsync();

    return dto is null ? Results.NotFound() : Results.Ok(dto);
});

app.MapPost("/terms", async (Term t, ITermService svc) =>
{
    await svc.CreateAsync(t);
    return Results.Created($"/terms/{t.Id}", new { t.Id, t.French, t.English, t.Notes });
});

app.Run();


// ========================== DTOs (records) ==========================
internal record SongListDto(int Id, string Title, int? Year, int ArtistId, string ArtistName);

internal record ArtistBriefDto(int Id, string Name, string? Country);
internal record TermBriefDto(int Id, string French, string English);

internal record SongDetailDto(
    int Id,
    string Title,
    int? Year,
    string? Lyrics,
    string? Translation,
    ArtistBriefDto Artist,
    List<TermBriefDto> Terms
);
