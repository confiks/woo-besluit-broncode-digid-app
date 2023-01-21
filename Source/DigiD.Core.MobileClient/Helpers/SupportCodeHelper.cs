using System;
using DigiD.Common.Helpers;
using DigiD.Common.Interfaces;
using DigiD.Common.NFC.Interfaces;
using DigiD.Common.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DigiD.Helpers
{
    public static class SupportCodeHelper
    {
        //SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS

        public static string GenerateSupportCode()
        {
            // First nibble of 4 bits, tussen haakjes het aantal bits dat gebruikt wordt voor het weergeven:
            // Startbit (1): ALTIJD 1
            // Demo (1):     0 = uit; 1 = aan
            // OS (2):       0 = Android, 1 = iOS, 2 = Windows/UWP, 3 = MacOS
            uint supportCode = 0b_1; // Startbit is ALTIJD 1, totale lengte 4x4 bits => 16

            ShiftLeftAndAdd(ref supportCode, 1, DependencyService.Get<IDemoSettings>().IsDemo ? 1U : 0U);

            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    ShiftLeftAndAdd(ref supportCode, 2, 0U);
                    break;
                case Device.iOS:
                    ShiftLeftAndAdd(ref supportCode, 2, 1U);
                    break;
                case Device.UWP:
                    ShiftLeftAndAdd(ref supportCode, 2, 2U);
                    break;
                case Device.macOS:
                    ShiftLeftAndAdd(ref supportCode, 2, 3U);
                    break;
            }

            // Second nibble of 4 bits
            ShiftLeftAndAdd(ref supportCode, 1, DigiD.App.HasNfc ? 1U : 0U);   //NFC aan of aanwezig
            ShiftLeftAndAdd(ref supportCode, 1, DependencyService.Get<INfcService>().IsNFCEnabled ? 1U : 0U);   //Toestemming NFC gebruik

            var cameraPresentOrEnabled = PermissionHelper.CheckCameraPermissions().Result;
            ShiftLeftAndAdd(ref supportCode, 1, cameraPresentOrEnabled ? 1U : 0U);
            var cameraCanBeUsed = cameraPresentOrEnabled;
            ShiftLeftAndAdd(ref supportCode, 1, cameraCanBeUsed ? 1U : 0U);

            //Third nibble of 4 bits DeviceType (4 bits long)
            //For the time being:
            //0 = nvt
            //1 = iPhone 7.0
            //2 = iPad
            ShiftLeftAndAdd(ref supportCode, 4, 0U); // 4 bits op 0 zetten
            var deviceType = DeviceInfo.Model;
            // See also DigiD.iOS.Services.DeviceHardware
            if (deviceType.Equals("iPhone9,1") || deviceType.Equals("iPhone9,3"))
                supportCode |= 1U;
            else if (deviceType.StartsWith("iPad"))
                supportCode |= 2u;

            //Fourth nibble of 4 bits
            // bit 1: on of offline, vliegtuigmodus (1)
            // bit 2: Developer modus (1)
            // bit 3 en 4 for future use (2).
            var access = Connectivity.NetworkAccess;
            
            ShiftLeftAndAdd(ref supportCode, 1, (access == NetworkAccess.None || access == NetworkAccess.Local) ? 0U : 1U);

            uint inDeveloperMode = Device.RuntimePlatform == Device.Android && DependencyService.Get<IDevice>().IsInDeveloperMode ? 1U : 0U;
            ShiftLeftAndAdd(ref supportCode, 1, inDeveloperMode); //developermode aan of uit

            ShiftLeftAndAdd(ref supportCode, 2, 0U);    // for future use, wel ff op 0 zetten.

            var resultBinair =  Convert.ToString(supportCode, toBase: 2);
            var resultHex =  Convert.ToString(supportCode, toBase: 16);

            System.Diagnostics.Debug.WriteLine($"SupportCode: {resultBinair} - {resultHex}");
            return resultHex.ToUpper();
        }

        private static void ShiftLeftAndAdd(ref uint supportCode, short shiftBit, uint value)
        {
            if (shiftBit > 0)
                supportCode <<= shiftBit;
            supportCode |= value;
        }
    }
}
