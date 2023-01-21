using DigiD.Common.Helpers;
using DigiD.Common.Mobile.BaseClasses;
using Xamarin.Forms.Xaml;

namespace DigiD.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WIDScannerPage : BaseContentPage
    {
        public WIDScannerPage()
        {
			InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            AnimationView.HeightRequest = (int)DisplayHelper.Width;
        }
    }
}
