using School.Models;

namespace School.Services
{
    public interface IStudentService
    {
        public Task<List<Student>> GetAllAsync();
        public Task<Student?> GetByIdAsync(string id);
        public Task<Student> CreateAsync(Student student);
        public Task DeleteAsync(string id);
        public Task UpdateAsync(string id, Student student);
        public Task<bool> Exists(string id);
        public Task EnrollAsync(string studentId, int courseId);
    }
}