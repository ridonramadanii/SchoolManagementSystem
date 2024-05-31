using System.Linq;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Services;
using Xunit;

namespace SchoolManagementSystem.Tests
{
    public class StudentServiceTests
    {
        private readonly StudentService _service;

        public StudentServiceTests()
        {
            // Initialize the StudentService with some test data
            _service = new StudentService();

            // Adding initial students for testing
            _service.AddStudent(new Student { Id = 1, Name = "John Doe", Email = "john.doe@example.com" });
            _service.AddStudent(new Student { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com" });
        }

        [Fact]
        public void GetAllStudents_ReturnsAllStudents()
        {
            // Act
            var students = _service.GetAllStudents();

            // Assert
            Assert.Equal(2, students.Count()); // Assuming the initial data has 2 students
        }

        [Fact]
        public void GetStudentById_ReturnsCorrectStudent()
        {
            // Act
            var student = _service.GetStudentById(1);

            // Assert
            Assert.NotNull(student);
            Assert.Equal(1, student.Id);
        }

        [Fact]
        public void AddStudent_AddsStudentCorrectly()
        {
            // Arrange
            var newStudent = new Student { Id = 3, Name = "New Student", Email = "new.student@example.com" };

            // Act
            _service.AddStudent(newStudent);
            var students = _service.GetAllStudents();

            // Assert
            Assert.Equal(3, students.Count()); // Assuming the initial data had 2 students
            Assert.Contains(students, s => s.Name == "New Student");
        }

        [Fact]
        public void UpdateStudent_UpdatesStudentCorrectly()
        {
            // Arrange
            var student = _service.GetStudentById(1);
            student.Name = "Updated Name";

            // Act
            _service.UpdateStudent(student);
            var updatedStudent = _service.GetStudentById(1);

            // Assert
            Assert.Equal("Updated Name", updatedStudent.Name);
        }

        [Fact]
        public void DeleteStudent_DeletesStudentCorrectly()
        {
            // Act
            _service.DeleteStudent(1);
            var students = _service.GetAllStudents();

            // Assert
            Assert.Equal(1, students.Count()); // Assuming the initial data had 2 students
            Assert.DoesNotContain(students, s => s.Id == 1);
        }

        [Fact]
        public void GetStudentById_ReturnsNull_ForNonExistentId()
        {
            // Act
            var student = _service.GetStudentById(999);

            // Assert
            Assert.Null(student);
        }

        [Fact]
        public void AddStudent_ThrowsException_WhenStudentIsNull()
        {
            // Act & Assert
            Assert.Throws<System.ArgumentNullException>(() => _service.AddStudent(null));
        }

        [Fact]
        public void AddStudent_ThrowsException_WhenStudentIdExists()
        {
            // Arrange
            var existingStudent = new Student { Id = 1, Name = "Duplicate Student", Email = "duplicate@example.com" };

            // Act & Assert
            Assert.Throws<System.InvalidOperationException>(() => _service.AddStudent(existingStudent));
        }

        [Fact]
        public void UpdateStudent_ThrowsException_WhenStudentIsNull()
        {
            // Act & Assert
            Assert.Throws<System.ArgumentNullException>(() => _service.UpdateStudent(null));
        }

        [Fact]
        public void UpdateStudent_ThrowsException_WhenStudentDoesNotExist()
        {
            // Arrange
            var nonExistentStudent = new Student { Id = 999, Name = "Non Existent", Email = "non.existent@example.com" };

            // Act & Assert
            Assert.Throws<System.InvalidOperationException>(() => _service.UpdateStudent(nonExistentStudent));
        }

        [Fact]
        public void DeleteStudent_ThrowsException_WhenIdDoesNotExist()
        {
            // Act & Assert
            Assert.Throws<System.InvalidOperationException>(() => _service.DeleteStudent(999));
        }

        [Fact]
        public void GetAllStudents_ReturnsEmptyList_WhenNoStudents()
        {
            // Arrange
            var emptyService = new StudentService();

            // Act
            var students = emptyService.GetAllStudents();

            // Assert
            Assert.Empty(students);
        }

        [Fact]
        public void AddStudent_IncreasesStudentCount()
        {
            // Arrange
            var initialCount = _service.GetAllStudents().Count();

            // Act
            _service.AddStudent(new Student { Id = 3, Name = "Another Student", Email = "another@example.com" });
            var finalCount = _service.GetAllStudents().Count();

            // Assert
            Assert.Equal(initialCount + 1, finalCount);
        }

        [Fact]
        public void UpdateStudent_DoesNotChangeStudentCount()
        {
            // Arrange
            var initialCount = _service.GetAllStudents().Count();
            var student = _service.GetStudentById(2);
            student.Name = "Updated Again";

            // Act
            _service.UpdateStudent(student);
            var finalCount = _service.GetAllStudents().Count();

            // Assert
            Assert.Equal(initialCount, finalCount);
        }

        [Fact]
        public void DeleteStudent_DecreasesStudentCount()
        {
            // Arrange
            var initialCount = _service.GetAllStudents().Count();

            // Act
            _service.DeleteStudent(2);
            var finalCount = _service.GetAllStudents().Count();

            // Assert
            Assert.Equal(initialCount - 1, finalCount);
        }

        [Fact]
        public void AddStudent_AssignsNewId()
        {
            // Arrange
            var newStudent = new Student { Name = "Student Without Id", Email = "student.noid@example.com" };

            // Act
            _service.AddStudent(newStudent);

            // Assert
            Assert.True(newStudent.Id > 0);
        }

        [Fact]
        public void GetAllStudents_ReturnsStudentsInExpectedOrder()
        {
            // Act
            var students = _service.GetAllStudents();

            // Assert
            Assert.Equal("Jane Smith", students.ElementAt(0).Name);
            Assert.Equal("John Doe", students.ElementAt(1).Name);
        }

        [Fact]
        public void AddStudent_DoesNotThrowException_ForUniqueId()
        {
            // Arrange
            var uniqueStudent = new Student { Id = 3, Name = "Unique Student", Email = "unique@example.com" };

            // Act & Assert
            _service.AddStudent(uniqueStudent);
        }

        [Fact]
        public void UpdateStudent_DoesNotThrowException_ForExistingId()
        {
            // Arrange
            var existingStudent = _service.GetStudentById(1);
            existingStudent.Name = "Still Exists";

            // Act & Assert
            _service.UpdateStudent(existingStudent);
        }

        [Fact]
        public void DeleteStudent_DoesNotThrowException_ForExistingId()
        {
            // Act & Assert
            _service.DeleteStudent(1);
        }

        [Fact]
        public void GetAllStudents_DoesNotReturnNull()
        {
            // Act
            var students = _service.GetAllStudents();

            // Assert
            Assert.NotNull(students);
        }
    }
}
