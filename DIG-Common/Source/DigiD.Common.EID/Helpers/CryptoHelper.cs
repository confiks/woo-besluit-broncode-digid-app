using System.Security.Cryptography;

namespace DigiD.Common.EID.Helpers
{
    public static class CryptoHelper
    {
        /// <summary>
        /// Genereer een key van 32 bytes gebaseerd op random gegenereerde positieve hele getallen.
        /// </summary>
        public static byte[] GenerateRandom(int length)
        {
            
            using (var random = new RNGCryptoServiceProvider())
            {
                var byteKey = new byte[length];
                random.GetBytes(byteKey);
                return byteKey;
            }
        }
    }
}
