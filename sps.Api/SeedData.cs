﻿// SeedData.cs
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using sps.DAL.DataModel;
using sps.Domain.Model.Entities;
using sps.Domain.Model.ValueObjects;

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
            var context = scope.ServiceProvider.GetRequiredService<SpsDbContext>();

            // Identity Seeding
            await SeedRolesAsync(roleManager);
            var users = await SeedUsersAsync(userManager);

            // New Entities Seeding
            var eduCategories = await SeedEduCategoriesAsync(context);
            var diagnoses = await SeedDiagnosesAsync(context);
            var eduStatuses = await SeedEduStatusesAsync(context);
            var supportTypes = await SeedSupportTypesAsync(context);
            var periods = await SeedPeriodsAsync(context);
            var educationalPrograms = await SeedEducationalProgramsAsync(context, eduCategories);
            var educationPeriodRates = await SeedEducationPeriodRatesAsync(context, educationalPrograms, periods);
            var supportingTeachers = await SeedSupportingTeachersAsync(context, educationalPrograms);
            var students = await SeedStudentsAsync(context, educationalPrograms, periods);
            var opkvalSupervisions = await SeedOpkvalSupervisionsAsync(context);
            var teacherPayments = await SeedTeacherPaymentsAsync(context, supportTypes);
            var studentPayments = await SeedStudentPaymentsAsync(context, supportTypes);
            await SeedSpsaCasesAsync(context, students, supportingTeachers, periods, diagnoses, eduCategories, supportTypes, eduStatuses, teacherPayments, opkvalSupervisions, studentPayments);

            await context.SaveChangesAsync();
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole<Guid>> roleManager)
        {
            var roles = new List<IdentityRole<Guid>>
            {
                new() { Id = Guid.NewGuid(), Name = "Admin", NormalizedName = "ADMIN" },
                new() { Id = Guid.NewGuid(), Name = "User", NormalizedName = "USER" },
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
                PhoneNumber = "11111111"
            };

            var user2 = new IdentityUser<Guid>
            {
                Id = Guid.Parse("225b5117-73f6-4796-a87a-962181baa3e5"),
                UserName = "user2@example.com",
                Email = "user2@example.com",
                NormalizedUserName = "USER2@EXAMPLE.COM",
                NormalizedEmail = "USER2@EXAMPLE.COM",
                EmailConfirmed = true,
                PhoneNumber = "22222222"
            };

            var user3 = new IdentityUser<Guid>
            {
                Id = Guid.Parse("335b5117-73f6-4796-a87a-962181baa3e5"),
                UserName = "user3@example.com",
                Email = "user3@example.com",
                NormalizedUserName = "USER3@EXAMPLE.COM",
                NormalizedEmail = "USER3@EXAMPLE.COM",
                EmailConfirmed = false,
                PhoneNumber = "33333333"
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

        private static async Task<List<EduCategory>> SeedEduCategoriesAsync(SpsDbContext context)
        {
            if (await context.EduCategories.AnyAsync())
            {
                return await context.EduCategories.ToListAsync();
            }

            var eduCategories = new List<EduCategory>
            {
                new() { Id = Guid.NewGuid(), Name = "Professionsbachelor" },
                new() { Id = Guid.NewGuid(), Name = "Erhvervsakademiuddannelse" },
                new() { Id = Guid.NewGuid(), Name = "Ungdomsuddannelse" },
            };

            await context.EduCategories.AddRangeAsync(eduCategories);
            await context.SaveChangesAsync();
            return eduCategories;
        }

        private static async Task<List<Diagnosis>> SeedDiagnosesAsync(SpsDbContext context)
        {
            if (await context.Diagnoses.AnyAsync())
            {
                return await context.Diagnoses.ToListAsync();
            }

            var diagnoses = new List<Diagnosis>
            {
                new() { Id = Guid.NewGuid(), Name = "Andet" },
                new() { Id = Guid.NewGuid(), Name = "Bevægehandicap" },
                new() { Id = Guid.NewGuid(), Name = "Generelle indlæringsvanskeligheder" },
                new() { Id = Guid.NewGuid(), Name = "Hørenedsættelse" },
                new() { Id = Guid.NewGuid(), Name = "Kronisk eller langvarig sygdom" },
                new() { Id = Guid.NewGuid(), Name = "Læse- skrivevanskeligheder" },
                new() { Id = Guid.NewGuid(), Name = "Matematikvanskeligheder" },
                new() { Id = Guid.NewGuid(), Name = "Neurologisk betinget funktionsnedsættelse" },
                new() { Id = Guid.NewGuid(), Name = "Psykisk funktionsnedsættelse el. udviklingsforstyrrelse" },
                new() { Id = Guid.NewGuid(), Name = "Sprog- og talevanskeligheder" },
                new() { Id = Guid.NewGuid(), Name = "Synsnedsættelse" }
            };
            await context.Diagnoses.AddRangeAsync(diagnoses);
            await context.SaveChangesAsync();
            return diagnoses;
        }

        private static async Task<List<EduStatus>> SeedEduStatusesAsync(SpsDbContext context)
        {
            if (await context.EduStatuses.AnyAsync())
            {
                return await context.EduStatuses.ToListAsync();
            }

            var eduStatuses = new List<EduStatus>
            {
                new() { Id = Guid.NewGuid(), Name = "Afbrudt" },
                new() { Id = Guid.NewGuid(), Name = "Aktiv" },
                new() { Id = Guid.NewGuid(), Name = "Gennemført" },
                new() { Id = Guid.NewGuid(), Name = "Inaktiv-SPS" },
                new() { Id = Guid.NewGuid(), Name = "Orlov" },
                new() { Id = Guid.NewGuid(), Name = "Syg" }
            };

            await context.EduStatuses.AddRangeAsync(eduStatuses);
            await context.SaveChangesAsync();
            return eduStatuses;
        }

        private static async Task<List<SupportType>> SeedSupportTypesAsync(SpsDbContext context)
        {
            if (await context.SupportTypes.AnyAsync())
            {
                return await context.SupportTypes.ToListAsync();
            }

            var supportTypes = new List<SupportType>
            {
                new() { Id = Guid.NewGuid(), Name = "Interne støttetimer" },
                new() { Id = Guid.NewGuid(), Name = "Interne støttetimer (inkl. opkvalificering og supervision)" },
                new() { Id = Guid.NewGuid(), Name = "Interne støttetimer (instruktion)" },
                new() { Id = Guid.NewGuid(), Name = "Interne støttetimer (ekstern konsulent)" },
                new() { Id = Guid.NewGuid(), Name = "Interne støttetimer (PAU læse-skrive)" },
                new() { Id = Guid.NewGuid(), Name = "Interne støttetimer (PAU andet)" }
            };

            await context.SupportTypes.AddRangeAsync(supportTypes);
            await context.SaveChangesAsync();
            return supportTypes;
        }

        private static async Task<List<Period>> SeedPeriodsAsync(SpsDbContext context)
        {
            if (await context.Periods.AnyAsync())
            {
                return await context.Periods.ToListAsync();
            }

            var periods = new List<Period>
            {
                new() { Id = Guid.NewGuid(), Name = "Forår 23" },
                new() { Id = Guid.NewGuid(), Name = "Efterår 23" },
                new() { Id = Guid.NewGuid(), Name = "Forår 24" },
                new() { Id = Guid.NewGuid(), Name = "Efterår 24" },
                new() { Id = Guid.NewGuid(), Name = "Forår 25" },
                new() { Id = Guid.NewGuid(), Name = "Efterår 25" }
            };

            await context.Periods.AddRangeAsync(periods);
            await context.SaveChangesAsync();
            return periods;
        }

        private static async Task<List<EducationalProgram>> SeedEducationalProgramsAsync(SpsDbContext context, List<EduCategory> eduCategories)
        {
            if (await context.EducationalPrograms.AnyAsync())
            {
                return await context.EducationalPrograms.ToListAsync();
            }

            var programs = new List<EducationalProgram>
            {
                new() { 
                    Id = Guid.NewGuid(), 
                    Name = "Sundhedsadminitrativ koordinator", 
                    ProgramCode = "MC001", 
                    Alias = "Main",
                    EduCategoryId = eduCategories[0].Id,
                    EduCategory = eduCategories[0]
                },
                new() { 
                    Id = Guid.NewGuid(), 
                    Name = "Administrationsbachelor", 
                    ProgramCode = "AB001", 
                    Alias = "Admin",
                    EduCategoryId = eduCategories[0].Id,
                    EduCategory = eduCategories[0]
                },
                new() { 
                    Id = Guid.NewGuid(), 
                    Name = "Administrationsøkonom", 
                    ProgramCode = "AO001", 
                    Alias = "AØ",
                    EduCategoryId = eduCategories[1].Id,
                    EduCategory = eduCategories[1]
                },
                new() { 
                    Id = Guid.NewGuid(), 
                    Name = "Bioanalytiker", 
                    ProgramCode = "BA001", 
                    Alias = "BIO",
                    EduCategoryId = eduCategories[0].Id,
                    EduCategory = eduCategories[0]
                },
                new() { 
                    Id = Guid.NewGuid(), 
                    Name = "Ergoterapeut", 
                    ProgramCode = "ET001", 
                    Alias = "ERGO",
                    EduCategoryId = eduCategories[0].Id,
                    EduCategory = eduCategories[0]
                },
                new() { 
                    Id = Guid.NewGuid(), 
                    Name = "Ernæring og sundhed", 
                    ProgramCode = "ES001", 
                    Alias = "E&S",
                    EduCategoryId = eduCategories[0].Id,
                    EduCategory = eduCategories[0]
                },
                new() { 
                    Id = Guid.NewGuid(), 
                    Name = "Fysioterapeut (Hillerød)", 
                    ProgramCode = "FH001", 
                    Alias = "FYSH",
                    EduCategoryId = eduCategories[0].Id,
                    EduCategory = eduCategories[0]
                },
                new() { 
                    Id = Guid.NewGuid(), 
                    Name = "Fysioterapeut (Nørrebro)", 
                    ProgramCode = "FN001", 
                    Alias = "FYSN",
                    EduCategoryId = eduCategories[0].Id,
                    EduCategory = eduCategories[0]
                }
                // Add the rest of your educational programs as needed
            };

            await context.EducationalPrograms.AddRangeAsync(programs);
            await context.SaveChangesAsync();
            return programs;
        }

        private static async Task<List<EducationPeriodRate>> SeedEducationPeriodRatesAsync(SpsDbContext context, List<EducationalProgram> educationalPrograms, List<Period> periods)
        {
            if (await context.EducationPeriodRates.AnyAsync())
            {
                return await context.EducationPeriodRates.ToListAsync();
            }

            var rates = new List<EducationPeriodRate>();

            // Create rates for each education and period combination
            foreach (var program in educationalPrograms)
            {
                foreach (var period in periods)
                {
                    rates.Add(new EducationPeriodRate
                    {
                        Id = Guid.NewGuid(),
                        EducationalProgramId = program.Id,
                        EducationalProgram = program,
                        PeriodId = period.Id,
                        Period = period,
                        Amount = 100.00m + (decimal)Random.Shared.NextDouble() * 50.00m
                    });
                }
            }

            await context.EducationPeriodRates.AddRangeAsync(rates);
            await context.SaveChangesAsync();
            return rates;
        }

        private static async Task<List<SupportingTeacher>> SeedSupportingTeachersAsync(SpsDbContext context, List<EducationalProgram> educationalPrograms)
        {
            if (await context.SupportingTeachers.AnyAsync())
            {
                return await context.SupportingTeachers.ToListAsync();
            }

            var teachers = new List<SupportingTeacher>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "John Smith",
                    Email = new SensitiveString("john.smith@example.com"),
                    EducationalProgramId = educationalPrograms[0].Id
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Jane Doe",
                    Email = new SensitiveString("jane.doe@example.com"),
                    EducationalProgramId = educationalPrograms[1].Id
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Michael Johnson",
                    Email = new SensitiveString("michael.johnson@example.com"),
                    EducationalProgramId = educationalPrograms[2].Id
                }
            };

            await context.SupportingTeachers.AddRangeAsync(teachers);
            await context.SaveChangesAsync();
            return teachers;
        }

        private static async Task<List<Student>> SeedStudentsAsync(SpsDbContext context, List<EducationalProgram> educationalPrograms, List<Period> periods)
        {
            if (await context.Students.AnyAsync())
            {
                return await context.Students.ToListAsync();
            }

            var students = new List<Student>
    {
        new()
        {
            Id = Guid.NewGuid(),
            StudentNumber = "S10001",
            CPRNumber = new CPRNumber("1111111111"),
            Name = new SensitiveString("Alex Johnson"),
            Comments = new List<Comment>
            {
                new Comment
                {
                    Id = Guid.NewGuid(),
                    CommentText = new SensitiveString("Regular progress reviews needed"),
                    EntityType = "Student",
                    CreatedAt = DateTime.UtcNow
                }
            },
            EducationalProgramId = educationalPrograms[0].Id,
            StartPeriodId = periods[0].Id,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        },
        new()
        {
            Id = Guid.NewGuid(),
            StudentNumber = "S10002",
            CPRNumber = new CPRNumber("2222222222"),
            Name = new SensitiveString("Sam Williams"),
            Comments = new List<Comment>(),
            EducationalProgramId = educationalPrograms[1].Id,
            StartPeriodId = periods[0].Id
        },
        new()
        {
            Id = Guid.NewGuid(),
            StudentNumber = "S10003",
            CPRNumber = new CPRNumber("3333333333"),
            Name = new SensitiveString("Taylor Brown"),
            Comments = new List<Comment>
            {
                new Comment
                {
                    Id = Guid.NewGuid(),
                    CommentText = new SensitiveString("Requires extended time for assessments"),
                    EntityType = "Student",
                    CreatedAt = DateTime.UtcNow
                }
            },
            EducationalProgramId = educationalPrograms[2].Id,
            StartPeriodId = periods[1].Id
        },
        new()
        {
            Id = Guid.NewGuid(),
            StudentNumber = "S10004",
            CPRNumber = new CPRNumber("4444444444"),
            Name = new SensitiveString("Jordan Smith"),
            Comments = new List<Comment>(),
            EducationalProgramId = educationalPrograms[3].Id,
            StartPeriodId = periods[1].Id,
            FinishedDate = DateTime.UtcNow.AddMonths(3)
        }
    };

            await context.Students.AddRangeAsync(students);
            await context.SaveChangesAsync();
            return students;
        }

        private static async Task<List<OpkvalSupervision>> SeedOpkvalSupervisionsAsync(SpsDbContext context)
        {
            if (await context.OpkvalSupervisions.AnyAsync())
            {
                return await context.OpkvalSupervisions.ToListAsync();
            }

            var supervisions = new List<OpkvalSupervision>
            {
                new() { Id = Guid.NewGuid(), CreateDate = DateTime.UtcNow, Status = Domain.Model.Entities.OpkvalSupervisionStatus.Godkendt },
                new() { Id = Guid.NewGuid(), CreateDate = DateTime.UtcNow, Status = Domain.Model.Entities.OpkvalSupervisionStatus.StuderendeGiveSamtykke },
                new() { Id = Guid.NewGuid(), CreateDate = DateTime.UtcNow, Status = Domain.Model.Entities.OpkvalSupervisionStatus.AfventerSTUK },
                new() { Id = Guid.NewGuid(), CreateDate = DateTime.UtcNow, Status = Domain.Model.Entities.OpkvalSupervisionStatus.AfventerSTUK }
            };

            await context.OpkvalSupervisions.AddRangeAsync(supervisions);
            await context.SaveChangesAsync();
            return supervisions;
        }

        private static async Task<List<TeacherPayment>> SeedTeacherPaymentsAsync(SpsDbContext context, List<SupportType> supportTypes)
        {
            if (await context.TeacherPayments.AnyAsync())
            {
                return await context.TeacherPayments.ToListAsync();
            }

            var payments = new List<TeacherPayment>
    {
        new()
        {
            Id = Guid.NewGuid(),
            Date = DateTime.UtcNow.AddDays(-30),
            Comments = new List<Comment>
            {
                new Comment
                {
                    Id = Guid.NewGuid(),
                    CommentText = new SensitiveString("Monthly payment"),
                    EntityType = "TeacherPayment",
                    CreatedAt = DateTime.UtcNow.AddDays(-30)
                }
            },
            Amount = 2500.00m,
            ExternalVoucherNumber = "VP-001",
            SupportTypeId = supportTypes[0].Id
        },
        new()
        {
            Id = Guid.NewGuid(),
            Date = DateTime.UtcNow.AddDays(-15),
            Comments = new List<Comment>(),
            Amount = 1800.50m,
            ExternalVoucherNumber = "VP-002",
            SupportTypeId = supportTypes[1].Id
        },
        new()
        {
            Id = Guid.NewGuid(),
            Date = DateTime.UtcNow.AddDays(-5),
            Comments = new List<Comment>
            {
                new Comment
                {
                    Id = Guid.NewGuid(),
                    CommentText = new SensitiveString("Special tutorial sessions"),
                    EntityType = "TeacherPayment",
                    CreatedAt = DateTime.UtcNow.AddDays(-5)
                }
            },
            Amount = 1200.75m,
            ExternalVoucherNumber = "VP-003",
            SupportTypeId = supportTypes[2].Id
        }
    };

            await context.TeacherPayments.AddRangeAsync(payments);
            await context.SaveChangesAsync();
            return payments;
        }

        private static async Task<List<StudentPayment>> SeedStudentPaymentsAsync(SpsDbContext context, List<SupportType> supportTypes)
        {
            if (await context.StudentPayments.AnyAsync())
            {
                return await context.StudentPayments.ToListAsync();
            }

            var payments = new List<StudentPayment>
    {
        new()
        {
            Id = Guid.NewGuid(),
            Date = DateTime.UtcNow.AddDays(-45),
            AccountNumber = new SensitiveString("9876-5432"),
            Comments = new List<Comment>
            {
                new Comment
                {
                    Id = Guid.NewGuid(),
                    CommentText = new SensitiveString("Financial aid payment"),
                    EntityType = "StudentPayment",
                    CreatedAt = DateTime.UtcNow.AddDays(-45)
                }
            },
            Amount = 1200.00m,
            ExternalVoucherNumber = "SP-001",
            SupportTypeId = supportTypes[0].Id
        },
        new()
        {
            Id = Guid.NewGuid(),
            Date = DateTime.UtcNow.AddDays(-25),
            AccountNumber = new SensitiveString("8765-4321"),
            Comments = new List<Comment>(),
            Amount = 950.50m,
            ExternalVoucherNumber = "SP-002",
            SupportTypeId = supportTypes[1].Id
        },
        new()
        {
            Id = Guid.NewGuid(),
            Date = DateTime.UtcNow.AddDays(-10),
            AccountNumber = new SensitiveString("7654-3210"),
            Comments = new List<Comment>
            {
                new Comment
                {
                    Id = Guid.NewGuid(),
                    CommentText = new SensitiveString("Materials allowance"),
                    EntityType = "StudentPayment",
                    CreatedAt = DateTime.UtcNow.AddDays(-10)
                }
            },
            Amount = 500.25m,
            ExternalVoucherNumber = "SP-003",
            SupportTypeId = supportTypes[2].Id
        }
    };

            await context.StudentPayments.AddRangeAsync(payments);
            await context.SaveChangesAsync();
            return payments;
        }

        private static async Task<List<SpsaCase>> SeedSpsaCasesAsync(
            SpsDbContext context,
            List<Student> students,
            List<SupportingTeacher> teachers,
            List<Period> periods,
            List<Diagnosis> diagnoses,
            List<EduCategory> eduCategories,
            List<SupportType> supportTypes,
            List<EduStatus> eduStatuses,
            List<TeacherPayment> teacherPayments,
            List<OpkvalSupervision> supervisions,
            List<StudentPayment> studentPayments)
        {
            if (await context.SpsaCases.AnyAsync())
            {
                return await context.SpsaCases.ToListAsync();
            }

            var cases = new List<SpsaCase>
            {
               new()
{
    Id = Guid.NewGuid(),
    SpsaCaseNumber = "SC-2023-001",
    HoursSought = 30,
    HoursSpent = 25,
    Comments = new List<Comment>
    {
        new Comment
        {
            Id = Guid.NewGuid(),
            CommentText = new SensitiveString("Progress is being made, further hours may be needed"),
            EntityType = "SpsaCase",
            CreatedAt = DateTime.UtcNow.AddMonths(-3)
        }
    },
    IsActive = true,
    ApplicationDate = DateTime.UtcNow.AddMonths(-3),
    LatestReapplicationDate = DateTime.UtcNow.AddMonths(-1),
    StudentId = students[0].Id,
    Student = students[0], // Add this line
    SupportingTeacherId = teachers[0].Id,
    AppliedPeriodId = periods[0].Id,
    DiagnosisId = diagnoses[0].Id,
                    EduCategoryId = eduCategories[0].Id,
                    SupportTypeId = supportTypes[0].Id,
                    EduStatusId = eduStatuses[0].Id,
                    TeacherPaymentId = teacherPayments[0].Id,
                    OpkvalSupervisionId = supervisions[0].Id,
                    StudentPaymentId = studentPayments[0].Id
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    SpsaCaseNumber = "SC-2023-002",
                    HoursSought = 25,
                    HoursSpent = 12,
                      Comments = new List<Comment>
    {
        new Comment
        {
            Id = Guid.NewGuid(),
            CommentText = new SensitiveString("Making good progress"),
            EntityType = "SpsaCase",
            CreatedAt = DateTime.UtcNow.AddMonths(-2)
        }
    },
                    IsActive = true,
                    ApplicationDate = DateTime.UtcNow.AddMonths(-2),
                    LatestReapplicationDate = null,
                    StudentId = students[1].Id,
                    Student = students[1], // Add this line
                    SupportingTeacherId = teachers[1].Id,
                    AppliedPeriodId = periods[1].Id,
                    DiagnosisId = diagnoses[1].Id,
                    EduCategoryId = eduCategories[1].Id,
                    SupportTypeId = supportTypes[1].Id,
                    EduStatusId = eduStatuses[0].Id,
                    TeacherPaymentId = teacherPayments[1].Id,
                    OpkvalSupervisionId = supervisions[1].Id,
                    StudentPaymentId = studentPayments[1].Id
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    SpsaCaseNumber = "SC-2023-003",
                    HoursSought = 40,
                    HoursSpent = 40,
                     Comments = new List<Comment>
    {
        new Comment
        {
            Id = Guid.NewGuid(),
            CommentText = new SensitiveString("Support completed successfully"),
            EntityType = "SpsaCase",
            CreatedAt = DateTime.UtcNow.AddMonths(-5)
        }
    },
                    IsActive = false,
                    ApplicationDate = DateTime.UtcNow.AddMonths(-5),
                    LatestReapplicationDate = DateTime.UtcNow.AddMonths(-2),
                    StudentId = students[2].Id,
                    Student = students[2],
                    SupportingTeacherId = teachers[2].Id,
                    AppliedPeriodId = periods[0].Id,
                    DiagnosisId = diagnoses[2].Id,
                    EduCategoryId = eduCategories[2].Id,
                    SupportTypeId = supportTypes[2].Id,
                    EduStatusId = eduStatuses[2].Id,
                    TeacherPaymentId = teacherPayments[2].Id,
                    OpkvalSupervisionId = supervisions[2].Id,
                    StudentPaymentId = studentPayments[2].Id
                }
            };

            await context.SpsaCases.AddRangeAsync(cases);
            await context.SaveChangesAsync();
            return cases;
        }
    }
}