using School.Models;
using School.Data;
using Microsoft.EntityFrameworkCore;

namespace School.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolDbContext _context;

        public StudentRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            List<Student> students = await _context.Students.Include(e => e.Courses).ToListAsync();
            return students;
        }

        public async Task<Student?> GetByIdAsync(string id)
        {
            return await _context.Students.Include(e => e.Courses).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Student> AddAsync(Student student) 
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            Student newStudent = await _context.Students.FirstOrDefaultAsync(e => e.FirstName == student.FirstName && e.LastName == student.LastName && e.Email == student.Email);
            return newStudent;
        }
        
        public async Task UpdateAsync(string id, Student student) 
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(string id) => await _context.Students.AnyAsync(e => e.Id == id);

        public async Task EnrollAsync(string studentId, int courseId) 
        {
            var student = await _context.Students
                .Include(s => s.Courses)
                .FirstOrDefaultAsync(s => s.Id == studentId);
                
            if (student == null)
                throw new ArgumentException($"Student with ID {studentId} not found");

            var course = await _context.Courses.FindAsync(courseId);
            if (course == null)
                throw new ArgumentException($"Course with ID {courseId} not found");

            // Check if already enrolled
            if (!student.Courses.Any(c => c.Id == courseId))
            {
                student.Courses.Add(course);
                await _context.SaveChangesAsync();
            }
        }
    }
}
