namespace sps.DAL.Tests
{
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using NUnit.Framework;
    using sps.DAL.DataModel;
    using sps.DAL.Repos.Implementations;
    using sps.Domain.Model.Entities;
    using sps.Domain.Model.Services;
    using sps.Domain.Model.ValueObjects;
    using System;
    using System.Threading.Tasks;

    [TestFixture]
    public class StudentRepoTests
    {
        private StudentRepo _studentRepo;
        private SpsDbContext _dataContext;
        private Mock<IEncryptionService> _encryptionServiceMock;

        [SetUp]
        public void Setup()
        {
            _encryptionServiceMock = new Mock<IEncryptionService>();
            var options = new DbContextOptionsBuilder<SpsDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dataContext = new SpsDbContext(options, _encryptionServiceMock.Object);
            _studentRepo = new StudentRepo(_dataContext);
        }

        [TearDown]
        public void TearDown()
        {
            _dataContext.Database.EnsureDeleted();
            _dataContext.Dispose();
        }

        [Test]
        public async Task GetStudentByIdAsync_ShouldReturnNull_WhenStudentDoesNotExist()
        {
            // Arrange
            var studentId = Guid.NewGuid();

            // Act
            var result = await _studentRepo.GetByIdAsync(studentId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetStudentByIdAsync_ShouldReturnStudent_WhenStudentExists()
        {
            // Arrange
            var studentId = Guid.NewGuid();
            var student = new Student
            {
                Id = studentId,
                StudentNumber = "12345",
                CPRNumber = new CPRNumber("1234567890"),
                Name = new SensitiveString("John Doe")
            };

            await _dataContext.Students.AddAsync(student);
            await _dataContext.SaveChangesAsync();

            // Act
            var result = await _studentRepo.GetByIdAsync(studentId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(studentId));
        }

        [Test]
        public async Task AddStudentAsync_ShouldAddStudent()
        {
            // Arrange
            var student = new Student
            {
                Id = Guid.NewGuid(),
                StudentNumber = "12345",
                CPRNumber = new CPRNumber("1234567890"),
                Name = new SensitiveString("John Doe")
            };

            // Act
            await _studentRepo.InsertAsync(student);
            await _dataContext.SaveChangesAsync();

            var result = await _dataContext.Students.FindAsync(student.Id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(student.Id));
        }

        [Test]
        public async Task UpdateStudentAsync_ShouldUpdateStudent()
        {
            // Arrange
            var studentId = Guid.NewGuid();
            var student = new Student
            {
                Id = studentId,
                StudentNumber = "12345",
                CPRNumber = new CPRNumber("4444444446"),
                Name = new SensitiveString("John Doe")
            };

            await _dataContext.Students.AddAsync(student);
            await _dataContext.SaveChangesAsync();

            student.StudentNumber = "67890";

            // Act
            await _studentRepo.UpdateAsync(student);
            await _dataContext.SaveChangesAsync();

            var result = await _dataContext.Students.FindAsync(studentId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StudentNumber, Is.EqualTo("67890"));
        }

        [Test]
        public async Task DeleteStudentAsync_ShouldRemoveStudent()
        {
            // Arrange
            var studentId = Guid.NewGuid();
            var student = new Student
            {
                Id = studentId,
                StudentNumber = "12345",
                CPRNumber = new CPRNumber("1234567890"),
                Name = new SensitiveString("John Doe")
            };

            await _dataContext.Students.AddAsync(student);
            await _dataContext.SaveChangesAsync();

            // Act
            await _studentRepo.DeleteAsync(student);
            await _dataContext.SaveChangesAsync();

            var result = await _dataContext.Students.FindAsync(studentId);

            // Assert
            Assert.That(result, Is.Null);
        }
    }
}