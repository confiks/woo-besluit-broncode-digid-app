﻿namespace DigiD.Common.Helpers
{
    public static class BsnHelper
    {
        /// <summary>
        /// Checks if a given string is a valid BSN. Based on the '11-proef'.
        /// </summary>
        /// <param name="bsn">The <see cref="string"/> to check</param>
        /// <returns>A <see cref="bool"/> indicating whether or not the value is a valid BSN</returns>
        public static bool IsValidBsn(this string bsn)
        {
            var isNumeric = int.TryParse(bsn, out _);

            if (!isNumeric)
                return false;

            if (bsn?.Length != 9)
            {
                return false;
            }
            

            var i = 9;
            var total = 0;

            for (var j = 0; j < 8; j++)
            {
                var number = bsn[j];
                if (int.TryParse(number.ToString(), out var n))
                    total += n * i;
                else
                    return false;

                i--;
            }
            if (int.TryParse(bsn[8].ToString(), out var rest))
                return total % 11 == rest;

            return false;
        }
    }
}
