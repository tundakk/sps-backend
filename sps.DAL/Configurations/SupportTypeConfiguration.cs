using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sps.Domain.Model.Entities;

namespace sps.DAL.Configurations
{
    public class SupportTypeConfiguration : IEntityTypeConfiguration<SupportType>
    {
        public void Configure(EntityTypeBuilder<SupportType> builder)
        {
            builder.HasKey(st => st.Id);

            // Configure properties
            builder.Property(st => st.Name).IsRequired();

            // Configure relationships
            builder.HasMany(st => st.SpsaCases)
                .WithOne(sc => sc.SupportType)
                .HasForeignKey(sc => sc.SupportTypeId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(st => st.TeacherPayments)
                .WithOne(tp => tp.SupportType)
                .HasForeignKey("SupportTypeId")
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(st => st.StudentPayments)
                .WithOne(sp => sp.SupportType)
                .HasForeignKey("SupportTypeId")
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}