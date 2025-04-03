using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using sps.Domain.Model.Services;
using sps.Domain.Model.ValueObjects;

namespace sps.DAL.Configurations.Converters
{
    /// <summary>
    /// Converts between CPRNumber value objects and encrypted string representations for database storage.
    /// </summary>
    public class EncryptedCPRNumberConverter : ValueConverter<CPRNumber, string>
    {
        public EncryptedCPRNumberConverter(IEncryptionService encryptionService)
            : base(
                // To database: Extract value from CPRNumber and encrypt it
                v => encryptionService.Encrypt(v.Value),
                // From database: Decrypt the stored value and create a new CPRNumber
                v => new CPRNumber(encryptionService.Decrypt(v)))
        {
        }
    }
}