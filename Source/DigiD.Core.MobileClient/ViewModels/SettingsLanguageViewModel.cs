using System.Globalization;
using System.Threading.Tasks;
using DigiD.Common;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Settings;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DigiD.ViewModels
{
    public class SettingsLanguageViewModel : BaseViewModel
    {
        public bool IsEnglish { get; set; }
        public bool IsDutch { get; set; }

        //tbv accessibility
        public string EnglishAccessibilityText => string.Format(IsEnglish ? AppResources.AccessibilityItemSelected : AppResources.AccessibilityItemNotSelected, AppResources.English);
        public string DutchAccessibilityText => string.Format(IsDutch ? AppResources.AccessibilityItemSelected : AppResources.AccessibilityItemNotSelected, AppResources.Dutch);

        private readonly IGeneralPreferences _generalSettings = DependencyService.Get<IGeneralPreferences>();

        public SettingsLanguageViewModel()
        {
            HasBackButton = true;
            PageId = "AP096";
            HeaderText = AppResources.Language;

            IsEnglish = _generalSettings.Language == "en";
            IsDutch = _generalSettings.Language == "nl";

            NavCloseCommand = new AsyncCommand(async () => { await NavigationService.PopToRoot(); });
        }

        public AsyncCommand<string> SelectLanguageCommand
        {
            get
            {
                return new AsyncCommand<string>(async language =>
                {
                    DialogService.ShowProgressDialog();

                    await Task.Delay(100);

                    _generalSettings.Language = language;
                    
                    Localization.Init(CultureInfo.GetCultureInfo(language));
                    
                    await NavigationService.ResetMainPage(new SettingsViewModel(), new SettingsLanguageViewModel());
                    
                    DialogService.HideProgressDialog();
                });
            }
        }
    }
}
