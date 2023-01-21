using DigiD.Common.Helpers;
using DigiD.Common.Mobile.BaseClasses;
using Xamarin.Forms.Xaml;

namespace DigiD.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivationRdaCompletedPage : BaseContentPage
    {
        public ActivationRdaCompletedPage()
        {
            InitializeComponent();
            animationView.WidthRequest = DisplayHelper.Width - 40;
        }
    }
}
