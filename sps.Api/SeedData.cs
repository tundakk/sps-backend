// SeedData.cs
using sps.DAL.DataModel;
using sps.Domain.Model.Entities;
using sps.Domain.Model.Enums;
using sps.Domain.Model.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace sps.API
{
    /// <summary>
    /// Provides methods to seed initial data into the database.
    /// </summary>
    public static class SeedData
    {
        /// <summary>
        /// Initializes the database with seed data.
        /// </summary>
        /// <param name="serviceProvider">The service provider to resolve dependencies.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser<Guid>>>();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();

            await SeedRolesAsync(roleManager);
            var users = await SeedUsersAsync(userManager);
            await SeedUserProfilesAsync(context, users);
            var rooms = await SeedRoomsAsync(context);
            var timeslots = await SeedTimeslotsAsync(context);
            await SeedServiceMessagesAsync(context);
            await SeedChatMessagesAsync(context, users);
            await context.SaveChangesAsync();
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole<Guid>> roleManager)
        {
            var roles = new List<IdentityRole<Guid>>
            {
                new() { Id = Guid.NewGuid(), Name = "Admin", NormalizedName = "ADMIN" },
                new() { Id = Guid.NewGuid(), Name = "User", NormalizedName = "USER" },
                new() { Id = Guid.NewGuid(), Name = "Manager", NormalizedName = "MANAGER" }
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role.Name!))
                {
                    await roleManager.CreateAsync(role);
                }
            }
        }

        private static async Task<List<IdentityUser<Guid>>> SeedUsersAsync(UserManager<IdentityUser<Guid>> userManager)
        {
            var users = new List<IdentityUser<Guid>>();

            var user1 = new IdentityUser<Guid>
            {
                Id = Guid.Parse("115b5117-73f6-4796-a87a-962181baa3e5"),
                UserName = "user1@example.com",
                Email = "user1@example.com",
                NormalizedUserName = "USER1@EXAMPLE.COM",
                NormalizedEmail = "USER1@EXAMPLE.COM",
                EmailConfirmed = true,
                PhoneNumber = "111-11111"
            };

            var user2 = new IdentityUser<Guid>
            {
                Id = Guid.Parse("225b5117-73f6-4796-a87a-962181baa3e5"),
                UserName = "user2@example.com",
                Email = "user2@example.com",
                NormalizedUserName = "USER2@EXAMPLE.COM",
                NormalizedEmail = "USER2@EXAMPLE.COM",
                EmailConfirmed = true,
                PhoneNumber = "222-22222"
            };

            var user3 = new IdentityUser<Guid>
            {
                Id = Guid.Parse("335b5117-73f6-4796-a87a-962181baa3e5"),
                UserName = "user3@example.com",
                Email = "user3@example.com",
                NormalizedUserName = "USER3@EXAMPLE.COM",
                NormalizedEmail = "USER3@EXAMPLE.COM",
                EmailConfirmed = true,
                PhoneNumber = "333-33333"
            };

            users.AddRange(new[] { user1, user2, user3 });

            foreach (var user in users)
            {
                var existingUser = await userManager.FindByEmailAsync(user.Email!);
                if (existingUser == null)
                {
                    var result = await userManager.CreateAsync(user, "Password123!");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "User");
                    }
                    else
                    {
                        throw new Exception($"Failed to create user {user.Email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
            }

            return users;
        }

        private static async Task SeedUserProfilesAsync(DataContext context, List<IdentityUser<Guid>> users)
        {
            var userProfiles = new List<UserProfile>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = users[0].Id,
                    ApartmentNumber = 101,
                    PhoneNumberSecondary = "555-12345",
                    EmailOptOut = false,
                    SmsOptOut = true,
                    PinCode = 1234
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = users[1].Id,
                    ApartmentNumber = 102,
                    PhoneNumberSecondary = null,
                    EmailOptOut = true,
                    SmsOptOut = false,
                    PinCode = 5678
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = users[2].Id,
                    ApartmentNumber = 103,
                    PhoneNumberSecondary = "555-67890",
                    EmailOptOut = false,
                    SmsOptOut = true,
                    PinCode = 9012
                }
            };

            foreach (var profile in userProfiles)
            {
                var existingProfile = await context.UserProfiles.FindAsync(profile.Id);
                if (existingProfile == null)
                {
                    await context.UserProfiles.AddAsync(profile);
                }
            }

            await context.SaveChangesAsync();
        }

        private static async Task<List<Room>> SeedRoomsAsync(DataContext context)
        {
            var rooms = new List<Room>();

            var room1 = new Room
            {
                Id = Guid.Parse("0c9c62e6-2d71-4a7d-b38e-fd3d9f8c0a6f"),
                Name = "Room A",
                Location = "Building 1",
                IsAvailable = true,
                MaxCapacity = 10
            };

            var room2 = new Room
            {
                Id = Guid.Parse("1d8e57f9-5e9b-4c4b-9f2d-1c4e7a1e2b2c"),
                Name = "Room B",
                Location = "Building 2",
                IsAvailable = false,
                MaxCapacity = 8
            };

            var existingRoom1 = await context.Rooms.FindAsync(room1.Id);
            if (existingRoom1 == null)
            {
                context.Rooms.Add(room1);
                rooms.Add(room1);
            }
            else
            {
                rooms.Add(existingRoom1);
            }

            var existingRoom2 = await context.Rooms.FindAsync(room2.Id);
            if (existingRoom2 == null)
            {
                context.Rooms.Add(room2);
                rooms.Add(room2);
            }
            else
            {
                rooms.Add(existingRoom2);
            }

            await context.SaveChangesAsync();
            return rooms;
        }

        private static async Task<List<Timeslot>> SeedTimeslotsAsync(DataContext context)
        {
            var timeslots = new List<Timeslot>();

            var timeslot1 = new Timeslot
            {
                Id = Guid.Parse("2e7c8c0d-9f6b-4e4f-9f4b-2a1d9f6b4e4f"),
                RoomId = Guid.Parse("0c9c62e6-2d71-4a7d-b38e-fd3d9f8c0a6f"),
                SlotTime = new TimeRange(DateTime.UtcNow.AddHours(1), DateTime.UtcNow.AddHours(2)),
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var timeslot2 = new Timeslot
            {
                Id = Guid.Parse("3f8d9d1e-af7c-5d5e-0e5c-3b2e0e7c5f5e"),
                RoomId = Guid.Parse("1d8e57f9-5e9b-4c4b-9f2d-1c4e7a1e2b2c"),
                SlotTime = new TimeRange(DateTime.UtcNow.AddHours(3), DateTime.UtcNow.AddHours(4)),
                IsAvailable = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var existingTimeslot1 = await context.Timeslots.FindAsync(timeslot1.Id);
            if (existingTimeslot1 == null)
            {
                context.Timeslots.Add(timeslot1);
                timeslots.Add(timeslot1);
            }
            else
            {
                timeslots.Add(existingTimeslot1);
            }

            var existingTimeslot2 = await context.Timeslots.FindAsync(timeslot2.Id);
            if (existingTimeslot2 == null)
            {
                context.Timeslots.Add(timeslot2);
                timeslots.Add(timeslot2);
            }
            else
            {
                timeslots.Add(existingTimeslot2);
            }

            await context.SaveChangesAsync();
            return timeslots;
        }

        private static async Task SeedServiceMessagesAsync(DataContext context)
        {
            if (!await context.ServiceMessages.AnyAsync())
            {
                var messages = new List<ServiceMessage>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Message = "Welcome to sps!",
                        CreatedAt = DateTime.UtcNow,
                        IsRead = false
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Message = "Please adhere to the laundry schedule.",
                        CreatedAt = DateTime.UtcNow,
                        IsRead = false
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Message = "Thank you for using sps.",
                        CreatedAt = DateTime.UtcNow,
                        IsRead = false
                    }
                };

                await context.ServiceMessages.AddRangeAsync(messages);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedChatMessagesAsync(DataContext context, List<IdentityUser<Guid>> users)
        {
            if (!await context.ChatMessages.AnyAsync())
            {
                var messages = new List<ChatMessage>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        SenderId = users[0].Id,
                        ReceiverId = users[1].Id,
                        Message = "Hello from user1",
                        CreatedAt = DateTime.UtcNow,
                        IsRead = false
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        SenderId = users[1].Id,
                        ReceiverId = users[0].Id,
                        Message = "Hello from user2",
                        CreatedAt = DateTime.UtcNow,
                        IsRead = false
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        SenderId = users[2].Id,
                        ReceiverId = users[0].Id,
                        Message = "Hello from user3",
                        CreatedAt = DateTime.UtcNow,
                        IsRead = false
                    }
                };

                await context.ChatMessages.AddRangeAsync(messages);
                await context.SaveChangesAsync();
            }
        }
    }
}