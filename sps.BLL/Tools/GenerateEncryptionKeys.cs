using sps.BLL.Services.Utilities;

namespace sps.BLL.Tools
{
    public static class GenerateEncryptionKeys
    {
        public static void Main()
        {
            var (key, iv) = EncryptionKeyGenerator.GenerateNewKeyAndIV();

            Console.WriteLine("Generated new encryption keys:");
            Console.WriteLine($"Key: {key}");
            Console.WriteLine($"IV: {iv}");
            Console.WriteLine("\nUpdate these values in your appsettings.json file under the Encryption section.");
        }
    }
}