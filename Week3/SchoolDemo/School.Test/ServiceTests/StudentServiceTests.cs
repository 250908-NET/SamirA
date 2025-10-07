using FluentAssertions;
using Moq;
using School.Models;
using School.Repositories;
using School.Services;

namespace School.Tests
{
    public class StudentServiceTests
    {
        private readonly Mock<IStudentRepository> _mockRepository;
        private readonly StudentService _service;

        public StudentServiceTests()
        {
            _mockRepository = new Mock<IStudentRepository>();
            _service = new StudentService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllStudents()
        {
            // Arrange
            var expectedStudents = new List<Student>
            {
                new Student { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@test.com" },
                new Student { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane@test.com" }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync())
                          .ReturnsAsync(expectedStudents);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(expectedStudents);

            // Verify repository was called exactly once
            _mockRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_WhenRepositoryReturnsEmptyList_ShouldReturnEmptyList()
        {
            // Arrange
            var emptyList = new List<Student>();
            _mockRepository.Setup(repo => repo.GetAllAsync())
                          .ReturnsAsync(emptyList);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
            _mockRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WithValidId_ShouldReturnStudent()
        {
            // Arrange
            var expectedStudent = new Student 
            { 
                Id = 1, 
                FirstName = "John", 
                LastName = "Doe", 
                Email = "john@test.com" 
            };

            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                          .ReturnsAsync(expectedStudent);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedStudent);
            _mockRepository.Verify(repo => repo.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WithInvalidId_ShouldReturnNull()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(999))
                          .ReturnsAsync((Student?)null);

            // Act
            var result = await _service.GetByIdAsync(999);

            // Assert
            result.Should().BeNull();
            _mockRepository.Verify(repo => repo.GetByIdAsync(999), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(42)]
        [InlineData(100)]
        public async Task GetByIdAsync_WithVariousIds_ShouldCallRepositoryWithCorrectId(int id)
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                          .ReturnsAsync((Student?)null);

            // Act
            await _service.GetByIdAsync(id);

            // Assert
            _mockRepository.Verify(repo => repo.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_WithValidStudent_ShouldReturnCreatedStudent()
        {
            // Arrange
            var inputStudent = new Student 
            { 
                FirstName = "Alice", 
                LastName = "Johnson", 
                Email = "alice@test.com" 
            };

            var createdStudent = new Student 
            { 
                Id = 1, 
                FirstName = "Alice", 
                LastName = "Johnson", 
                Email = "alice@test.com" 
            };

            _mockRepository.Setup(repo => repo.AddAsync(inputStudent))
                          .ReturnsAsync(createdStudent);

            // Act
            var result = await _service.CreateAsync(inputStudent);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(createdStudent);
            _mockRepository.Verify(repo => repo.AddAsync(inputStudent), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_WithNullStudent_ShouldThrowException()
        {
            // Arrange
            Student nullStudent = null;
            _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Student>()))
                          .ThrowsAsync(new ArgumentNullException());

            // Act & Assert
            await _service.Invoking(s => s.CreateAsync(nullStudent))
                         .Should().ThrowAsync<ArgumentNullException>();

            _mockRepository.Verify(repo => repo.AddAsync(nullStudent), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WithValidId_ShouldCallRepositoryDelete()
        {
            // Arrange
            const int studentId = 1;
            _mockRepository.Setup(repo => repo.DeleteAsync(studentId))
                          .Returns(Task.CompletedTask);

            // Act
            await _service.DeleteAsync(studentId);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(studentId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenRepositoryThrows_ShouldPropagateException()
        {
            // Arrange
            const int studentId = 1;
            _mockRepository.Setup(repo => repo.DeleteAsync(studentId))
                          .ThrowsAsync(new InvalidOperationException("Student not found"));

            // Act & Assert
            await _service.Invoking(s => s.DeleteAsync(studentId))
                         .Should().ThrowAsync<InvalidOperationException>()
                         .WithMessage("Student not found");

            _mockRepository.Verify(repo => repo.DeleteAsync(studentId), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WithValidData_ShouldCallRepositoryUpdate()
        {
            // Arrange
            const int studentId = 1;
            var updatedStudent = new Student 
            { 
                Id = 1, 
                FirstName = "Updated", 
                LastName = "Name", 
                Email = "updated@test.com" 
            };

            _mockRepository.Setup(repo => repo.UpdateAsync(studentId, updatedStudent))
                          .Returns(Task.CompletedTask);

            // Act
            await _service.UpdateAsync(studentId, updatedStudent);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(studentId, updatedStudent), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WhenRepositoryThrows_ShouldPropagateException()
        {
            // Arrange
            const int studentId = 999;
            var student = new Student { Id = 999, FirstName = "Test", LastName = "User", Email = "test@test.com" };
            
            _mockRepository.Setup(repo => repo.UpdateAsync(studentId, student))
                          .ThrowsAsync(new InvalidOperationException("Student not found"));

            // Act & Assert
            await _service.Invoking(s => s.UpdateAsync(studentId, student))
                         .Should().ThrowAsync<InvalidOperationException>()
                         .WithMessage("Student not found");

            _mockRepository.Verify(repo => repo.UpdateAsync(studentId, student), Times.Once);
        }

        [Fact]
        public async Task Exists_WithExistingId_ShouldReturnTrue()
        {
            // Arrange
            const int studentId = 1;
            _mockRepository.Setup(repo => repo.Exists(studentId))
                          .ReturnsAsync(true);

            // Act
            var result = await _service.Exists(studentId);

            // Assert
            result.Should().BeTrue();
            _mockRepository.Verify(repo => repo.Exists(studentId), Times.Once);
        }

        [Fact]
        public async Task Exists_WithNonExistingId_ShouldReturnFalse()
        {
            // Arrange
            const int studentId = 999;
            _mockRepository.Setup(repo => repo.Exists(studentId))
                          .ReturnsAsync(false);

            // Act
            var result = await _service.Exists(studentId);

            // Assert
            result.Should().BeFalse();
            _mockRepository.Verify(repo => repo.Exists(studentId), Times.Once);
        }

        [Fact]
        public async Task EnrollAsync_WithValidIds_ShouldCallRepositoryEnroll()
        {
            // Arrange
            const int studentId = 1;
            const int courseId = 2;
            
            _mockRepository.Setup(repo => repo.EnrollAsync(studentId, courseId))
                          .Returns(Task.CompletedTask);

            // Act
            await _service.EnrollAsync(studentId, courseId);

            // Assert
            _mockRepository.Verify(repo => repo.EnrollAsync(studentId, courseId), Times.Once);
        }

        [Fact]
        public async Task EnrollAsync_WhenRepositoryThrows_ShouldPropagateException()
        {
            // Arrange
            const int studentId = 1;
            const int courseId = 999;
            
            _mockRepository.Setup(repo => repo.EnrollAsync(studentId, courseId))
                          .ThrowsAsync(new ArgumentException("Course not found"));

            // Act & Assert
            await _service.Invoking(s => s.EnrollAsync(studentId, courseId))
                         .Should().ThrowAsync<ArgumentException>()
                         .WithMessage("Course not found");

            _mockRepository.Verify(repo => repo.EnrollAsync(studentId, courseId), Times.Once);
        }

        [Fact]
        public async Task EnrollAsync_WithInvalidStudentId_ShouldPropagateException()
        {
            // Arrange
            const int invalidStudentId = 999;
            const int courseId = 1;
            
            _mockRepository.Setup(repo => repo.EnrollAsync(invalidStudentId, courseId))
                          .ThrowsAsync(new ArgumentException($"Student with ID {invalidStudentId} not found"));

            // Act & Assert
            await _service.Invoking(s => s.EnrollAsync(invalidStudentId, courseId))
                         .Should().ThrowAsync<ArgumentException>()
                         .WithMessage($"Student with ID {invalidStudentId} not found");

            _mockRepository.Verify(repo => repo.EnrollAsync(invalidStudentId, courseId), Times.Once);
        }

        [Fact]
        public void Constructor_WithNullRepository_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            Action act = () => new StudentService(null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task GetAllAsync_WhenRepositoryThrows_ShouldPropagateException()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAllAsync())
                          .ThrowsAsync(new InvalidOperationException("Database connection failed"));

            // Act & Assert
            await _service.Invoking(s => s.GetAllAsync())
                         .Should().ThrowAsync<InvalidOperationException>()
                         .WithMessage("Database connection failed");

            _mockRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WhenRepositoryThrows_ShouldPropagateException()
        {
            // Arrange
            const int studentId = 1;
            _mockRepository.Setup(repo => repo.GetByIdAsync(studentId))
                          .ThrowsAsync(new InvalidOperationException("Database connection failed"));

            // Act & Assert
            await _service.Invoking(s => s.GetByIdAsync(studentId))
                         .Should().ThrowAsync<InvalidOperationException>()
                         .WithMessage("Database connection failed");

            _mockRepository.Verify(repo => repo.GetByIdAsync(studentId), Times.Once);
        }
    }
}