using Xamarin.CommunityToolkit.ObjectModel;

namespace DigiD.ViewModels
{
    public class AP120ViewModel : BaseTwoFactorViewModel
    {
        public AP120ViewModel(bool twoFactorEnabled)
        {
            PageId = "AP120";
            HasBackButton = true;
            TwoFactorEnabled = twoFactorEnabled;

            NavCloseCommand = new AsyncCommand(async () =>
            {
                await NavigationService.PopToRoot();
            });
        }
    }
}
