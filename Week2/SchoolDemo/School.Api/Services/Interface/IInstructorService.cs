using School.Models;

namespace School.Services
{
    public interface IInstructorService
    {
        public Task<List<Instructor>> GetAllAsync();
        public Task<Instructor?> GetByIdAsync(int id);
        public Task CreateAsync(Instructor instructor);
    }
}