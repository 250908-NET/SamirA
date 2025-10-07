using FrenchTutor.Api.Models;
using FrenchTutor.Api.Repository.Interface;
using FrenchTutor.Api.Services.Interface;

namespace FrenchTutor.Api.Services.Implementation;

public class ArtistService : IArtistService
{
    private readonly IArtistRepository _repo;
    public ArtistService(IArtistRepository repo) => _repo = repo;

    public Task<List<Artist>> GetAllAsync() => _repo.GetAllAsync();
    public Task<Artist?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

    public async Task CreateAsync(Artist artist)
    {
        await _repo.AddAsync(artist);
        await _repo.SaveChangesAsync();
    }
}

public class SongService : ISongService
{
    private readonly ISongRepository _repo;
    public SongService(ISongRepository repo) => _repo = repo;

    public Task<List<Song>> GetAllAsync() => _repo.GetAllAsync();
    public Task<Song?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

    public async Task CreateAsync(Song song)
    {
        await _repo.AddAsync(song);
        await _repo.SaveChangesAsync();
    }
}

public class TermService : ITermService
{
    private readonly ITermRepository _repo;
    public TermService(ITermRepository repo) => _repo = repo;

    public Task<List<Term>> GetAllAsync() => _repo.GetAllAsync();
    public Task<Term?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

    public async Task CreateAsync(Term term)
    {
        await _repo.AddAsync(term);
        await _repo.SaveChangesAsync();
    }
}
