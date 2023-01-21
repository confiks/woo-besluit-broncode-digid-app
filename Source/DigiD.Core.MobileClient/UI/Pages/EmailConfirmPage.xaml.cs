using System;
using System.Threading.Tasks;
using DigiD.Common.Services;
using DigiD.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiD.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmailConfirmPage : BaseActivationPage
    {
        public EmailConfirmPage()
        {
            InitializeComponent();
            DefaultElementName = Device.RuntimePlatform == Device.iOS ? nameof(errorLabel) : string.Empty;
            buttonPanel.ViewOrder = new[] { primaryButton, secundairyButton };
        }

        ~EmailConfirmPage()
        {
            ((EmailConfirmViewModel)BindingContext).HideKeyboard -= OnHideKeyboard;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetEntryDefault();
        }

        private void SetEntryDefault()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await EmailEntry.Focus();
                await Task.Delay(600);
            });
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (!DependencyService.Get<IA11YService>().IsInVoiceOverMode())
                ((EmailConfirmViewModel)this.BindingContext).HideKeyboard += OnHideKeyboard;
        }

        private async void OnHideKeyboard(object sender, EventArgs e)
        {
            await EmailEntry.UnFocus();
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            await EmailEntry.UnFocus();
        }
    }
}
