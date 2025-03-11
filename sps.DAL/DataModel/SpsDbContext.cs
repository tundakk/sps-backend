using Microsoft.EntityFrameworkCore;
using sps.Domain.Model.Entities;
using sps.Domain.Model.Services;
using System.Reflection;

namespace sps.DAL.DataModel
{
    public class SpsDbContext : DbContext
    {
        private readonly IEncryptionService _encryptionService;

        public SpsDbContext(DbContextOptions<SpsDbContext> options, IEncryptionService encryptionService) 
            : base(options)
        {
            _encryptionService = encryptionService;
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<EduCategory> EduCategories { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<SpsaCase> SpsaCases { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<StudentPayment> StudentPayments { get; set; }
        public DbSet<TeacherPayment> TeacherPayments { get; set; }
        public DbSet<OpkvalSupervision> OpkvalSupervisions { get; set; }
        public DbSet<SupportingTeacher> SupportingTeachers { get; set; }
        public DbSet<EduStatus> EduStatuses { get; set; }
        public DbSet<EducationPeriodRate> EducationPeriodRates { get; set; }
        public DbSet<SupportType> SupportTypes { get; set; }

        public DbSet<Diagnosis> Diagnoses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
               // Configure the SupportRate property with precision and scale
            modelBuilder.Entity<SpsaCase>()
                .Property(c => c.SupportRate)
                .HasPrecision(18, 2); // Using common money precision/scale - adjust if needed
            
            // Use encryption for sensitive data
            modelBuilder.UseEncryption(_encryptionService);

            // Apply all configurations from assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}