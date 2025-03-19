using Microsoft.EntityFrameworkCore;
using sps.DAL.Configurations;
using sps.Domain.Model.Services;

namespace sps.DAL.DataModel
{
    public static class DbContextEncryptionExtensions
    {
        public static ModelBuilder UseEncryption(this ModelBuilder modelBuilder, IEncryptionService encryptionService)
        {
            // Apply configurations for entities with sensitive data
            modelBuilder.ApplyConfiguration(new StudentConfiguration(encryptionService));
            modelBuilder.ApplyConfiguration(new SupportingTeacherConfiguration(encryptionService));
            modelBuilder.ApplyConfiguration(new StudentPaymentConfiguration(encryptionService));
            modelBuilder.ApplyConfiguration(new TeacherPaymentConfiguration(encryptionService));
            modelBuilder.ApplyConfiguration(new CommentConfiguration(encryptionService));


            return modelBuilder;
        }
    }
}