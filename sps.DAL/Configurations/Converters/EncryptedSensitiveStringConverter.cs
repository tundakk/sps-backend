using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using sps.Domain.Model.Services;
using sps.Domain.Model.ValueObjects;

namespace sps.DAL.Configurations.Converters
{
    public class EncryptedSensitiveStringConverter : ValueConverter<SensitiveString, string>
    {
        public EncryptedSensitiveStringConverter(IEncryptionService encryptionService)
            : base(
                v => encryptionService.Encrypt(v.Value),
                v => new SensitiveString(encryptionService.Decrypt(v)))
        {
        }
    }
}