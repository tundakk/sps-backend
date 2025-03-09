using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sps.Domain.Model.Entities;

namespace sps.DAL.Configurations
{
    public class TeacherPaymentCommentConfiguration : IEntityTypeConfiguration<TeacherPaymentComment>
    {
        public void Configure(EntityTypeBuilder<TeacherPaymentComment> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CommentText)
                .UseEncryption()
                .IsRequired();

            builder.Property(c => c.CreatedAt)
                .IsRequired();

            builder.Property(c => c.TeacherPaymentId)
                .IsRequired();

            builder.HasOne(c => c.TeacherPayment)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.TeacherPaymentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}