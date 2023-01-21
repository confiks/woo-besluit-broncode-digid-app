using System;
using System.Threading.Tasks;
using DigiD.Common.Services;
using DigiD.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiD.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivationSMSPage : BaseActivationPage
    {
        public ActivationSMSPage()
        {
            InitializeComponent();
            DefaultElementName = Device.RuntimePlatform == Device.iOS ? nameof(errorLabel) : string.Empty;
        }

        ~ActivationSMSPage()
        {
            ((ActivationSmsViewModel)BindingContext).HideKeyboard -= OnHideKeyboard;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (!DependencyService.Get<IA11YService>().IsInVoiceOverMode())
                ((ActivationSmsViewModel)BindingContext).HideKeyboard += OnHideKeyboard;
        }

        private async void OnHideKeyboard(object sender, EventArgs e)
        {
            await SMSEntry.UnFocus();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!DependencyService.Get<IA11YService>().IsInVoiceOverMode())
                SetEntryDefault();
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            await SMSEntry.UnFocus();
        }

        private void SetEntryDefault()
        {
            Device.BeginInvokeOnMainThread(async () => {
                await Task.Delay(600);
                await SMSEntry.Focus();
            });
        }

    }
}

