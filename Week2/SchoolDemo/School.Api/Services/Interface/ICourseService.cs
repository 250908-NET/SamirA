using School.Models;

namespace School.Services
{
    public interface ICourseService
    {
        public Task<List<Course>> GetAllAsync();
        public Task<Course?> GetByIdAsync(int id);
        public Task CreateAsync(Course course);
    }
}