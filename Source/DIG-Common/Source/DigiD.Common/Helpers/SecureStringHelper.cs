using System;
using System.Globalization;
using System.Linq;
using System.Security;
using DigiD.Common.EID.Helpers;

namespace DigiD.Common.Helpers
{
    public static class SecureStringHelper
    {
        public static SecureString ToSecureString(this string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var result = new SecureString();
            foreach (var c in value)
                result.AppendChar(c);

            return result;
        }

        public static bool IsIncreasing(this SecureString s)
        {
            return "01234567890".Contains(s.ToPlain());
        }

        public static bool IsDecreasing(this SecureString s)
        {
            return "09876543210".Contains(s.ToPlain());
        }

        public static bool AreEqual(this SecureString s)
        {
            return s.ToPlain().Distinct().Count() == 1;
        }

        public static void AppendChar(this SecureString secureString, int value)
        {
            if (secureString == null)
                throw new ArgumentNullException(nameof(secureString));

            foreach (var c in value.ToString(CultureInfo.InvariantCulture))
                secureString.AppendChar(c);
        }

        public static void RemoveChar(this SecureString secureString)
        {
            if (secureString == null)
                throw new ArgumentNullException(nameof(secureString));

            secureString.RemoveAt(secureString.Length - 1);
        }
    }
}
