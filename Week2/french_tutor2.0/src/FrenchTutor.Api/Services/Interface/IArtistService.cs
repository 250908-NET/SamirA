using FrenchTutor.Api.Models;

namespace FrenchTutor.Api.Services.Interface;

public interface IArtistService
{
    Task<List<Artist>> GetAllAsync();
    Task<Artist?> GetByIdAsync(int id);
    Task CreateAsync(Artist artist);
}

public interface ISongService
{
    Task<List<Song>> GetAllAsync();
    Task<Song?> GetByIdAsync(int id);
    Task CreateAsync(Song song);
}

public interface ITermService
{
    Task<List<Term>> GetAllAsync();
    Task<Term?> GetByIdAsync(int id);
    Task CreateAsync(Term term);
}
