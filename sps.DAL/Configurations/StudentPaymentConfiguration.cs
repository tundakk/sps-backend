using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
            builder.HasKey(sp => sp.Id);
            
            builder.Property(sp => sp.Date)
                .IsRequired();
            
            builder.Property(sp => sp.Amount)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(sp => sp.AccountNumber)
                .UseEncryption(_encryptionService)
                .IsRequired();

            builder.Property(sp => sp.VoucherText)
                .IsRequired(false);

            builder.Property(sp => sp.CompleteVoucherText)
                .IsRequired(false);

            builder.Property(sp => sp.ExternalVoucherNumber)
                .IsRequired(false);

            // Configure relationship with SupportType
            builder.HasOne(sp => sp.SupportType)
                .WithMany(st => st.StudentPayments)
                .HasForeignKey(sp => sp.SupportTypeId)
                .OnDelete(DeleteBehavior.SetNull);

            // Configure relationship with Comments
            builder.HasMany(sp => sp.Comments)
                .WithOne(c => c.StudentPayment)
                .HasForeignKey(c => c.StudentPaymentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}