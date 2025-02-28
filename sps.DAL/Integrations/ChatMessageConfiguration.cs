using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sps.Domain.Model.Entities;

namespace sps.DAL.Configurations
{
    public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.HasKey(cm => cm.Id);
            
            builder.Property(cm => cm.Message).IsRequired();
            builder.Property(cm => cm.CreatedAt).IsRequired();
            builder.Property(cm => cm.IsRead).IsRequired();

            builder.HasOne(cm => cm.Sender)
                .WithMany(up => up.SentMessages)
                .HasForeignKey(cm => cm.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cm => cm.Receiver)
                .WithMany(up => up.ReceivedMessages)
                .HasForeignKey(cm => cm.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}