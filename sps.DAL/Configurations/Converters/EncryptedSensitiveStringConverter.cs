using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using sps.Domain.Model.Services;
using sps.Domain.Model.ValueObjects;

namespace sps.DAL.Configurations.Converters
{
    /// <summary>
    /// Converts between SensitiveString value objects and encrypted string representations for database storage.
    /// </summary>
    public class EncryptedSensitiveStringConverter : ValueConverter<SensitiveString, string>
    {
        public EncryptedSensitiveStringConverter(IEncryptionService encryptionService)
            : base(
                // To database: Extract value from SensitiveString and encrypt it
                v => encryptionService.Encrypt(v.Value),
                // From database: Decrypt the stored value and create a new SensitiveString
                v => new SensitiveString(encryptionService.Decrypt(v)))
        {
        }
    }
}