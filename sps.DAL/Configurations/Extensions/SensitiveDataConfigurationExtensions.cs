using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sps.DAL.Configurations.Converters;
using sps.Domain.Model.Services;
using sps.Domain.Model.ValueObjects;

namespace sps.DAL.Configurations.Extensions
{
    /// <summary>
    /// Extension methods for configuring sensitive data properties in entity type configurations.
    /// </summary>
    public static class SensitiveDataConfigurationExtensions
    {
        /// <summary>
        /// Configures a CPRNumber property to be encrypted in the database.
        /// </summary>
        public static PropertyBuilder<CPRNumber> UseEncryption(
            this PropertyBuilder<CPRNumber> propertyBuilder,
            IEncryptionService encryptionService)
        {
            return propertyBuilder.HasConversion(
                new EncryptedCPRNumberConverter(encryptionService));
        }

        /// <summary>
        /// Configures a SensitiveString property to be encrypted in the database.
        /// </summary>
        public static PropertyBuilder<SensitiveString> UseEncryption(
            this PropertyBuilder<SensitiveString> propertyBuilder,
            IEncryptionService encryptionService)
        {
            return propertyBuilder.HasConversion(
                new EncryptedSensitiveStringConverter(encryptionService));
        }
    }
}