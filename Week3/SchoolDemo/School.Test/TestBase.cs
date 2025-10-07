using Microsoft.EntityFrameworkCore;
using School.Models;
using School.Data;

namespace School.Tests
{

    public class TestBase : IDisposable
    {

        protected SchoolDbContext Context { get; private set; }

        public TestBase()
        {
            var options = new DbContextOptionsBuilder<SchoolDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB per test
                .Options;

            Context = new SchoolDbContext(options);
            Context.Database.EnsureCreated();
        }

        protected async Task SeedDataAsync()
        {
            // Add test data
            var instructor = new Instructor
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@instructor.edu"
            };

            var student1 = new Student
            {
                Id = 1,
                FirstName = "Alice",
                LastName = "Smith",
                Email = "alice.smith@student.edu"
            };

            var student2 = new Student
            {
                Id = 2,
                FirstName = "Bob",
                LastName = "Johnson",
                Email = "bob.johnson@student.edu"
            };

            var course = new Course
            {
                Id = 1,
                Title = "C# Programming",
                Description = "Learn C# programming",
                InstructorId = 1
            };

            Context.Instructors.Add(instructor);
            Context.Students.AddRange(student1, student2);
            Context.Courses.Add(course);

            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
