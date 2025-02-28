using sps.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace sps.DAL.Configurations
{
    public class LostAndFoundConfiguration : IEntityTypeConfiguration<LostAndFound>
    {
        public void Configure(EntityTypeBuilder<LostAndFound> builder)
        {
            builder.HasKey(lf => lf.Id);

            // Additional property configurations if needed

            // Seed data if necessary
            // builder.HasData( ... );
        }
    }
}