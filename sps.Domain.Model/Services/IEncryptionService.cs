namespace sps.Domain.Model.Services
{
    /// <summary>
    /// Service for encrypting and decrypting sensitive data.
    /// </summary>
    public interface IEncryptionService
    {
        /// <summary>
        /// Encrypts the specified plaintext.
        /// </summary>
        /// <param name="plaintext">The plain text to encrypt.</param>
        /// <returns>The encrypted data as a string.</returns>
        string Encrypt(string plaintext);

        /// <summary>
        /// Decrypts the specified ciphertext.
        /// </summary>
        /// <param name="ciphertext">The encrypted text to decrypt.</param>
        /// <returns>The decrypted plaintext.</returns>
        string Decrypt(string ciphertext);
    }
}