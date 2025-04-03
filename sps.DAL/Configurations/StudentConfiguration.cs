using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sps.DAL.Configurations.Extensions;
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
            builder.HasKey(e => e.Id);

            builder.Property(e => e.StudentNumber)
                .IsRequired();

            // Apply encryption to CPRNumber 
            builder.Property(e => e.CPRNumber)
                .IsRequired()
                .UseEncryption(_encryptionService);

            // Apply encryption to Name
            builder.Property(e => e.Name)
                .IsRequired()
                .UseEncryption(_encryptionService);

            builder.Property(e => e.CreatedAt)
                .IsRequired();

            builder.Property(e => e.UpdatedAt)
                .IsRequired();

            // Relationships
            builder.HasOne(e => e.EducationalProgram)
                .WithMany(ep => ep.Students)
                .HasForeignKey(e => e.EducationalProgramId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.StartPeriod)
                .WithMany()
                .HasForeignKey(e => e.StartPeriodId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.Comments)
                .WithOne()
                .HasForeignKey("StudentId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.SpsaCases)
                .WithOne(sc => sc.Student)
                .HasForeignKey(sc => sc.StudentId);
        }
    }
}