using Android.Content;
using Android.Nfc;
using Android.OS;

namespace DigiD.Droid.Helpers
{
    internal static class NfcHelper
    {
        internal static readonly NfcAdapter NFCAdapter = NfcAdapter.GetDefaultAdapter(Xamarin.Essentials.Platform.CurrentActivity);

        internal static void OpenSettings()
        {
            Xamarin.Essentials.Platform.CurrentActivity.StartActivity(Build.VERSION.SdkInt >= BuildVersionCodes.JellyBean
                ? new Intent(Android.Provider.Settings.ActionNfcSettings)
                : new Intent(Android.Provider.Settings.ActionWirelessSettings));
        }
    }
}
