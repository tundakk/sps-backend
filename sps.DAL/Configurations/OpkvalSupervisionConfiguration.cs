using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sps.Domain.Model.Entities;

namespace sps.DAL.Configurations
{
    public class OpkvalSupervisionConfiguration : IEntityTypeConfiguration<OpkvalSupervision>
    {
        public void Configure(EntityTypeBuilder<OpkvalSupervision> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.CreateDate)
                .IsRequired();

            builder.Property(s => s.LastUpdatedBy)
                .IsRequired(false);

            builder.Property(s => s.HoursSought)
                .IsRequired(false);

            builder.Property(s => s.SupervisionHoursSpent)
                .IsRequired();

            builder.Property(s => s.QualificationHoursSpent)
                .IsRequired();

            builder.Property(s => s.Status)
                .IsRequired();

            // Configure relationship with Comments
            builder.HasMany(s => s.Comments)
                .WithOne(c => c.OpkvalSupervision)
                .HasForeignKey(c => c.OpkvalSupervisionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure relationship with SpsaCases
            builder.HasMany(s => s.SpsaCases)
                .WithOne(c => c.OpkvalSupervision)
                .HasForeignKey(c => c.OpkvalSupervisionId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}