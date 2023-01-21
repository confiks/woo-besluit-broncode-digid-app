using System;
using DigiD.Common.Constants;

namespace DigiD.Common.Models
{
    public class Pin
    {
        public ObfuscatedString Hash { get; }
        public DateTime ExpirationDate { get; }

        public Pin(string hash, DateTime date)
        {
            Hash = new ObfuscatedString(AppConfigConstants.PinKey, hash);
            ExpirationDate = date;
        }
    }
}
