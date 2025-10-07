using School.Models;
using School.Data;
using Microsoft.EntityFrameworkCore;

namespace School.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly SchoolDbContext _context;

        public CourseRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public Task<List<Course>> GetAllAsync()
        {
           throw new NotImplementedException();
        }

        public Task<Course?> GetByIdAsync(int id)
        {
           throw new NotImplementedException();
        }

        public Task AddAsync(Course course)
        {
           throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
           throw new NotImplementedException();
        }
    }
}
