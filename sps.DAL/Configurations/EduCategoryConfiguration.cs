using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sps.Domain.Model.Entities;

namespace sps.DAL.Configurations
{
    public class EduCategoryConfiguration : IEntityTypeConfiguration<EduCategory>
    {
        public void Configure(EntityTypeBuilder<EduCategory> builder)
        {
            builder.HasKey(ec => ec.Id);

            // Configure properties
            builder.Property(ec => ec.Name).IsRequired();

            // Configure relationships - update to use EducationalProgram instead of Education
            builder.HasMany(ec => ec.EducationalPrograms)
                .WithOne(e => e.EduCategory)
                .HasForeignKey(e => e.EduCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(ec => ec.SpsaCases)
                .WithOne(sc => sc.EduCategory)
                .HasForeignKey(sc => sc.EduCategoryId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}