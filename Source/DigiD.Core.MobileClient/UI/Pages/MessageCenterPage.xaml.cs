using DigiD.Common.Mobile.BaseClasses;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace DigiD.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessageCenterPage : BaseContentPage
    {
        public MessageCenterPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>()
                .SetModalPresentationStyle(UIModalPresentationStyle.OverFullScreen);
        }
    }
}
