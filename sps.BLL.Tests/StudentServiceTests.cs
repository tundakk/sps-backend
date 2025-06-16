namespace sps.BLL.Tests
{
    using Microsoft.Extensions.Logging;
    using Moq;
    using NUnit.Framework;
    using sps.BLL.Services.Implementations;
    using sps.DAL.Repos.Interfaces;
    using sps.Domain.Model.Entities;
    using sps.Domain.Model.Models;
    using sps.Domain.Model.ValueObjects;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [TestFixture]
    public class StudentServiceTests
    {
        private Mock<IStudentRepo> _studentRepoMock;
        private Mock<ILogger<StudentService>> _loggerMock;
        private StudentService _studentService;

        [SetUp]
        public void Setup()
        {
            _studentRepoMock = new Mock<IStudentRepo>();
            _loggerMock = new Mock<ILogger<StudentService>>();
            _studentService = new StudentService(_studentRepoMock.Object, _loggerMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _studentRepoMock = null;
            _loggerMock = null;
            _studentService = null;
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnAllStudents()
        {
            // Arrange
            var students = new List<Student>
            {
                new Student { Id = Guid.NewGuid(), StudentNumber = "12345", CPRNumber = new CPRNumber("1234567890"), Name = new SensitiveString("John Doe") },
                new Student { Id = Guid.NewGuid(), StudentNumber = "67890", CPRNumber = new CPRNumber("0987654321"), Name = new SensitiveString("Jane Doe") }
            };
            _studentRepoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(students);

            // Act
            var result = await _studentService.GetAllAsync();

            // Assert
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnStudent_WhenStudentExists()
        {
            // Arrange
            var studentId = Guid.NewGuid();
            var student = new Student { Id = studentId, StudentNumber = "12345", CPRNumber = new CPRNumber("1234567890"), Name = new SensitiveString("John Doe") };
            _studentRepoMock.Setup(repo => repo.GetByIdAsync(studentId)).ReturnsAsync(student);

            // Act
            var result = await _studentService.GetByIdAsync(studentId);

            // Assert
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.Id, Is.EqualTo(studentId));
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnNull_WhenStudentDoesNotExist()
        {
            // Arrange
            var studentId = Guid.NewGuid();
            _studentRepoMock.Setup(repo => repo.GetByIdAsync(studentId)).ReturnsAsync((Student)null);

            // Act
            var result = await _studentService.GetByIdAsync(studentId);

            // Assert
            Assert.That(result.Data, Is.Null);
        }

        [Test]
        public async Task InsertAsync_ShouldAddStudent()
        {
            // Arrange
            var studentModel = new StudentModel
            {
                Id = Guid.NewGuid(),
                StudentNumber = "12345",
                CPRNumber = new CPRNumber("1234567890"), // Assuming CPRNumber has a constructor that takes a string
                Name = new SensitiveString("John Doe") // Assuming SensitiveString has a constructor that takes a string
            };
            var student = new Student
            {
                Id = studentModel.Id,
                StudentNumber = studentModel.StudentNumber,
                CPRNumber = studentModel.CPRNumber,
                Name = studentModel.Name
            };
            _studentRepoMock.Setup(repo => repo.InsertAsync(It.IsAny<Student>())).ReturnsAsync(student);

            // Act
            var result = await _studentService.InsertAsync(studentModel);

            // Assert
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.Id, Is.EqualTo(studentModel.Id));
        }        [Test]
        public async Task UpdateAsync_ShouldUpdateStudent()
        {
            // Arrange
            var studentModel = new StudentModel
            {
                Id = Guid.NewGuid(),
                StudentNumber = "12345",
                CPRNumber = new CPRNumber("1234567890"), // Assuming CPRNumber has a constructor that takes a string
                Name = new SensitiveString("John Doe") // Assuming SensitiveString has a constructor that takes a string
            };
            var student = new Student
            {
                Id = studentModel.Id,
                StudentNumber = studentModel.StudentNumber,
                CPRNumber = studentModel.CPRNumber,
                Name = studentModel.Name
            };
            
            // Mock both GetByIdAsync (for the update check) and UpdateAsync
            _studentRepoMock.Setup(repo => repo.GetByIdAsync(studentModel.Id)).ReturnsAsync(student);
            _studentRepoMock.Setup(repo => repo.UpdateAsync(It.IsAny<Student>())).ReturnsAsync(student);

            // Act
            var result = await _studentService.UpdateAsync(studentModel);

            // Assert
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.Id, Is.EqualTo(studentModel.Id));
        }

        [Test]
        public async Task DeleteAsync_ShouldRemoveStudent()
        {
            // Arrange
            var studentId = Guid.NewGuid();
            var student = new Student { Id = studentId, StudentNumber = "12345", CPRNumber = new CPRNumber("1234567890"), Name = new SensitiveString("John Doe") };
            _studentRepoMock.Setup(repo => repo.GetByIdAsync(studentId)).ReturnsAsync(student);
            _studentRepoMock.Setup(repo => repo.DeleteAsync(student)).ReturnsAsync(student);

            // Act
            var result = await _studentService.DeleteAsync(studentId);

            // Assert
            Assert.That(result.Data, Is.True);
        }
    }
}