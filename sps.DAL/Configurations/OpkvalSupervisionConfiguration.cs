using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sps.Domain.Model.Entities;

namespace sps.DAL.Configurations
{
    public class OpkvalSupervisionConfiguration : IEntityTypeConfiguration<OpkvalSupervision>
    {
        public void Configure(EntityTypeBuilder<OpkvalSupervision> builder)
        {
            builder.HasKey(os => os.Id);

            // Configure properties
            builder.Property(os => os.Status).IsRequired();
            builder.Property(os => os.HoursSpentSupervision).IsRequired();
            builder.Property(os => os.HoursSpentOpkvalificering).IsRequired();
            builder.Property(os => os.HoursSought).IsRequired();

            // Configure relationships
            builder.HasMany(os => os.SpsaCases)
                .WithOne(sc => sc.OpkvalSupervision)
                .HasForeignKey(sc => sc.OpkvalSupervisionId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}