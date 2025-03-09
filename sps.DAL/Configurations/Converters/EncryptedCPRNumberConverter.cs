using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using sps.Domain.Model.Services;
using sps.Domain.Model.ValueObjects;

namespace sps.DAL.Configurations.Converters
{
    public class EncryptedCPRNumberConverter : ValueConverter<CPRNumber, string>
    {
        public EncryptedCPRNumberConverter(IEncryptionService encryptionService)
            : base(
                v => encryptionService.Encrypt(v.Value),
                v => new CPRNumber(encryptionService.Decrypt(v)))
        {
        }
    }
}