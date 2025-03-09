using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sps.Domain.Model.Entities;

namespace sps.DAL.Configurations
{
    public class SpsaCaseCommentConfiguration : IEntityTypeConfiguration<SpsaCaseComment>
    {
        public void Configure(EntityTypeBuilder<SpsaCaseComment> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CommentText)
                .UseEncryption()
                .IsRequired();

            builder.Property(c => c.CreatedAt)
                .IsRequired();

            builder.Property(c => c.SpsaCaseId)
                .IsRequired();

            builder.HasOne(c => c.SpsaCase)
                .WithMany(sc => sc.Comments)
                .HasForeignKey(c => c.SpsaCaseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}