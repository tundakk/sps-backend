using sps.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace sps.DAL.Configurations
{
    public class ServiceMessageConfiguration : IEntityTypeConfiguration<ServiceMessage>
    {
        public void Configure(EntityTypeBuilder<ServiceMessage> builder)
        {
            builder.HasKey(sm => sm.Id);
            builder.Property(sm => sm.Message).IsRequired();
            builder.Property(sm => sm.CreatedAt).IsRequired();
            builder.Property(sm => sm.IsRead).IsRequired();
        }
    }
}