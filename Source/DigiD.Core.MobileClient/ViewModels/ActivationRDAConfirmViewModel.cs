using System;
using System.Threading.Tasks;
using DigiD.Common;
using DigiD.Common.Enums;
using DigiD.Common.Interfaces;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Models.ResponseModels;
using DigiD.Common.RDA.ViewModels;
using DigiD.Common.Services;
using DigiD.Common.SessionModels;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DigiD.ViewModels
{
    public class ActivationRdaConfirmViewModel : BaseViewModel
    {
#if A11YTEST
        public ActivationRdaConfirmViewModel() : this(new RdaSessionResponse(),
            async (r) => { await Task.FromResult(false); }, async () => { await Task.CompletedTask; })
        {

        }
#endif

        public ActivationRdaConfirmViewModel(RdaSessionResponse session, Func<bool, Task> completeAction, Func<Task> retryAction)
        {
            PageId = "AP086";
            HeaderText = AppResources.AP086_Header;
            FooterText = AppResources.AP086_Message;
            ButtonCommand = new AsyncCommand(async () =>
            {
                await DependencyService.Get<INavigationService>().PushAsync(new RdaViewModel(session, completeAction, "AP038", false, retryAction));
            });

            NavCloseCommand = new AsyncCommand(async () =>
            {
                var result = await DependencyService.Get<IAlertService>().DisplayAlert(
                    AppResources.ActivationAnnulerenAlertHeader
                    , AppResources.ActivationAnnulerenAlertMessage
                    , AppResources.Yes
                    , AppResources.No);

                if (result)
                {
                    if (HttpSession.ActivationSessionData?.ActivationMethod != ActivationMethod.RequestNewDigidAccount)
                        await App.CancelSession(true, async () => await NavigationService.ShowMessagePage(MessagePageType.ActivationCancelled));
                    else
                        await completeAction.Invoke(false);
                }
            });
        }
    }
}
