using System;

namespace DigiD.Common.Settings
{
    public interface IPreferences
    {
        Version CheckedVersion { get; set; }
        DateTimeOffset LetterRequestDate { get; set; }
        bool LetterReRequestAllowed { get; set; }
        bool M17PopupShown { get; set; }
        bool ShowPreferenceOptions { get; set; }
        void Reset();
    }
}
