using DigiD.Common;
using DigiD.Common.Enums;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Helpers;
using Xamarin.CommunityToolkit.ObjectModel;

namespace DigiD.ViewModels
{
    public class ActivationRdaFailedViewModel : BaseViewModel
    {
#if A11YTEST
        public ActivationRdaFailedViewModel() : this(ActivationMethod.SMS){}
#endif

        public ActivationRdaFailedViewModel(ActivationMethod activationMethod)
        {
            HasBackButton = false;

            if (activationMethod == ActivationMethod.SMS)
            {
                PageId = "AP104";
                HeaderText = AppResources.AP104_Header;
                FooterText = AppResources.AP104_Message;
                ButtonText = AppResources.AP104_Button;
            }
            else
            {
                PageId = "AP105";
                HeaderText = AppResources.AP105_Header;
                FooterText = AppResources.AP105_Message;
                ButtonText = AppResources.AP105_Button;
            }

            ButtonCommand = new AsyncCommand(async () =>
            {
                await ActivationHelper.StartActivationWithAlternative();
            });
        }
    }
}
