using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sps.Domain.Model.ValueObjects;

namespace sps.DAL.Configurations
{
    public class SensitiveStringConfiguration : IEntityTypeConfiguration<SensitiveString>
    {
        public void Configure(EntityTypeBuilder<SensitiveString> builder)
        {
            builder.HasNoKey();
            builder.Property(s => s.Value).HasColumnName("Value");
        }
    }
}