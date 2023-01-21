using System;
using System.Threading.Tasks;
using System.Windows.Input;
using DigiD.Common;
using DigiD.Common.Enums;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.SessionModels;
using Xamarin.CommunityToolkit.ObjectModel;

namespace DigiD.ViewModels
{
    public class ConfirmRdaViewModel : BaseViewModel
    {
        public bool ShowIdsTitle => AppSession.Process == Process.UpgradeAndAuthenticate;

#if A11YTEST
        public ConfirmRdaViewModel() : this(new AuthenticateChallengeResponse { WebService = "A11YTest" }) { }
#endif

        public ConfirmRdaViewModel(Func<bool, Task> resultAction)
        {
            if (AppSession.Process == Process.UpgradeAndAuthenticate)
            {
                PageId = "AP072";
                HeaderText = AppResources.AP072_Header;
                FooterText = AppResources.AP072_Message;
                ButtonText = AppResources.ConfirmOK;
                CancelCommand = new AsyncCommand(async () =>
                {
                    CanExecute = false;
                    await resultAction.Invoke(false);
                },() => CanExecute);
            }
            else
            {
                PageId = "AP066";
                HeaderText = AppResources.LoginAndUpgradeHeader;
                FooterText = AppResources.LoginAndUpgradeFooter;
                ButtonText = AppResources.GoNextButtonText;
                CancelCommand = new AsyncCommand(async () =>
                {
                    CanExecute = false;
                    await resultAction.Invoke(false);
                }, () => CanExecute);
            }

            ButtonCommand = new AsyncCommand(async () =>
            {
                CanExecute = false;
                await resultAction.Invoke(true);
            }, () => CanExecute);
        }

        public ICommand CancelCommand { get; set; }
    }
}
