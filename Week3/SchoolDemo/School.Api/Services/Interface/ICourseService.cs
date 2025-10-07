using School.Models;

namespace School.Services
{
    public interface ICourseService
    {
        public Task<List<Course>> GetAllAsync();
        public Task<Course?> GetByIdAsync(int id);
        public Task<Course> CreateAsync(Course course);
        public Task UpdateAsync(int id, Course course);
        public Task DeleteAsync(int id);
        public Task<bool> Exists(int id);
    }
}