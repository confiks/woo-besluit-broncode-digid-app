using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Services;
using DigiD.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiD.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsLanguagePage : BaseContentPage
    {
        public SettingsLanguagePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (DependencyService.Get<IA11YService>().IsInVoiceOverMode() && BindingContext is SettingsLanguageViewModel vm)
            {
                await Device.InvokeOnMainThreadAsync(async () =>
                {
                    var selectedItem = englishLanguage;

                    if (vm.IsDutch)
                        selectedItem = dutchLanguage;

                    selectedItem.Focus();
                    var tts = $"{vm.HeaderText} {AutomationProperties.GetName(selectedItem) ?? string.Empty}";
                    await DependencyService.Get<IA11YService>().Speak(tts, 2000);
                });
            }
        }
    }
}
