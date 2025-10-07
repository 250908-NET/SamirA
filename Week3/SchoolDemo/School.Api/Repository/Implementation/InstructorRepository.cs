using School.Models;
using School.Data;
using Microsoft.EntityFrameworkCore;

namespace School.Repositories
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly SchoolDbContext _context;

        public InstructorRepository(SchoolDbContext context) => _context = context;

        public async Task<List<Instructor>> GetAllAsync() => await _context.Instructors.Include(e => e.Courses).ToListAsync();

        public async Task<Instructor?> GetByIdAsync(string id) => await _context.Instructors.Include(e => e.Courses).FirstOrDefaultAsync(e => e.Id == id);

        public async Task AddAsync(Instructor instructor) 
        {
            await _context.Instructors.AddAsync(instructor);
            await _context.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(string id, Instructor instructor)
        {
            if (await _context.Instructors.FindAsync(id) is null) throw new InvalidOperationException("*Instructor with ID " + id + " not found*");
            instructor.Id = id;
            
            var existing = await _context.Instructors.FindAsync(id);
            _context.Entry(existing).CurrentValues.SetValues(instructor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            if (await _context.Instructors.FindAsync(id) is not Instructor existing) return;
            var instructor = await _context.Instructors.FindAsync(id);
            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(string id) => await _context.Instructors.AnyAsync(e => e.Id == id);
    }
}
