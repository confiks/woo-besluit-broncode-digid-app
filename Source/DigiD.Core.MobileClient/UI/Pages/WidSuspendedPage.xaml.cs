using DigiD.Common.Mobile.BaseClasses;
using Xamarin.Forms.Xaml;

namespace DigiD.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WidSuspendedPage : BaseContentPage
    {
        public WidSuspendedPage()
        {
            InitializeComponent();

            animationView.WidthRequest = 50;
            animationView.HeightRequest = animationView.WidthRequest;
        }
    }
}
