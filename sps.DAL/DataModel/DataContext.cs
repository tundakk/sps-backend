using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using sps.Domain.Model.Entities;

namespace sps.DAL.DataModel
{
    // This context is only for the SPS domain entities
    public class SpsDbContext : DbContext
    {
        public SpsDbContext(DbContextOptions<SpsDbContext> options) : base(options)
        {
        }

        // DbSet properties
        public DbSet<Student> Students { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<SpsaCase> SpsaCases { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<EduCategory> EduCategories { get; set; }
        public DbSet<StudentPayment> StudentPayments { get; set; }
        public DbSet<SupportingTeacher> SupportingTeachers { get; set; }
        public DbSet<OpkvalSupervision> OpkvalSupervisions { get; set; }
        public DbSet<EduStatus> EduStatuses { get; set; }
        public DbSet<EducationPeriodRate> EducationPeriodRates { get; set; }
        public DbSet<TeacherPayment> TeacherPayments { get; set; }
        public DbSet<SupportType> SupportTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations from assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SpsDbContext).Assembly);
        }
    }

    // This context is only for Identity tables
    public class IdentityDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Identity table names
            modelBuilder.Entity<IdentityUser<Guid>>(entity =>
            {
                entity.ToTable(name: "AspNetUsers");
            });

            modelBuilder.Entity<IdentityRole<Guid>>(entity =>
            {
                entity.ToTable(name: "AspNetRoles");
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>(entity =>
            {
                entity.ToTable("AspNetUserRoles");
            });

            modelBuilder.Entity<IdentityUserClaim<Guid>>(entity =>
            {
                entity.ToTable("AspNetUserClaims");
            });

            modelBuilder.Entity<IdentityUserLogin<Guid>>(entity =>
            {
                entity.ToTable("AspNetUserLogins");
            });

            modelBuilder.Entity<IdentityRoleClaim<Guid>>(entity =>
            {
                entity.ToTable("AspNetRoleClaims");
            });

            modelBuilder.Entity<IdentityUserToken<Guid>>(entity =>
            {
                entity.ToTable("AspNetUserTokens");
            });
        }
    }
}