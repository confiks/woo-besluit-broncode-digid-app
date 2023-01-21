using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Services;
using DigiD.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiD.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsAppThemePage : BaseContentPage
    {
        public SettingsAppThemePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (DependencyService.Get<IA11YService>().IsInVoiceOverMode() && BindingContext is SettingsAppThemeViewModel vm)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var selectedItem = darkmodeOffButton;
                    if (vm.IsInAutomaticMode)
                        selectedItem = automaticButton;
                    else if (vm.IsInDarkMode)
                        selectedItem = darkmodeOnButton;

                    selectedItem.Focus();
                    var tts = $"{header.Text} {AutomationProperties.GetName(selectedItem) ?? ""}";
                    await DependencyService.Get<IA11YService>().Speak(tts, 2000);
                });
            }
        }
    }
}
