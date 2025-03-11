using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sps.Domain.Model.Entities;

namespace sps.DAL.Configurations
{
    public class EducationConfiguration : IEntityTypeConfiguration<Education>
    {
        public void Configure(EntityTypeBuilder<Education> builder)
        {
            builder.HasKey(e => e.Id);

            // Configure properties
            builder.Property(e => e.Name).IsRequired();

            // Configure relationships
            builder.HasOne(e => e.EduCategory)
                .WithMany(ec => ec.Educations)  // Specify the navigation property on EduCategory
                .HasForeignKey(e => e.EduCategoryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.EducationPeriodRates)
                .WithOne(epr => epr.Education)
                .HasForeignKey("EducationId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.Students)
                .WithOne(s => s.Education)
                .HasForeignKey(s => s.EducationId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}