using DigiD.Common.Mobile.BaseClasses;
using Xamarin.CommunityToolkit.ObjectModel;

namespace DigiD.ViewModels
{
    public class WebViewViewModel : BaseViewModel
    {
        public string Url { get; set; }

        public WebViewViewModel(string url)
        {
            Url = url;
            NavCloseCommand = new AsyncCommand(async () =>
            {
                await NavigationService.PopCurrentModalPage();
            });
        }
    }
}
