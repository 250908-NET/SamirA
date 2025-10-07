using School.Models;

namespace School.Services
{
    public interface IStudentService
    {
        public Task<List<Student>> GetAllAsync();
        public Task<Student?> GetByIdAsync(int id);
        public Task CreateAsync(Student student);
    }
}