using Xamarin.Forms;

namespace DigiD.Common.Settings
{
    public interface IGeneralPreferences
    {
#if ENVIRONMENT_SELECTABLE
        string SelectedHost { get; set; }
#else
        string SelectedHost { get; }
#endif
        int NumberOfSuccessfulLogins { get; set; }
        bool PiwikTrackEnabled { get; set; }
        string Language { get; set; }
        OSAppTheme AppTheme { get; set; }
        string WhatsNewVersion { get; set; }
        string InstanceId { get; }
    }
}
