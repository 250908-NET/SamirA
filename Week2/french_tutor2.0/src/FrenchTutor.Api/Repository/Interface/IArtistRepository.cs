using FrenchTutor.Api.Models;

namespace FrenchTutor.Api.Repository.Interface;

public interface IArtistRepository
{
    Task<List<Artist>> GetAllAsync();
    Task<Artist?> GetByIdAsync(int id);
    Task AddAsync(Artist artist);
    Task SaveChangesAsync();
}

public interface ISongRepository
{
    Task<List<Song>> GetAllAsync();
    Task<Song?> GetByIdAsync(int id);
    Task AddAsync(Song song);
    Task SaveChangesAsync();
}

public interface ITermRepository
{
    Task<List<Term>> GetAllAsync();
    Task<Term?> GetByIdAsync(int id);
    Task AddAsync(Term term);
    Task SaveChangesAsync();
}
