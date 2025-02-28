using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sps.Domain.Model.Entities;

namespace sps.DAL.Configurations
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.ApartmentNumber).IsRequired();
            builder.Property(u => u.EmailOptOut).HasDefaultValue(false);
            builder.Property(u => u.SmsOptOut).HasDefaultValue(false);

            // Configure relationships
            builder.HasOne(up => up.User)
                .WithOne()
                .HasForeignKey<UserProfile>(up => up.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(up => up.Bookings)
                .WithOne(b => b.UserProfile)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(up => up.DesiredTimeslots)
                .WithOne(dt => dt.UserProfile)
                .HasForeignKey(dt => dt.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}