using DigiD.Common.Enums;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Settings;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DigiD.ViewModels
{
    public class AP119ViewModel : BaseViewModel
    {
        public AP119ViewModel()
        {
            DependencyService.Get<IMobileSettings>().ActivationMethod = ActivationMethod.RequestNewDigidAccount;
            
            PageId = "AP119";
            HasBackButton = true;
            
            ButtonCommand = new AsyncCommand(async () =>
            {
                await NavigationService.GoBack();
            });
        }
    }
}
