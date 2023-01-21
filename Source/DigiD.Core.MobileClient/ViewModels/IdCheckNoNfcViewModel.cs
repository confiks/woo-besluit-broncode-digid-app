using DigiD.Common;
using DigiD.Common.Enums;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Settings;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DigiD.ViewModels
{
    public class IdCheckNoNfcViewModel : BaseViewModel
    {
        public IdCheckNoNfcViewModel()
        {
            if (DependencyService.Get<IMobileSettings>().LoginLevel == LoginLevel.Midden)
            { 
                PageId = "AP091";
                HeaderText = AppResources.AP091_Header;
                FooterText = AppResources.AP091_Message;

            }
            else
            {
                PageId = "AP111";
                HeaderText = AppResources.AP111_Header;
                FooterText = AppResources.AP111_Message;
            }
            NavCloseCommand = new AsyncCommand(async () => await NavigationService.PopToRoot());
        }
    }
}
