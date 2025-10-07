using FluentAssertions;
using School.Models;
using School.Repositories;
using Microsoft.EntityFrameworkCore;

namespace School.Tests
{
    public class InstructorRepositoryTests : TestBase
    {
        private readonly InstructorRepository _repository;

        public InstructorRepositoryTests()
        {
            _repository = new InstructorRepository(Context);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllInstructors()
        {
            // Arrange
            await SeedDataAsync();

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            result.Should().Contain(i => i.FirstName == "John" && i.LastName == "Doe" && i.Email == "john.doe@instructor.edu");
        }

        [Fact]
        public async Task GetAllAsync_WithMultipleInstructors_ShouldReturnAll()
        {
            // Arrange
            await SeedDataAsync();
            
            var additionalInstructor = new Instructor
            {
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@instructor.edu"
            };
            
            await _repository.AddAsync(additionalInstructor);

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            result.Should().HaveCount(2);
            result.Should().Contain(i => i.FirstName == "John");
            result.Should().Contain(i => i.FirstName == "Jane");
        }

        [Fact]
        public async Task GetAllAsync_WithEmptyDatabase_ShouldReturnEmptyList()
        {
            // Arrange
            // No seeding - empty database

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetByIdAsync_WithValidId_ShouldReturnInstructor()
        {
            // Arrange
            await SeedDataAsync();

            // Act
            var result = await _repository.GetByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result.FirstName.Should().Be("John");
            result.LastName.Should().Be("Doe");
            result.Email.Should().Be("john.doe@instructor.edu");
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
        public async Task GetByIdAsync_WithEmptyDatabase_ShouldReturnNull()
        {
            // Arrange
            // No seeding - empty database

            // Act
            var result = await _repository.GetByIdAsync(1);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task AddAsync_ShouldAddInstructorToDatabase()
        {
            // Arrange
            var newInstructor = new Instructor
            {
                FirstName = "Alice",
                LastName = "Johnson",
                Email = "alice.johnson@instructor.edu"
            };

            // Act
            await _repository.AddAsync(newInstructor);

            // Assert
            newInstructor.Id.Should().BeGreaterThan(0);

            // Verify it's actually in the database
            var instructorInDb = await Context.Instructors.FindAsync(newInstructor.Id);
            instructorInDb.Should().NotBeNull();
            instructorInDb.FirstName.Should().Be("Alice");
            instructorInDb.LastName.Should().Be("Johnson");
            instructorInDb.Email.Should().Be("alice.johnson@instructor.edu");
        }

        [Fact]
        public async Task AddAsync_WithMultipleInstructors_ShouldAddAll()
        {
            // Arrange
            var instructor1 = new Instructor { FirstName = "Alice", LastName = "Johnson", Email = "alice.johnson@instructor.edu" };
            var instructor2 = new Instructor { FirstName = "Bob", LastName = "Smith", Email = "bob.smith@instructor.edu" };

            // Act
            await _repository.AddAsync(instructor1);
            await _repository.AddAsync(instructor2);

            // Assert
            var allInstructors = await _repository.GetAllAsync();
            allInstructors.Should().HaveCount(2);
            allInstructors.Should().Contain(i => i.FirstName == "Alice");
            allInstructors.Should().Contain(i => i.FirstName == "Bob");
        }

        [Fact]
        public async Task AddAsync_WithNullInstructor_ShouldThrowException()
        {
            // Arrange
            Instructor nullInstructor = null;

            // Act & Assert
            await _repository.Invoking(r => r.AddAsync(nullInstructor))
                .Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task UpdateAsync_WithValidInstructor_ShouldUpdateDatabase()
        {
            // Arrange
            await SeedDataAsync();
            
            var updatedInstructor = new Instructor
            {
                Id = 1,
                FirstName = "UpdatedJohn",
                LastName = "UpdatedDoe",
                Email = "updated.john.doe@instructor.edu"
            };

            // Act
            await _repository.UpdateAsync(1, updatedInstructor);

            // Assert
            var result = await _repository.GetByIdAsync(1);
            result.Should().NotBeNull();
            result.FirstName.Should().Be("UpdatedJohn");
            result.LastName.Should().Be("UpdatedDoe");
            result.Email.Should().Be("updated.john.doe@instructor.edu");
        }

        [Fact]
        public async Task UpdateAsync_WithNonExistentInstructor_ShouldThrowException()
        {
            // Arrange
            var nonExistentInstructor = new Instructor
            {
                Id = 999,
                FirstName = "Ghost",
                LastName = "Instructor"
            };

            // Act & Assert
            await _repository.Invoking(r => r.UpdateAsync(999, nonExistentInstructor))
                .Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("*Instructor with ID 999 not found*");
        }

        [Fact]
        public async Task UpdateAsync_WithMismatchedIds_ShouldUpdateBasedOnParameterId()
        {
            // Arrange
            await SeedDataAsync();
            
            var updatedInstructor = new Instructor
            {
                Id = 999, // Different from parameter ID
                FirstName = "UpdatedJohn",
                LastName = "UpdatedDoe",
                Email = "updated.john.doe@instructor.edu"
            };

            // Act
            await _repository.UpdateAsync(1, updatedInstructor); // Using ID 1

            // Assert
            var result = await _repository.GetByIdAsync(1);
            result.Should().NotBeNull();
            result.FirstName.Should().Be("UpdatedJohn");
            result.LastName.Should().Be("UpdatedDoe");
            result.Email.Should().Be("updated.john.doe@instructor.edu");
        }

        [Fact]
        public async Task DeleteAsync_WithValidId_ShouldRemoveInstructor()
        {
            // Arrange
            await SeedDataAsync();

            // Act
            await _repository.DeleteAsync(1);

            // Assert
            var deletedInstructor = await _repository.GetByIdAsync(1);
            deletedInstructor.Should().BeNull();

            var allInstructors = await _repository.GetAllAsync();
            allInstructors.Should().BeEmpty();
        }

        [Fact]
        public async Task DeleteAsync_WithInvalidId_ShouldNotThrowException()
        {
            // Arrange
            await SeedDataAsync();

            // Act & Assert - should not throw for non-existent ID
            await _repository.Invoking(r => r.DeleteAsync(999))
                .Should().NotThrowAsync();

            // Verify original instructor is still there
            var instructor = await _repository.GetByIdAsync(1);
            instructor.Should().NotBeNull();
        }

        [Fact]
        public async Task DeleteAsync_WithEmptyDatabase_ShouldNotThrowException()
        {
            // Arrange
            // No seeding - empty database

            // Act & Assert
            await _repository.Invoking(r => r.DeleteAsync(1))
                .Should().NotThrowAsync();
        }

        [Fact]
        public async Task Exists_WithValidId_ShouldReturnTrue()
        {
            // Arrange
            await SeedDataAsync();

            // Act
            var result = await _repository.Exists(1);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task Exists_WithInvalidId_ShouldReturnFalse()
        {
            // Arrange
            await SeedDataAsync();

            // Act
            var result = await _repository.Exists(999);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task Exists_WithEmptyDatabase_ShouldReturnFalse()
        {
            // Arrange
            // No seeding - empty database

            // Act
            var result = await _repository.Exists(1);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldIncludeCoursesIfConfigured()
        {
            // Arrange
            await SeedDataAsync();

            // Act
            var result = await _repository.GetByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result.FirstName.Should().Be("John");
            result.Courses.Should().NotBeNull();
            
            // If your repository includes courses, you can uncomment and adjust:
            // result.Courses.Should().HaveCount(1);
            // result.Courses.First().Title.Should().Be("C# Programming");
        }

        [Fact]
        public async Task AddAsync_ShouldGenerateUniqueIds()
        {
            // Arrange
            var instructor1 = new Instructor { FirstName = "Alice", LastName = "Johnson", Email = "alice.johnson@instructor.edu" };
            var instructor2 = new Instructor { FirstName = "Bob", LastName = "Smith", Email = "bob.smith@instructor.edu" };

            // Act
            await _repository.AddAsync(instructor1);
            await _repository.AddAsync(instructor2);

            // Assert
            instructor1.Id.Should().BeGreaterThan(0);
            instructor2.Id.Should().BeGreaterThan(0);
            instructor1.Id.Should().NotBe(instructor2.Id);
        }

        [Theory]
        [InlineData("John", "Doe", "john.doe@instructor.edu")]
        [InlineData("Jane", "Smith", "jane.smith@instructor.edu")]
        [InlineData("Alice", "Johnson", "alice.johnson@instructor.edu")]
        public async Task AddAsync_WithVariousNames_ShouldAddSuccessfully(string firstName, string lastName, string email)
        {
            // Arrange
            var instructor = new Instructor
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };

            // Act
            await _repository.AddAsync(instructor);

            // Assert
            var result = await _repository.GetByIdAsync(instructor.Id);
            result.Should().NotBeNull();
            result.FirstName.Should().Be(firstName);
            result.LastName.Should().Be(lastName);
            result.Email.Should().Be(email);
        }
    }
}