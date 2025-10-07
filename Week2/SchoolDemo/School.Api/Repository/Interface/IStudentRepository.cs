using School.Models;

namespace School.Repositories
{
    public interface IStudentRepository
    {
        public Task<List<Student>> GetAllAsync();
        public Task<Student?> GetByIdAsync(int id);
        public Task AddAsync(Student student);
        public Task SaveChangesAsync();
    }
}