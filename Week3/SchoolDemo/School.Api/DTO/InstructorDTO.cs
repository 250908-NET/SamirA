using School.Models;

namespace School.DTO
{
    public class InstructorDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<CourseDTO> Courses { get; set; } = new();
        public Double Salary { get; set; }
    }
}