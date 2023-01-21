using System;
using System.Linq;
using System.Text;

namespace DigiD.Common.NFC.Helpers
{
    public static class StringHelper
    {
        public static string ToHexString(this byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            return BitConverter.ToString(data);
        }

        public static string FromBCD(this byte[] bytes)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            var temp = new StringBuilder(bytes.Length * 2);

            foreach (var t in bytes)
            {
                temp.Append((byte)((t & 0xf0) >> 4));
                temp.Append((byte)(t & 0x0f));
            }

            return temp.ToString();
        }

        public static string ToBase64(this byte[] ba)
        {
            if (ba == null)
                return string.Empty;

            return Convert.ToBase64String(ba);
        }

        public static string ToBase16(this byte[] ba)
        {
            if (ba == null)
                return string.Empty;

            var hex = BitConverter.ToString(ba);
            return hex.Replace("-", "").ToLowerInvariant();
        }

        public static byte[] HexToByteArray(this string hex)
        {
            if (hex == null)
                throw new ArgumentNullException(nameof(hex));

            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }
    }
}
