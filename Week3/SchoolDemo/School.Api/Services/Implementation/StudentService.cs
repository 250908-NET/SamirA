using School.Models;
using School.Repositories;

namespace School.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;

        public StudentService(IStudentRepository repo)
        {
            if (repo == null) throw new ArgumentNullException(nameof(repo));
            _repo = repo;
        }

        public async Task<List<Student>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<Student?> GetByIdAsync(string id) 
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Student> CreateAsync(Student student)
        {
            // the long-hand way
            return await _repo.AddAsync(student);
        }

        // the short-hand way
        public async Task DeleteAsync(string id) 
        {
            await _repo.DeleteAsync(id);
        }
        
        public async Task UpdateAsync(string id, Student student) 
        {
            await _repo.UpdateAsync(id, student);
        }

        public async Task<bool> Exists(string id) => await _repo.Exists(id);

        public async Task EnrollAsync(string studentId, int courseId) => await _repo.EnrollAsync(studentId, courseId);
    }
}