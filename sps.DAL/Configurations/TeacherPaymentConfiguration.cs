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
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Date)
                .IsRequired();

            builder.Property(p => p.Amount)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(p => p.Comment)
                .UseEncryption(_encryptionService);

            builder.Property(p => p.ExternalVoucherNumber)
                .HasMaxLength(50);

            builder.HasOne(p => p.SupportType)
                .WithMany(st => st.TeacherPayments)
                .HasForeignKey(p => p.SupportTypeId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(p => p.SpsaCases)
                .WithOne(c => c.TeacherPayment)
                .HasForeignKey(c => c.TeacherPaymentId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}