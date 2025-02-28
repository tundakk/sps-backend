using sps.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace sps.DAL.Configurations
{
    public class TimeslotConfiguration : IEntityTypeConfiguration<Timeslot>
    {
        public void Configure(EntityTypeBuilder<Timeslot> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.IsAvailable).IsRequired();
            builder.Property(t => t.CreatedAt).IsRequired();
            builder.Property(t => t.UpdatedAt).IsRequired();

            builder.HasOne(t => t.Room)
                .WithMany()
                .HasForeignKey(t => t.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(t => t.Bookings)
                .WithOne(b => b.Timeslot)
                .HasForeignKey(b => b.TimeslotId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(t => t.DesiredTimeslots)
                .WithOne(dt => dt.Timeslot)
                .HasForeignKey(dt => dt.TimeslotId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.OwnsOne(t => t.SlotTime, sa =>
            {
                sa.Property(p => p.Start).HasColumnName("StartTime");
                sa.Property(p => p.End).HasColumnName("EndTime");
            });
        }
    }
}