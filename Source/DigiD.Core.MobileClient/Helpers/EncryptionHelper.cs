using System.IO;
using System.Security.Cryptography;
using DigiD.Common.Helpers;
using DigiD.Common.NFC.Helpers;

namespace DigiD.Helpers
{
    internal static class EncryptionHelper
	{
        /// <summary>
        /// Will encrypt data and return result as byte[]
        /// </summary>
        /// <param name="data"></param>
        /// <param name="iv"></param>
        /// <returns>Encrypted data as byte[]</returns>
        internal static string Encrypt(string data, byte[] iv, byte[] key)
        {
            using (var cipher = Aes.Create())
            {
                cipher.Mode = CipherMode.CBC;
                cipher.KeySize = 256;

                using (var encryptor = cipher.CreateEncryptor(key, iv))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            var result = data.GetBytes();
                            cryptoStream.Write(result, 0, result.Length);
                            cryptoStream.FlushFinalBlock();

                            return memoryStream.ToArray().ToBase16();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Will decrypt data and return result as byte[]
        /// </summary>
        /// <param name="data"></param>
        /// <param name="iv"></param>
        /// <param name="key"></param>
        /// <returns>Decrypt data as byte[]</returns>
        internal static string Decrypt(string data, byte[] iv, byte[] key)
        {
            using (var aes = Aes.Create())
            {
                aes.Mode = CipherMode.CBC;
                aes.KeySize = 256;

                using (var decryptor = aes.CreateDecryptor(key, iv))
                {
                    var cipherText = data.FromBase16();
                    using (var memoryStream = new MemoryStream(cipherText))
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (var streamReader = new StreamReader(cryptoStream))  
                            {  
                                return streamReader.ReadToEnd();  
                            } 
                        }
                    }
                }
            }
        }
    }
}
