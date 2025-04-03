using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sps.DAL.Configurations.Extensions;
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
            builder.HasKey(e => e.Id);

            // Apply encryption to CommentText
            builder.Property(e => e.CommentText)
                .IsRequired()
                .UseEncryption(_encryptionService);

            builder.Property(e => e.EntityType)
                .IsRequired();

            builder.Property(e => e.CreatedAt)
                .IsRequired();
            
            // Optional properties
            builder.Property(e => e.CreatedBy);
            builder.Property(e => e.CreatedByUserId);
            
            // Relationships
            builder.HasOne(e => e.SpsaCase)
                .WithMany(c => c.Comments)
                .HasForeignKey(e => e.SpsaCaseId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(e => e.Student)
                .WithMany(s => s.Comments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(e => e.TeacherPayment)
                .WithMany(tp => tp.Comments)
                .HasForeignKey(e => e.TeacherPaymentId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(e => e.StudentPayment)
                .WithMany(sp => sp.Comments)
                .HasForeignKey(e => e.StudentPaymentId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(e => e.OpkvalSupervision)
                .WithMany(os => os.Comments)
                .HasForeignKey(e => e.OpkvalSupervisionId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}