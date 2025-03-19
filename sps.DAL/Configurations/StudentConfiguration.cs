using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sps.DAL.Configurations.Converters;
using sps.Domain.Model.Entities;
using sps.Domain.Model.Services;

namespace sps.DAL.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        private readonly IEncryptionService _encryptionService;

        public StudentConfiguration(IEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
        }

        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.StudentNumber).IsRequired();

            // Configure encrypted CPR number
            builder.Property(s => s.CPRNumber)
                .HasConversion(new EncryptedCPRNumberConverter(_encryptionService))
                .IsRequired();

            // Configure encrypted Name
            builder.Property(s => s.Name)
                .UseEncryption(_encryptionService)
                .IsRequired();

            // Navigation properties configuration
            builder.HasOne(s => s.StartPeriod)
                .WithMany(p => p.Students)
                .HasForeignKey(s => s.StartPeriodId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(s => s.Education)
                .WithMany(e => e.Students)
                .HasForeignKey(s => s.EducationId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            // Configure relationship with Comments
            builder.HasMany(s => s.Comments)
                .WithOne(c => c.Student)
                .HasForeignKey(c => c.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}