using School.Models;

namespace School.Repositories
{
    public interface ICourseRepository
    {
        public Task<List<Course>> GetAllAsync();
        public Task<Course?> GetByIdAsync(int id);
        public Task AddAsync(Course course);
        public Task SaveChangesAsync();
    }
}