using School.Models;

namespace School.Repositories
{
    public interface IInstructorRepository
    {
        public Task<List<Instructor>> GetAllAsync();
        public Task<Instructor?> GetByIdAsync(string id);
        public Task AddAsync(Instructor instructor);
        public Task UpdateAsync(string id, Instructor instructor);
        public Task DeleteAsync(string id);
        public Task<bool> Exists(string id);
    }
}