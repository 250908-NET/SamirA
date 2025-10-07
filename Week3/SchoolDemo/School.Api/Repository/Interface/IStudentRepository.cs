using School.Models;

namespace School.Repositories
{
    public interface IStudentRepository
    {
        public Task<List<Student>> GetAllAsync();
        public Task<Student?> GetByIdAsync(string id);
        public Task<Student> AddAsync(Student student);
        public Task UpdateAsync(string id, Student student);
        public Task DeleteAsync(string id);
        public Task<bool> Exists(string id);
        public Task EnrollAsync(string studentId, int courseId);
    }
}