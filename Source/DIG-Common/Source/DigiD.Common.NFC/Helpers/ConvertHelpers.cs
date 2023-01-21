using System;
using System.Globalization;
using System.Linq;

namespace DigiD.Common.NFC.Helpers
{
    public static class ConvertHelpers
    {
        public static int ConvertHexToInt(this byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            return ConvertHexToInt(BitConverter.ToString(data));
        }
        
        public static int ConvertHexToInt(this string hexValue)
        {
            if (hexValue == null)
                throw new ArgumentNullException(nameof(hexValue));

            return int.Parse(hexValue.Replace("-",string.Empty), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
        }

        public static byte[] ConvertHexToBytes(this string hexString)
        {
            if (string.IsNullOrEmpty(hexString))
                return new byte[]{ 0x00 };

            hexString = hexString.Replace("-", string.Empty);

            if (hexString.Length % 2 != 0)
                hexString = "0" + hexString;

            return Enumerable.Range(0, hexString.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hexString.Substring(x, 2), 16))
                .ToArray();
        }
    }
}
