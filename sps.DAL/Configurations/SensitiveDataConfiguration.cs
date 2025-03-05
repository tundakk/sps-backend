using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sps.DAL.Configurations.Converters;
using sps.Domain.Model.Services;
using sps.Domain.Model.ValueObjects;

namespace sps.DAL.Configurations
{
    public static class SensitiveDataConfiguration
    {
        public static PropertyBuilder<SensitiveString> UseEncryption(
            this PropertyBuilder<SensitiveString> propertyBuilder,
            IEncryptionService encryptionService)
        {
            return propertyBuilder
                .HasConversion(new EncryptedSensitiveStringConverter(encryptionService));
        }
    }
}