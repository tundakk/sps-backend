using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sps.Domain.Model.Entities;

namespace sps.DAL.Configurations
{
    public class DesiredTimeslotConfiguration : IEntityTypeConfiguration<DesiredTimeslot>
    {
        public void Configure(EntityTypeBuilder<DesiredTimeslot> builder)
        {
            builder.HasKey(dt => dt.Id);

            builder.Property(dt => dt.NotificationSent).IsRequired();
            builder.Property(dt => dt.CreatedAt).IsRequired();

            builder.HasOne(dt => dt.UserProfile)
                .WithMany(up => up.DesiredTimeslots)
                .HasForeignKey(dt => dt.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(dt => dt.Timeslot)
                .WithMany()
                .HasForeignKey(dt => dt.TimeslotId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}