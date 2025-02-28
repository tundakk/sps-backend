using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sps.Domain.Model.Entities;

namespace sps.DAL.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Status).IsRequired();
            builder.Property(b => b.CreatedAt).IsRequired();
            builder.Property(b => b.UpdatedAt).IsRequired();

            builder.HasOne(b => b.UserProfile)
                .WithMany(up => up.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.Timeslot)
                .WithMany()
                .HasForeignKey(b => b.TimeslotId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}