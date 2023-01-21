using System.Threading.Tasks;
using DigiD.Common;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Settings;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DigiD.ViewModels
{
    public class SettingsAppThemeViewModel : BaseViewModel
    {
        public bool IsInDarkMode { get; set; } = Application.Current.UserAppTheme == OSAppTheme.Dark;
        public bool IsInLightMode { get; set; } = Application.Current.UserAppTheme == OSAppTheme.Light;
        public bool IsInAutomaticMode { get; set; } = Application.Current.UserAppTheme == OSAppTheme.Unspecified;

        public string DarkModeAccessibilityText => string.Format(IsInDarkMode ? AppResources.AccessibilityItemSelected : AppResources.AccessibilityItemNotSelected, AppResources.DarkmodeOnButtonText);
        public string LightModeAccessibilityText => string.Format(IsInLightMode ? AppResources.AccessibilityItemSelected : AppResources.AccessibilityItemNotSelected, AppResources.DarkmodeOffButtonText);
        public string AutomaticAccessibilityText => string.Format(IsInAutomaticMode ? AppResources.AccessibilityItemSelected : AppResources.AccessibilityItemNotSelected, AppResources.DarkmodeAutomaticButtonText);

        public AsyncCommand<OSAppTheme> SetAppThemeCommand
        {
            get
            {
                return new AsyncCommand<OSAppTheme>(async appTheme =>
                {
                    DialogService.ShowProgressDialog();

                    await Task.Delay(100);

                    Application.Current.UserAppTheme = appTheme;
                    DependencyService.Get<IGeneralPreferences>().AppTheme = Application.Current.UserAppTheme;
                    IsInDarkMode = Application.Current.UserAppTheme == OSAppTheme.Dark;
                    IsInLightMode = Application.Current.UserAppTheme == OSAppTheme.Light;
                    IsInAutomaticMode = Application.Current.UserAppTheme == OSAppTheme.Unspecified;

                    DialogService.HideProgressDialog();
                });
            }
        }

        public SettingsAppThemeViewModel()
        {
            HasBackButton = true;
            PageId = "AP097";
            NavCloseCommand = new AsyncCommand(async () => { await NavigationService.PopToRoot(); });
        }
    }
}
