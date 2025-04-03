using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sps.Domain.Model.Entities;
using sps.Domain.Model.Services;

namespace sps.DAL.Configurations
{
    public class TeacherPaymentConfiguration : IEntityTypeConfiguration<TeacherPayment>
    {
        private readonly IEncryptionService _encryptionService;

        public TeacherPaymentConfiguration(IEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
        }

        public void Configure(EntityTypeBuilder<TeacherPayment> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Date)
                .IsRequired();

            builder.Property(e => e.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.ExternalVoucherNumber);

            // Relationships
            builder.HasOne(e => e.SupportType)
                .WithMany()
                .HasForeignKey(e => e.SupportTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.Comments)
                .WithOne(c => c.TeacherPayment)
                .HasForeignKey(c => c.TeacherPaymentId);
        }
    }
}