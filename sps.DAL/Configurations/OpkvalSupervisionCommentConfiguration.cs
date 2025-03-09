using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sps.Domain.Model.Entities;

namespace sps.DAL.Configurations
{
    public class OpkvalSupervisionCommentConfiguration : IEntityTypeConfiguration<OpkvalSupervisionComment>
    {
        public void Configure(EntityTypeBuilder<OpkvalSupervisionComment> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CommentText)
                .UseEncryption()
                .IsRequired();

            builder.Property(c => c.CreatedAt)
                .IsRequired();

            builder.Property(c => c.OpkvalSupervisionId)
                .IsRequired();

            builder.HasOne(c => c.OpkvalSupervision)
                .WithMany(s => s.Comments)
                .HasForeignKey(c => c.OpkvalSupervisionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}