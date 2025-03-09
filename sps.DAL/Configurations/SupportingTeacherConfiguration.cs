using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Email)
                .UseEncryption(_encryptionService)
                .IsRequired();

            builder.HasOne(t => t.Place)
                .WithMany(p => p.SupportingTeachers)
                .HasForeignKey(t => t.PlacesId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(t => t.SpsaCases)
                .WithOne(c => c.SupportingTeacher)
                .HasForeignKey(c => c.SupportingTeacherId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}