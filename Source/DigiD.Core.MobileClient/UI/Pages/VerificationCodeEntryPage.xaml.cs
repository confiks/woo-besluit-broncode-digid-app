using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiD.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerificationCodeEntryPage : BaseContentPage
    {
        public VerificationCodeEntryPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (!DependencyService.Get<IA11YService>().IsInVoiceOverMode())
                await VerificationCodeEntryView.Focus();
        }
    }
}
