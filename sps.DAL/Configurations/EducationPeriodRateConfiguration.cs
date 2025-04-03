using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sps.Domain.Model.Entities;

namespace sps.DAL.Configurations
{
    public class EducationPeriodRateConfiguration : IEntityTypeConfiguration<EducationPeriodRate>
    {
        public void Configure(EntityTypeBuilder<EducationPeriodRate> builder)
        {
            builder.HasKey(epr => epr.Id);

            // Configure properties
            builder.Property(epr => epr.Amount).IsRequired().HasColumnType("decimal(18, 2)");

            // Configure relationships
            builder.HasOne(epr => epr.Period)
                .WithMany(p => p.EducationPeriodRates)
                .HasForeignKey(epr => epr.PeriodId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(epr => epr.EducationalProgram)
                .WithMany(e => e.EducationPeriodRates)
                .HasForeignKey(epr => epr.EducationalProgramId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Add a unique constraint for the combination of PeriodId and EducationalProgramId
            builder.HasIndex(epr => new { epr.PeriodId, epr.EducationalProgramId }).IsUnique();
        }
    }
}