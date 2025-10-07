using School.Models;
using School.Data;
using Microsoft.EntityFrameworkCore;

namespace School.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly SchoolDbContext _context;

        public CourseRepository(SchoolDbContext context) => _context = context;

        public async Task<List<Course>> GetAllAsync() => await _context.Courses.Include(e => e.Students).Include(e => e.Instructor).ToListAsync();

        public async Task<Course?> GetByIdAsync(int id) => await _context.Courses.Include(e => e.Students).Include(e => e.Instructor).FirstOrDefaultAsync(e => e.Id == id); // await _context.Courses.FindAsync(id);

        public async Task<Course> AddAsync(Course course) 
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return course;
        }

        public async Task UpdateAsync(int id, Course course) 
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id) 
        {
            var course = await _context.Courses.FindAsync(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id) => (await _context.Courses.FindAsync(id) != null) ? true : false;
    }
}