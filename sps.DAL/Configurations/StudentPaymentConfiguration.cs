using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sps.DAL.Configurations.Extensions;
using sps.Domain.Model.Entities;
using sps.Domain.Model.Services;

namespace sps.DAL.Configurations
{
    public class StudentPaymentConfiguration : IEntityTypeConfiguration<StudentPayment>
    {
        private readonly IEncryptionService _encryptionService;

        public StudentPaymentConfiguration(IEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
        }

        public void Configure(EntityTypeBuilder<StudentPayment> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Date)
                .IsRequired();

            // Apply encryption to AccountNumber
            builder.Property(e => e.AccountNumber)
                .IsRequired()
                .UseEncryption(_encryptionService);

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
                .WithOne(c => c.StudentPayment)
                .HasForeignKey(c => c.StudentPaymentId);
        }
    }
}