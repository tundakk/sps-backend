using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sps.Domain.Model.Entities;

namespace sps.DAL.Configurations
{
    public class PlaceConfiguration : IEntityTypeConfiguration<Place>
    {
        public void Configure(EntityTypeBuilder<Place> builder)
        {
            builder.HasKey(p => p.Id);

            // Configure properties
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.PlaceNumber).IsRequired();
            builder.Property(p => p.Alias).IsRequired(false);

            // Configure relationships
            builder.HasMany(p => p.SupportingTeachers)
                .WithOne(st => st.Place)
                .HasForeignKey(st => st.PlacesId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}