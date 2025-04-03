using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sps.DAL.Configurations.Extensions;
using sps.Domain.Model.Entities;
using sps.Domain.Model.Services;

namespace sps.DAL.Configurations
{
    public class SupportingTeacherConfiguration : IEntityTypeConfiguration<SupportingTeacher>
    {
        private readonly IEncryptionService _encryptionService;

        public SupportingTeacherConfiguration(IEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
        }

        public void Configure(EntityTypeBuilder<SupportingTeacher> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired();

            // Apply encryption to Email
            builder.Property(e => e.Email)
                .IsRequired()
                .UseEncryption(_encryptionService);

            // Relationships
            builder.HasOne(e => e.EducationalProgram)
                .WithMany(ep => ep.SupportingTeachers)
                .HasForeignKey(e => e.EducationalProgramId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.SpsaCases)
                .WithOne(sc => sc.SupportingTeacher)
                .HasForeignKey(sc => sc.SupportingTeacherId);
        }
    }
}