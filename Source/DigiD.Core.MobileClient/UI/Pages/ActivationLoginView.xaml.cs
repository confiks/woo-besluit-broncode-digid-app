using DigiD.Common.Constants;
using DigiD.Common.Controls;
using DigiD.Common.Services;
using DigiD.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiD.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivationLoginView : BaseActivationPage
    {
        public ActivationLoginView()
        {
            InitializeComponent();
            buttonPanel.ViewOrder = new[] { primaryButton, secondaryButton };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<ActivationLoginViewModel>(this, MessagingConstants.FindDefaultElement, InvalidInput);
        }

        private void InvalidInput(ActivationLoginViewModel obj)
        {
            DefaultElement.Focus();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<ActivationLoginViewModel>(this, MessagingConstants.FindDefaultElement);
        }


        private void Handle_Completed(object sender, System.EventArgs e)
        {
            if (sender is CustomEntryField cef)
                cef.CheckMinimum(PasswordField);
        }

        private void HandlePassword_Completed(object sender, System.EventArgs e)
        {
            if (!(BindingContext is ActivationLoginViewModel vm))
                return;

            if (!DependencyService.Get<IA11YService>().IsInVoiceOverMode())
                return;

            if (sender is CustomEntryField cef && cef.CheckMinimum() && vm.IsValid && vm.ButtonCommand?.CanExecute(null) == true)
            {
                vm.ButtonCommand.Execute(null);
            }
        }
    }
}
