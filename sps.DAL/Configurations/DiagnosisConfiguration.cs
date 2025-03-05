using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sps.Domain.Model.Entities;

namespace sps.DAL.Configurations
{
    public class DiagnosisConfiguration : IEntityTypeConfiguration<Diagnosis>
    {
        public void Configure(EntityTypeBuilder<Diagnosis> builder)
        {
            builder.HasKey(d => d.Id);

            // Configure properties
            builder.Property(d => d.Name).IsRequired();

            // Configure relationships
            builder.HasMany(d => d.SpsaCases)
                .WithOne(sc => sc.Diagnosis)
                .HasForeignKey(sc => sc.DiagnosisId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}