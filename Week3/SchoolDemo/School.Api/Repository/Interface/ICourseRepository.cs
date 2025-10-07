using School.Models;

namespace School.Repositories
{
    public interface ICourseRepository
    {
        public Task<List<Course>> GetAllAsync();
        public Task<Course?> GetByIdAsync(int id);
        public Task<Course> AddAsync(Course course);
        public Task UpdateAsync(int id, Course course);
        public Task DeleteAsync(int id);
        public Task<bool> Exists(int id);
    }
}