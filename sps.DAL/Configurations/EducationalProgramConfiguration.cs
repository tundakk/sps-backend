using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sps.Domain.Model.Entities;

namespace sps.DAL.Configurations
{
    public class EducationalProgramConfiguration : IEntityTypeConfiguration<EducationalProgram>
    {
        public void Configure(EntityTypeBuilder<EducationalProgram> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.ProgramCode)
                .HasMaxLength(50);

            builder.Property(e => e.Alias)
                .HasMaxLength(100);

            // Relationships
            builder.HasOne(e => e.EduCategory)
                .WithMany()
                .HasForeignKey(e => e.EduCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.Students)
                .WithOne(s => s.EducationalProgram)
                .HasForeignKey(s => s.EducationalProgramId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(e => e.SupportingTeachers)
                .WithOne(t => t.EducationalProgram)
                .HasForeignKey(t => t.EducationalProgramId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(e => e.EducationPeriodRates)
                .WithOne()
                .HasForeignKey("EducationalProgramId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}