using DigiD.Common.Mobile.BaseClasses;
using Xamarin.CommunityToolkit.ObjectModel;

namespace DigiD.ViewModels
{
    public class AP082ViewModel : BaseViewModel
    {
        public AP082ViewModel()
        {
            PageId = "AP082";
            HasBackButton = true;
            ButtonCommand = new AsyncCommand(async () => await NavigationService.GoBack());
        }

        public override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}
