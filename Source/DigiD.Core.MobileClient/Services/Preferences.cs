using System;
using System.Diagnostics.CodeAnalysis;
using DigiD.Common.Mobile.Helpers;
using DigiD.Common.Settings;
using DigiD.Services;

[assembly: Xamarin.Forms.Dependency(typeof(Preferences))]
namespace DigiD.Services
{
    public class Preferences : IPreferences
    {
        [NotNull]
        public Version CheckedVersion
        {
            get
            {
                if (Xamarin.Essentials.Preferences.ContainsKey(nameof(CheckedVersion)))
                {
                    var value = Xamarin.Essentials.Preferences.Get(nameof(CheckedVersion), Xamarin.Essentials.AppInfo.Version.ToString());
                    return new Version(value);
                }

                return Xamarin.Essentials.AppInfo.Version;
            }
            set => Xamarin.Essentials.Preferences.Set(nameof(CheckedVersion), value.ToString());
        }

        public DateTimeOffset LetterRequestDate
        {
            get => Xamarin.Essentials.Preferences.ContainsKey(nameof(LetterRequestDate)) ? Xamarin.Essentials.Preferences.Get(nameof(LetterRequestDate), 0L).FromUnixTime() : DateTimeOffset.MinValue;
            set => Xamarin.Essentials.Preferences.Set(nameof(LetterRequestDate), value.ToUnixTimeSeconds());
        }

        public bool LetterReRequestAllowed
        {
            get => Xamarin.Essentials.Preferences.ContainsKey(nameof(LetterReRequestAllowed)) ? Xamarin.Essentials.Preferences.Get(nameof(LetterReRequestAllowed), true) : true;
            set => Xamarin.Essentials.Preferences.Set(nameof(LetterReRequestAllowed), value);
        }

        public bool NewMessagesAvailable
        {
            get => Xamarin.Essentials.Preferences.Get(nameof(NewMessagesAvailable), false);
            set => Xamarin.Essentials.Preferences.Set(nameof(NewMessagesAvailable), value);
        }

        public bool M17PopupShown
        {
            get => Xamarin.Essentials.Preferences.Get(nameof(M17PopupShown), false);
            set => Xamarin.Essentials.Preferences.Set(nameof(M17PopupShown), value);
        }

        public bool ShowPreferenceOptions 
        {
            get => Xamarin.Essentials.Preferences.Get(nameof(ShowPreferenceOptions), true);
            set => Xamarin.Essentials.Preferences.Set(nameof(ShowPreferenceOptions), value);
        }

        public void Reset()
        {
            LetterRequestDate = DateTimeOffset.MinValue;
            LetterReRequestAllowed = true;
            LetterNotificationDate = DateTimeOffset.MinValue;
            NewMessagesAvailable = false;
            M17PopupShown = false;
        }

        public DateTimeOffset? LetterNotificationDate
        {
            get
            {
                if (Xamarin.Essentials.Preferences.ContainsKey(nameof(LetterNotificationDate)))
                    return Xamarin.Essentials.Preferences.Get(nameof(LetterNotificationDate), 0L).FromUnixTime();

                return null;
            }
            set
            {
                if (value.HasValue)
                    Xamarin.Essentials.Preferences.Set(nameof(LetterNotificationDate), value.Value.ToUnixTime());
                else
                    Xamarin.Essentials.Preferences.Remove(nameof(LetterNotificationDate));
            }
        }
    }
}
