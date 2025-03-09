using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sps.Domain.Model.Entities;

namespace sps.DAL.Configurations
{
    public class EduStatusConfiguration : IEntityTypeConfiguration<EduStatus>
    {
        public void Configure(EntityTypeBuilder<EduStatus> builder)
        {
            builder.HasKey(es => es.Id);

            // Configure properties
            builder.Property(es => es.Name).IsRequired();

            // Configure relationships
            builder.HasMany(es => es.SpsaCases)
                .WithOne(sc => sc.EduStatus)
                .HasForeignKey(sc => sc.EduStatusId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}