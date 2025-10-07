using School.Models;
using School.Repositories;

namespace School.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorRepository _repo;

        public InstructorService(IInstructorRepository repo)
        {
            if (repo == null) throw new ArgumentNullException(nameof(repo));
            _repo = repo;
        }

        public async Task<List<Instructor>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<Instructor?> GetByIdAsync(string id) => await _repo.GetByIdAsync(id);

        public async Task CreateAsync(Instructor instructor)
        {
            await _repo.AddAsync(instructor);
        }

        public async Task UpdateAsync(string id, Instructor instructor) 
        {
            await _repo.UpdateAsync(id, instructor);
        }

        public async Task DeleteAsync(string id) 
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<bool> Exists(string id) => await _repo.Exists(id);
    }
}