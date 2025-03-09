using Microsoft.EntityFrameworkCore;
using sps.Domain.Model.Entities;
using sps.Domain.Model.Services;
using System.Reflection;

namespace sps.DAL.DataModel
{
    public class SpsDbContext : DbContext
    {
        private readonly IEncryptionService _encryptionService;

        public SpsDbContext(
            DbContextOptions<SpsDbContext> options,
            IEncryptionService encryptionService) 
            : base(options)
        {
            _encryptionService = encryptionService;
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<StudentComment> StudentComments { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<SpsaCase> SpsaCases { get; set; }
        public DbSet<SpsaCaseComment> SpsaCaseComments { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<EduCategory> EduCategories { get; set; }
        public DbSet<StudentPayment> StudentPayments { get; set; }
        public DbSet<StudentPaymentComment> StudentPaymentComments { get; set; }
        public DbSet<TeacherPayment> TeacherPayments { get; set; }
        public DbSet<TeacherPaymentComment> TeacherPaymentComments { get; set; }
        public DbSet<OpkvalSupervision> OpkvalSupervisions { get; set; }
        public DbSet<OpkvalSupervisionComment> OpkvalSupervisionComments { get; set; }
        public DbSet<SupportingTeacher> SupportingTeachers { get; set; }
        public DbSet<EduStatus> EduStatuses { get; set; }
        public DbSet<EducationPeriodRate> EducationPeriodRates { get; set; }
        public DbSet<SupportType> SupportTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply encryption configurations
            modelBuilder.UseEncryption(_encryptionService);

            // Apply all configurations from assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}