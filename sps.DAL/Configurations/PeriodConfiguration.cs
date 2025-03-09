using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sps.Domain.Model.Entities;

namespace sps.DAL.Configurations
{
    public class PeriodConfiguration : IEntityTypeConfiguration<Period>
    {
        public void Configure(EntityTypeBuilder<Period> builder)
        {
            builder.HasKey(p => p.Id);

            // Configure properties
            builder.Property(p => p.Name).IsRequired();

            // Configure relationships
            builder.HasMany(p => p.EducationPeriodRates)
                .WithOne(epr => epr.Period)
                .HasForeignKey("PeriodId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.SpsaCases)
                .WithOne(sc => sc.AppliedPeriod)
                .HasForeignKey(sc => sc.AppliedPeriodId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(p => p.Students)
                .WithOne(s => s.StartPeriod)
                .HasForeignKey(s => s.StartPeriodId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}