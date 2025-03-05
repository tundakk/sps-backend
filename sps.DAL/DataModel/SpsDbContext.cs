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

            // Apply encryption configurations
            modelBuilder.UseEncryption(_encryptionService);

            // Apply remaining configurations that don't involve encryption
            var configTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.GetInterfaces()
                    .Any(i => i.IsGenericType && 
                             i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)) &&
                    !t.Name.EndsWith("Configuration"));

            foreach (var type in configTypes)
            {
                var config = Activator.CreateInstance(type);
                if (config != null)
                {
                    modelBuilder.ApplyConfiguration((dynamic)config);
                }
            }
        }
    }
}