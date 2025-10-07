using School.Models;

namespace School.Services
{
    public interface IInstructorService
    {
        public Task<List<Instructor>> GetAllAsync();
        public Task<Instructor?> GetByIdAsync(string id);
        public Task CreateAsync(Instructor instructor);
        public Task UpdateAsync(string id, Instructor instructor);
        public Task DeleteAsync(string id);
        public Task<bool> Exists(string id);
    }
}