using School.Models;
using School.Repositories;

namespace School.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;

        public StudentService(IStudentRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Student>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<Student?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task CreateAsync(Student student)
        {
            await _repo.AddAsync(student);
            await _repo.SaveChangesAsync();
        }
    }
}