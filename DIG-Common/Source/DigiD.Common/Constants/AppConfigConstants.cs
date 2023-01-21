using System;
using System.Collections.Generic;
using DigiD.Common.Enums;
using DigiD.Common.Models;
using Xamarin.Forms;

namespace DigiD.Common.Constants
{
    public static class AppConfigConstants
    {
        private const string iOSPublicFile = "SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS"; //SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS
        private const string AndroidPublicFile = "SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS"; //SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS

        public const string A11YEffectGroupName = "DigiD.A11Y.Effect";

        internal const string HostKey = "SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS";
        internal const string PinKey = "SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS";
        internal static readonly ObfuscatedString PublicFileUrl = new ObfuscatedString(HostKey, Device.RuntimePlatform == Device.Android ? AndroidPublicFile : iOSPublicFile);

        public static ReleaseType ReleaseType => ReleaseType.Productie;
        public static TimeSpan DefaultHttpTimeout => TimeSpan.FromSeconds(60);
        public static TimeSpan SessionTimeout { get; set; } = TimeSpan.FromSeconds(120);
        public static TimeSpan DisplayLockTimeout { get; set; } = TimeSpan.FromSeconds(60);

        public static readonly IReadOnlyList<ObfuscatedString> HostWhiteList = new List<ObfuscatedString>
        {
#if PROD
            new ObfuscatedString(HostKey, "SSSSSSSSSSSS"), //digid.nl
#elif PREPROD
            new ObfuscatedString(HostKey, "SSSSSSSSSSSS"), //digid.nl
            new ObfuscatedString(HostKey, "SSSSSSSSSSSSSSSSSSSSSSSS"), //SSSSSSSSSSSSSSSSS
            new ObfuscatedString(HostKey, "SSSSSSSSSSSSSSSSSSSSSSSS"), //SSSSSSSSSSSSSSSSS
#else
            new ObfuscatedString(HostKey, "SSSSSSSSSSSS"), //digid.nl
            new ObfuscatedString(HostKey, "SSSSSSSSSSSSSSSSSSSSSSSS"), //SSSSSSSSSSSSSSSSS
            new ObfuscatedString(HostKey, "SSSSSSSSSSSSSSSSSSSSSSSS"), //SSSSSSSSSSSSSSSSS
            new ObfuscatedString(HostKey, "SSSSSSSSSSSSSSSS"), //SSSSSSSSSSS
            new ObfuscatedString(HostKey, "SSSSSSSSSSSSSSSS"), //SSSSSSSSSSS
            new ObfuscatedString(HostKey, "SSSSSSSSSSSSSSSS"), //SSSSSSSSSSS
            new ObfuscatedString(HostKey, "SSSSSSSSSSSSSSSS"), //SSSSSSSSSSS
            new ObfuscatedString(HostKey, "SSSSSSSSSSSSSSSS"), //SSSSSSSSSSS
            new ObfuscatedString(HostKey, "SSSSSSSSSSSSSSSS"), //SSSSSSSSSSS
            new ObfuscatedString(HostKey, "SSSSSSSSSSSSSSSS"), //SSSSSSSSSSS
            new ObfuscatedString(HostKey, "SSSSSSSSSSSSSSSS"), //SSSSSSSSSSS
            new ObfuscatedString(HostKey, "SSSSSSSSSSSSSSSS"), //SSSSSSSSSSS
            new ObfuscatedString(HostKey, "SSSSSSSSSSSSSSSS"), //SSSSSSSSSSS
            new ObfuscatedString(HostKey, "SSSSSSSSSSSSSSSS"), //SSSSSSSSSSS
            new ObfuscatedString(HostKey, "SSSSSSSSSSSSSSSSSSSS"), //SSSSSSSSSSSSSSS
#endif
        };

        //SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS
        internal static List<string> Exclusions { get; } = new List<string>
        {
            "SSSSSSSSSSSSSSS",
            "SSSSSSSSSSSSSSSSS",
            "SSSSSSSSSSSSSSSS",
            "SSSSSSSSSSSSSSS",
            "SSSSSSSSSSSSSSS",
            "SSSSSSSSSSSSSSS",
            "SSSSSSSSSSSSSSS",
            "SSSSSSSSSSSSSSS",
            "SSSSSSSSSSSSSSS",
            "SSSSSSSSSSSSSSS",
            "SSSSSSSSSSSSSSS",
            "SSSSSSSSSSSSSSS",
            "SSSSSSSSSSSSSSS",
        };

#if PROD
        public static readonly ObfuscatedString PiwikBaseUrl = new ObfuscatedString(HostKey, "SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS"); //SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS
#else
        public static readonly ObfuscatedString PiwikBaseUrl = new ObfuscatedString(HostKey, "SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS"); //SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS
#endif

        public const int QRCodeValidationTime = 15;
        public const int RatingLimit = 5;
    }
}
