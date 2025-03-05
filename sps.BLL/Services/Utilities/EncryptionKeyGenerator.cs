using System.Security.Cryptography;

namespace sps.BLL.Services.Utilities
{
    public static class EncryptionKeyGenerator
    {
        public static (string key, string iv) GenerateNewKeyAndIV()
        {
            using var aes = Aes.Create();
            aes.GenerateKey();
            aes.GenerateIV();

            return (
                Convert.ToBase64String(aes.Key),
                Convert.ToBase64String(aes.IV)
            );
        }
    }
}