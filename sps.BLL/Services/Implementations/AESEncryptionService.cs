using Microsoft.Extensions.Configuration;
using sps.Domain.Model.Services;
using System.Security.Cryptography;

namespace sps.BLL.Services.Implementations
{
    public class AESEncryptionService : IEncryptionService
    {
        private readonly string _key;
        private readonly string _iv;

        public AESEncryptionService(IConfiguration configuration)
        {
            _key = configuration["Encryption:Key"] ?? throw new ArgumentNullException("Encryption:Key not configured");
            _iv = configuration["Encryption:IV"] ?? throw new ArgumentNullException("Encryption:IV not configured");
        }

        public string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return plainText;

            using var aes = Aes.Create();
            aes.Key = Convert.FromBase64String(_key);
            aes.IV = Convert.FromBase64String(_iv);

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using var msEncrypt = new MemoryStream();
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(plainText);
            }

            return Convert.ToBase64String(msEncrypt.ToArray());
        }

        public string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText)) return cipherText;

            using var aes = Aes.Create();
            aes.Key = Convert.FromBase64String(_key);
            aes.IV = Convert.FromBase64String(_iv);

            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            try
            {
                var cipherBytes = Convert.FromBase64String(cipherText);
                using var msDecrypt = new MemoryStream(cipherBytes);
                using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                using var srDecrypt = new StreamReader(csDecrypt);

                return srDecrypt.ReadToEnd();
            }
            catch
            {
                return cipherText; // Return original if decryption fails
            }
        }
    }
}