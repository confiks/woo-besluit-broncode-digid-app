using DigiD.Common;
using DigiD.Common.Enums;
using DigiD.Common.Interfaces;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.SessionModels;
using DigiD.Helpers;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DigiD.ViewModels
{
    public class RdaScanFailedViewModel : BaseViewModel
    {
#if A11YTEST
        public RdaScanFailedViewModel() : this(ActivationMethod.SMS)
        {

        }
#endif
        public string CancelText { get; set; }
        public AsyncCommand CancelCommand { get; set; }
        public string MessageText { get; set; }

        public RdaScanFailedViewModel(ActivationMethod activationMethod)
        {
            switch (activationMethod)
            {
                case ActivationMethod.RequestNewDigidAccount:
                    PageId = "AP087";
                    MessageText = AppResources.AP087_Message;
                    CancelText = AppResources.AP087_ButtonText;
                    break;
                case ActivationMethod.SMS:
                    PageId = "AP088";
                    MessageText = AppResources.AP088_Message;
                    CancelText = AppResources.AP088_ButtonText;
                    break;
                case ActivationMethod.App:
                case ActivationMethod.Password:
                    PageId = "AP089";
                    MessageText = AppResources.AP089_Message;
                    CancelText = AppResources.AP089_ButtonText;
                    break;
            }
            
            ButtonCommand = new AsyncCommand(async () =>
            {
                await NavigationService.GoBack();
            });
            
            CancelCommand = new AsyncCommand(async () =>
            {
                DialogService.ShowProgressDialog();

                switch (HttpSession.ActivationSessionData.ActivationMethod)
                {
                    case ActivationMethod.App:
                        await ActivationHelper.AskPushNotificationPermissionAsync(ActivationMethod.App);
                        break;
                    case ActivationMethod.RequestNewDigidAccount:
                        await DependencyService.Get<IEnrollmentServices>().SkipRda();
                        await ActivationHelper.AskPushNotificationPermissionAsync(ActivationMethod.RequestNewDigidAccount);
                        break;
                    default:
                        await ActivationHelper.StartActivationWithAlternative();
                        break;
                }

                DialogService.HideProgressDialog();
            });
        }
    }
}
