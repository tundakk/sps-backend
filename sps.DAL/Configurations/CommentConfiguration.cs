using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sps.Domain.Model.Entities;
using sps.Domain.Model.Services;

namespace sps.DAL.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        private readonly IEncryptionService _encryptionService;

        public CommentConfiguration(IEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
        }

        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ComplexProperty(c => c.CommentText, b =>
            {
                b.Property(s => s.Value)
                    .HasColumnName("CommentText")
                    .HasConversion(
                        v => _encryptionService.Encrypt(v),
                        v => _encryptionService.Decrypt(v));
            });

            builder.Property(c => c.CreatedAt)
                .IsRequired();

            builder.Property(c => c.CreatedBy)
                .IsRequired(false)
                .HasMaxLength(256);

            builder.Property(c => c.EntityType)
                .IsRequired()
                .HasMaxLength(50);

            // Configure relationships
            builder.HasOne(c => c.SpsaCase)
                .WithMany(s => s.Comments)
                .HasForeignKey(c => c.SpsaCaseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Student)
                .WithMany(s => s.Comments)
                .HasForeignKey(c => c.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.TeacherPayment)
                .WithMany(s => s.Comments)
                .HasForeignKey(c => c.TeacherPaymentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.StudentPayment)
                .WithMany(s => s.Comments)
                .HasForeignKey(c => c.StudentPaymentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.OpkvalSupervision)
                .WithMany(s => s.Comments)
                .HasForeignKey(c => c.OpkvalSupervisionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Add a check constraint to ensure only one foreign key is set
            builder.ToTable(tb => tb.HasCheckConstraint("CK_Comment_SingleEntity",
                "CASE WHEN SpsaCaseId IS NOT NULL THEN 1 ELSE 0 END + " +
                "CASE WHEN StudentId IS NOT NULL THEN 1 ELSE 0 END + " +
                "CASE WHEN TeacherPaymentId IS NOT NULL THEN 1 ELSE 0 END + " +
                "CASE WHEN StudentPaymentId IS NOT NULL THEN 1 ELSE 0 END + " +
                "CASE WHEN OpkvalSupervisionId IS NOT NULL THEN 1 ELSE 0 END = 1"));
        }
    }
}