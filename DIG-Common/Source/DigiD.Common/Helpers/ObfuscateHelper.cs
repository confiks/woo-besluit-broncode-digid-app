using System;
using System.Text;

namespace DigiD.Common.Helpers
{
    /// <summary>
	/// Helper voor het generen van een byte array key en het obfuscaten van strings met deze key.
	/// </summary>
	public static class ObfuscateHelper
    {
        #region Public Static Methods
        
        /// <summary>
        /// Obfuscate de text met een XOR met de key met als resultaat een een string representatie van een bytearray.
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="byteKey">Key</param>
        public static byte[] Obfuscate(this string text, byte[] byteKey)
        {
            if (byteKey == null)
                throw new ArgumentNullException(nameof(byteKey));

            var decrypted = Encoding.UTF8.GetBytes(text);
            var obfuscated = new byte[decrypted.Length];
            
            for (var i = 0; i < decrypted.Length; i++)
            {
                obfuscated[i] = (byte)(decrypted[i] ^ byteKey[i % byteKey.Length]);
            }

            return obfuscated;
        }

        /// <summary>
        /// DeObfuscate de text gerepresenteerd als obfuscated bytearray met een XOR met de key.
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="key">Key</param>
        public static string DeObfuscate(this byte[] text, byte[] key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if (text == null)
                return string.Empty;

            var result = new byte[text.Length];

            for (var c = 0; c < text.Length; c++)
            {
                result[c] = (byte)(text[c] ^ key[c % key.Length]);
            }

            return Encoding.UTF8.GetString(result);
        }

        #endregion
    }
}
