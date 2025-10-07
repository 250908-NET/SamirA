using FluentAssertions;
using School.Models;
using School.Repositories;
using Microsoft.EntityFrameworkCore;

namespace School.Tests
{
    public class StudentRepositoryTests : TestBase
    {
        private readonly StudentRepository _repository;

        public StudentRepositoryTests()
        {
            _repository = new StudentRepository(Context);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllStudents()
        {
            // Arrange
            await SeedDataAsync();

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().Contain(s => s.FirstName == "Alice");
            result.Should().Contain(s => s.FirstName == "Bob");
        }

        [Fact]
        public async Task GetByIdAsync_WithValidId_ShouldReturnStudent()
        {
            // Arrange
            await SeedDataAsync();

            // Act
            var result = await _repository.GetByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result.FirstName.Should().Be("Alice");
            result.Email.Should().Be("alice.smith@student.edu");
        }

        [Fact]
        public async Task GetByIdAsync_WithInvalidId_ShouldReturnNull()
        {
            // Arrange
            await SeedDataAsync();

            // Act
            var result = await _repository.GetByIdAsync(999);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task AddAsync_ShouldAddStudentToDatabase()
        {
            // Arrange
            var newStudent = new Student
            {
                FirstName = "Charlie",
                LastName = "Brown",
                Email = "charlie.brown@student.edu"
            };

            // Act
            var result = await _repository.AddAsync(newStudent);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.FirstName.Should().Be("Charlie");

            // Verify it's in the database
            var studentInDb = await Context.Students.FindAsync(result.Id);
            studentInDb.Should().NotBeNull();
            studentInDb.FirstName.Should().Be("Charlie");
        }

        [Fact]
        public async Task EnrollAsync_WithValidIds_ShouldEnrollStudentInCourse()
        {
            // Arrange
            await SeedDataAsync();

            // Act
            await _repository.EnrollAsync(1, 1);

            // Assert
            var student = await Context.Students
                .Include(s => s.Courses)
                .FirstOrDefaultAsync(s => s.Id == 1);

            student.Should().NotBeNull();
            student.Courses.Should().HaveCount(1);
            student.Courses.First().Id.Should().Be(1);
        }

        [Fact]
        public async Task EnrollAsync_WithInvalidStudentId_ShouldThrowException()
        {
            // Arrange
            await SeedDataAsync();

            // Act & Assert
            await _repository.Invoking(r => r.EnrollAsync(999, 1))
                .Should().ThrowAsync<ArgumentException>()
                .WithMessage("*Student with ID 999 not found*");
        }

        [Fact]
        public async Task EnrollAsync_WithInvalidCourseId_ShouldThrowException()
        {
            // Arrange
            await SeedDataAsync();

            // Act & Assert
            await _repository.Invoking(r => r.EnrollAsync(1, 999))
                .Should().ThrowAsync<ArgumentException>()
                .WithMessage("*Course with ID 999 not found*");
        }

        [Fact]
        public async Task EnrollAsync_WhenAlreadyEnrolled_ShouldNotDuplicateEnrollment()
        {
            // Arrange
            await SeedDataAsync();
            await _repository.EnrollAsync(1, 1); // First enrollment

            // Act
            await _repository.EnrollAsync(1, 1); // Attempt duplicate

            // Assert
            var student = await Context.Students
                .Include(s => s.Courses)
                .FirstOrDefaultAsync(s => s.Id == 1);

            student.Courses.Should().HaveCount(1); // Should still be only 1
        }
    }
}