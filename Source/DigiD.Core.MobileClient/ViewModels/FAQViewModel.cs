using DigiD.Common;
using DigiD.Common.Mobile.BaseClasses;
using Xamarin.CommunityToolkit.ObjectModel;

namespace DigiD.ViewModels
{
    public class FaqViewModel : BaseViewModel
    {
        public FaqViewModel()
        {
            PageId = "AP065";
            HeaderText = AppResources.FAQHeader;
            HasBackButton = true;
            NavCloseCommand = new AsyncCommand(async () => await NavigationService.PopToRoot());
        }
        
    }
}
