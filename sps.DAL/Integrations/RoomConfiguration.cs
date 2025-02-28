using sps.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace sps.DAL.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name).IsRequired();
            builder.Property(r => r.Location).IsRequired();
            builder.Property(r => r.IsAvailable).IsRequired();
            builder.Property(r => r.MaxCapacity).IsRequired();

            builder.HasMany(r => r.Timeslots)
                .WithOne(t => t.Room)
                .HasForeignKey(t => t.RoomId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}