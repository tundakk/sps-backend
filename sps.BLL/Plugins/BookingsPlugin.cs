//namespace sps.BLL.Plugins
//{
//    using sps.Domain.Model.Enums;
//    using sps.Domain.Model.Models;
//    using Microsoft.SemanticKernel;
//    using System;
//    using System.Collections.Generic;
//    using System.ComponentModel;
//    using System.Linq;
//    using System.Threading.Tasks;

//    /// <summary>
//    /// Plugin to manage bookings.
//    /// </summary>
//    public class BookingsPlugin
//    {
//        // Mock data for bookings
//        private readonly List<BookingModel> bookings = new()
//        {
//            new BookingModel
//            {
//                Id = Guid.NewGuid(),
//                UserId = Guid.NewGuid(),
//                TimeslotId = Guid.NewGuid(),
//                Status = BookingStatus.Pending,
//                CreatedAt = DateTime.UtcNow,
//                UpdatedAt = DateTime.UtcNow
//            },
//            new BookingModel
//            {
//                Id = Guid.NewGuid(),
//                UserId = Guid.NewGuid(),
//                TimeslotId = Guid.NewGuid(),
//                Status = BookingStatus.InProgress,
//                CreatedAt = DateTime.UtcNow.AddHours(-1),
//                UpdatedAt = DateTime.UtcNow.AddHours(-1)
//            },
//             new BookingModel
//            {
//                Id = Guid.NewGuid(),
//                UserId = Guid.NewGuid(),
//                TimeslotId = Guid.NewGuid(),
//                Status = BookingStatus.Completed,
//                CreatedAt = DateTime.UtcNow.AddHours(-1),
//                UpdatedAt = DateTime.UtcNow.AddHours(-1)
//            },
//             new BookingModel
//            {
//                Id = Guid.NewGuid(),
//                UserId = Guid.NewGuid(),
//                TimeslotId = Guid.NewGuid(),
//                Status = BookingStatus.Canceled,
//                CreatedAt = DateTime.UtcNow.AddHours(-1),
//                UpdatedAt = DateTime.UtcNow.AddHours(-1)
//            }
//        };

//        /// <summary>
//        /// Gets a list of bookings.
//        /// </summary>
//        /// <returns>A list of bookings.</returns>
//        [KernelFunction("get_bookings")]
//        [Description("Gets a list of bookings")]
//        [return: Description("A list of bookings")]
//        public async Task<List<BookingModel>> GetBookingsAsync()
//        {
//            return bookings;
//        }

//        /// <summary>
//        /// Creates a new booking.
//        /// </summary>
//        /// <param name="userId">The user ID.</param>
//        /// <param name="timeslotId">The timeslot ID.</param>
//        /// <returns>The created booking.</returns>
//        [KernelFunction("create_booking")]
//        [Description("Creates a new booking")]
//        [return: Description("The created booking")]
//        public async Task<BookingModel> CreateBookingAsync(Guid userId, Guid timeslotId)
//        {
//            var booking = new BookingModel
//            {
//                Id = Guid.NewGuid(),
//                UserId = userId,
//                TimeslotId = timeslotId,
//                Status = BookingStatus.Pending,
//                CreatedAt = DateTime.UtcNow,
//                UpdatedAt = DateTime.UtcNow
//            };

//            bookings.Add(booking);

//            return booking;
//        }

//        /// <summary>
//        /// Cancels an existing booking.
//        /// </summary>
//        /// <param name="bookingId">The booking ID.</param>
//        /// <returns>The updated booking; returns null if the booking does not exist.</returns>
//        [KernelFunction("cancel_booking")]
//        [Description("Cancels an existing booking")]
//        [return: Description("The updated booking; returns null if the booking does not exist")]
//        public async Task<BookingModel?> CancelBookingAsync(Guid bookingId)
//        {
//            var booking = bookings.FirstOrDefault(b => b.Id == bookingId);

//            if (booking == null)
//            {
//                return null;
//            }

//            booking.Status = BookingStatus.Canceled;
//            booking.UpdatedAt = DateTime.UtcNow;

//            return booking;
//        }
//    }
//}