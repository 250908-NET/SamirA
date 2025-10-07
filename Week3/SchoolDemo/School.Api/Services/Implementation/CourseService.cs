using School.Models;
using School.Repositories;

namespace School.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repo;

        public CourseService(ICourseRepository repo)
        {
            if (repo == null) throw new ArgumentNullException(nameof(repo));
            _repo = repo;
        }

        public async Task<List<Course>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<Course?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task<Course> CreateAsync(Course course)
        {
            Course createdCourse = await _repo.AddAsync(course);
            return createdCourse;
        }

        public async Task UpdateAsync(int id, Course course)
        {
            await _repo.UpdateAsync(id, course);
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<bool> Exists(int id) => await _repo.Exists(id);
    }
}