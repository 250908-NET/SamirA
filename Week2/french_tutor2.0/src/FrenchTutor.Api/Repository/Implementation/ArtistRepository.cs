using Microsoft.EntityFrameworkCore;
using FrenchTutor.Api.Data;
using FrenchTutor.Api.Models;
using FrenchTutor.Api.Repository.Interface;

namespace FrenchTutor.Api.Repository.Implementation;

public class ArtistRepository : IArtistRepository
{
    private readonly FrenchTutorDbContext _db;
    public ArtistRepository(FrenchTutorDbContext db) => _db = db;

    public Task<List<Artist>> GetAllAsync() => _db.Artists.AsNoTracking().ToListAsync();
    public Task<Artist?> GetByIdAsync(int id) => _db.Artists.Include(a => a.Songs).FirstOrDefaultAsync(a => a.Id == id);
    public async Task AddAsync(Artist artist) => await _db.Artists.AddAsync(artist);
    public Task SaveChangesAsync() => _db.SaveChangesAsync();
}

public class SongRepository : ISongRepository
{
    private readonly FrenchTutorDbContext _db;
    public SongRepository(FrenchTutorDbContext db) => _db = db;

    public Task<List<Song>> GetAllAsync() => _db.Songs.AsNoTracking().Include(s => s.Artist).ToListAsync();
    public Task<Song?> GetByIdAsync(int id) => _db.Songs.Include(s => s.Artist).Include(s => s.Terms).FirstOrDefaultAsync(s => s.Id == id);
    public async Task AddAsync(Song song) => await _db.Songs.AddAsync(song);
    public Task SaveChangesAsync() => _db.SaveChangesAsync();
}

public class TermRepository : ITermRepository
{
    private readonly FrenchTutorDbContext _db;
    public TermRepository(FrenchTutorDbContext db) => _db = db;

    public Task<List<Term>> GetAllAsync() => _db.Terms.AsNoTracking().ToListAsync();
    public Task<Term?> GetByIdAsync(int id) => _db.Terms.Include(t => t.Songs).FirstOrDefaultAsync(t => t.Id == id);
    public async Task AddAsync(Term term) => await _db.Terms.AddAsync(term);
    public Task SaveChangesAsync() => _db.SaveChangesAsync();
}
