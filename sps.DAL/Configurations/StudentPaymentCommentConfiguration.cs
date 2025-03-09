using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sps.Domain.Model.Entities;

namespace sps.DAL.Configurations
{
    public class StudentPaymentCommentConfiguration : IEntityTypeConfiguration<StudentPaymentComment>
    {
        public void Configure(EntityTypeBuilder<StudentPaymentComment> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CommentText)
                .UseEncryption()
                .IsRequired();

            builder.Property(c => c.CreatedAt)
                .IsRequired();

            builder.Property(c => c.StudentPaymentId)
                .IsRequired();

            builder.HasOne(c => c.StudentPayment)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.StudentPaymentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}