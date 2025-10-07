using School.Models;
using School.Data;
using Microsoft.EntityFrameworkCore;

namespace School.Repositories
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly SchoolDbContext _context;

        public InstructorRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public Task<List<Instructor>> GetAllAsync()
        {
           throw new NotImplementedException();
        }

        public Task<Instructor?> GetByIdAsync(int id)
        {
           throw new NotImplementedException();
        }

        public Task AddAsync(Instructor instructor)
        {
           throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
           throw new NotImplementedException();
        }
    }
}
