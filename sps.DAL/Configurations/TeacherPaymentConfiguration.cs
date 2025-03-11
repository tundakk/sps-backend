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
            builder.HasKey(tp => tp.Id);
            
            builder.Property(tp => tp.Date)
                .IsRequired();
            
            builder.Property(tp => tp.Amount)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(tp => tp.VoucherText)
                .IsRequired(false);

            builder.Property(tp => tp.CompleteVoucherText)
                .IsRequired(false);

            builder.Property(tp => tp.ExternalVoucherNumber)
                .IsRequired(false);

            // Configure relationship with SupportType
            builder.HasOne(tp => tp.SupportType)
                .WithMany(st => st.TeacherPayments)
                .HasForeignKey(tp => tp.SupportTypeId)
                .OnDelete(DeleteBehavior.SetNull);

            // Configure relationship with Comments
            builder.HasMany(tp => tp.Comments)
                .WithOne(c => c.TeacherPayment)
                .HasForeignKey(c => c.TeacherPaymentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}