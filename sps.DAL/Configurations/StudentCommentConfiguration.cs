using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sps.Domain.Model.Entities;

namespace sps.DAL.Configurations
{
    public class StudentCommentConfiguration : IEntityTypeConfiguration<StudentComment>
    {
        public void Configure(EntityTypeBuilder<StudentComment> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CommentText)
                .UseEncryption()
                .IsRequired();

            builder.Property(c => c.CreatedAt)
                .IsRequired();

            builder.Property(c => c.StudentId)
                .IsRequired();

            builder.HasOne(c => c.Student)
                .WithMany(s => s.Comments)
                .HasForeignKey(c => c.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}