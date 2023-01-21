using System.Text.RegularExpressions;

namespace DigiD.Common.Helpers
{
    public static class ZipcodeHelper
    {
        /// <summary>
        /// Checks if a given string is a valid Postalcode. Based on the dutch postalcode.
        /// </summary>
        /// <param name="zipcode">The <see cref="string"/> to check</param>
        /// <returns>A <see cref="bool"/> indicating whether or not the value is a valid Postalcode</returns>
        public static bool IsValidZipcode(this string zipcode)
        {
            string POSTCODE_EXPRESSION = "^[1-9][0-9]{3}[A-Za-z]{2}$";
            Regex regex = new Regex(POSTCODE_EXPRESSION, RegexOptions.Compiled | RegexOptions.Singleline);

            if (string.IsNullOrEmpty(zipcode))
                return false;

            var match = regex.Match(zipcode);
            return match.Success;
        }
    }
}
