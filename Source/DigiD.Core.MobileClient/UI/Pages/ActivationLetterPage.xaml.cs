using DigiD.Common.Services;
using DigiD.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiD.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivationLetterPage
    {
        private ActivationLetterViewModel ViewModel => (ActivationLetterViewModel)BindingContext;

        public ActivationLetterPage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (!DependencyService.Get<IA11YService>().IsInVoiceOverMode())
                ViewModel.HideKeyboard += ViewModel_HideKeyboard;
        }

        private async void ViewModel_HideKeyboard(object sender, System.EventArgs e)
        {
            await ActivationCodeEntry.UnFocus();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel.ActivationCodeEntryVisible && !DependencyService.Get<IA11YService>().IsInVoiceOverMode())
            {
                DefaultElementName = nameof(ActivationCodeEntry);
                await ActivationCodeEntry.Focus();
            }
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            if (ViewModel.ActivationCodeEntryVisible)
                await ActivationCodeEntry.UnFocus();
        }
    }
}

