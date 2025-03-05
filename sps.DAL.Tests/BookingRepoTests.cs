//namespace sps.DAL.Tests
//{
//    using Microsoft.EntityFrameworkCore;
//    using sps.DAL.DataModel;
//    using sps.Domain.Model.Enums;

//    [TestFixture]
//    public class BookingRepoTests
//    {
//        private BookingRepo _bookingRepo;
//        private SpsDbContext _dataContext;

//        [SetUp]
//        public void Setup()
//        {
//            var options = new DbContextOptionsBuilder<SpsDbContext>()
//                .UseInMemoryDatabase(databaseName: "TestDatabase")
//                .Options;

//            _dataContext = new SpsDbContext(options);
//            _bookingRepo = new BookingRepo(_dataContext);
//        }

//        [TearDown]
//        public void TearDown()
//        {
//            _dataContext.Database.EnsureDeleted();
//            _dataContext.Dispose();
//        }

//        [Test]
//        public async Task HasActiveBookingAsync_ShouldReturnFalse_WhenNoActiveBookings()
//        {
//            // Arrange
//            var userId = Guid.NewGuid();

//            // Act
//            var result = await _bookingRepo.HasActiveBookingAsync(userId);

//            // Assert
//            Assert.That(result, Is.False);
//        }

//        [Test]
//        public async Task HasActiveBookingAsync_ShouldReturnTrue_WhenActiveBookingExists()
//        {
//            // Arrange
//            var userId = Guid.NewGuid();
//            var timeslotId = Guid.NewGuid();
//            var booking = new Booking
//            {
//                Id = Guid.NewGuid(),
//                UserId = userId,
//                TimeslotId = timeslotId,
//                Status = BookingStatus.InProgress,
//                CreatedAt = DateTime.UtcNow,
//                UpdatedAt = DateTime.UtcNow
//            };

//            await _dataContext.Bookings.AddAsync(booking);
//            await _dataContext.SaveChangesAsync();

//            // Act
//            var result = await _bookingRepo.HasActiveBookingAsync(userId);

//            // Assert
//            Assert.That(result, Is.True);
//        }

//        [Test]
//        public async Task HasActiveBookingAsync_ShouldReturnFalse_WhenBookingIsCompleted()
//        {
//            // Arrange
//            var userId = Guid.NewGuid();
//            var timeslotId = Guid.NewGuid();
//            var booking = new Booking
//            {
//                Id = Guid.NewGuid(),
//                UserId = userId,
//                TimeslotId = timeslotId,
//                Status = BookingStatus.Completed,
//                CreatedAt = DateTime.UtcNow,
//                UpdatedAt = DateTime.UtcNow
//            };

//            await _dataContext.Bookings.AddAsync(booking);
//            await _dataContext.SaveChangesAsync();

//            // Act
//            var result = await _bookingRepo.HasActiveBookingAsync(userId);

//            // Assert
//            Assert.That(result, Is.False);
//        }

//        [Test]
//        public async Task HasActiveBookingAsync_ShouldReturnFalse_WhenBookingIsCancelled()
//        {
//            // Arrange
//            var userId = Guid.NewGuid();
//            var timeslotId = Guid.NewGuid();
//            var booking = new Booking
//            {
//                Id = Guid.NewGuid(),
//                UserId = userId,
//                TimeslotId = timeslotId,
//                Status = BookingStatus.Canceled,
//                CreatedAt = DateTime.UtcNow,
//                UpdatedAt = DateTime.UtcNow
//            };

//            await _dataContext.Bookings.AddAsync(booking);
//            await _dataContext.SaveChangesAsync();

//            // Act
//            var result = await _bookingRepo.HasActiveBookingAsync(userId);

//            // Assert
//            Assert.That(result, Is.False);
//        }
//    }
//}