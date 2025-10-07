using FluentAssertions;
using School.Models;
using School.Repositories;
using Microsoft.EntityFrameworkCore;

namespace School.Tests
{
    public class CourseRepositoryTests : TestBase
    {
        private readonly CourseRepository _repository;

        public CourseRepositoryTests()
        {
            _repository = new CourseRepository(Context);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnCoursesWithInstructorAndStudents()
        {
            // Arrange
            await SeedDataAsync();

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            
            var course = result.First();
            course.Title.Should().Be("C# Programming");
            course.Instructor.Should().NotBeNull();
            course.Instructor.FirstName.Should().Be("John");
        }

        [Fact]
        public async Task GetByIdAsync_WithValidId_ShouldReturnCourseWithDetails()
        {
            // Arrange
            await SeedDataAsync();

            // Act
            var result = await _repository.GetByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be("C# Programming");
            result.Instructor.Should().NotBeNull();
            result.Students.Should().NotBeNull();
        }

        [Fact]
        public async Task CreateAsync_ShouldAddCourseToDatabase()
        {
            // Arrange
            await SeedDataAsync();
            
            var newCourse = new Course
            {
                Title = "Advanced C#",
                Description = "Advanced C# concepts",
                InstructorId = 1
            };

            // Act
            var result = await _repository.AddAsync(newCourse);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.Title.Should().Be("Advanced C#");

            // Verify it's in the database
            var courseInDb = await Context.Courses.FindAsync(result.Id);
            courseInDb.Should().NotBeNull();
        }
    }
}