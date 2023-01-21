using System;
using System.Security;
using System.Threading.Tasks;
using DigiD.Common;
using DigiD.Common.Constants;
using DigiD.Common.Enums;
using DigiD.Common.Http.Enums;
using DigiD.Common.Interfaces;
using DigiD.Common.Models;
using DigiD.Common.RDA.ViewModels;
using DigiD.Common.Services;
using DigiD.Common.SessionModels;
using DigiD.Common.Settings;
using DigiD.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DigiD.Helpers
{
    internal static class RdaHelper
    {
        /// <summary>
        /// Initialize RDA session
        /// </summary>
        /// <returns></returns>
        internal static async Task Init(bool showOptions)
        {
            var next = true;
            if (showOptions)
                next = await DependencyService.Get<INavigationService>().ConfirmAsync(AuthenticationActions.RDAUpgrade);

            if (!next)
            {
                await DependencyService.Get<INavigationService>().PopToRoot(true);
                return;
            }

            DependencyService.Get<IDialog>().ShowProgressDialog();
            var response = await DependencyService.Get<IRdaServices>().InitSessionRDA();
            HttpSession.AppSessionId = response.SessionId;

            switch (response.ApiResult)
            {
                case ApiResult.Ok:
                    {
                        await StartUpgrade();
                        break;
                    }
                case ApiResult.Disabled:
                    await DependencyService.Get<INavigationService>().ShowMessagePage(MessagePageType.RDADisabled);
                    break;
                default:
                    if (HttpSession.ActivationSessionData?.ActivationMethod != ActivationMethod.RequestNewDigidAccount)
                    {
                        if (HttpSession.ActivationSessionData != null)
                            await ActivationHelper.StartActivationWithAlternative();
                        else
                            await DependencyService.Get<INavigationService>().ShowMessagePage(MessagePageType.UpgradeAccountWithNFCError);
                    }
                    break;
            }
        }

        internal static async Task StartUpgrade()
        {
            DependencyService.Get<IDialog>().ShowProgressDialog();
            var session = await RdaViewModel.GetRdaSessionUrl();

            switch (session.ApiResult)
            {
                case ApiResult.Ok:
                    await DependencyService.Get<INavigationService>().PushAsync(new RdaViewModel(session, CompleteAction, "AP038"));
                    break;
                case ApiResult.NoDocumentsFound:
                    await DependencyService.Get<INavigationService>().PushAsync(new ConfirmViewModel(new ConfirmModel(ConfirmActions.RdaNoDocumentsFound), false, async result => await NoDocumentsResultAction(result, false, CompleteAction)));
                    DependencyService.Get<IDialog>().HideProgressDialog();
                    break;
                case ApiResult.Nok:
                    await DependencyService.Get<INavigationService>().ShowMessagePage(MessagePageType.UnknownError);
                    DependencyService.Get<IDialog>().HideProgressDialog();
                    break;
            }
        }

        internal static async Task HandleRdaCompletion(SecureString pin)
        {
            if (HttpSession.ActivationSessionData != null)
            {
                HttpSession.ActivationSessionData.Pin = pin;

                //Check if there are documents available, if not, use alternative path
                DependencyService.Get<IDialog>().ShowProgressDialog();
                var session = await RdaViewModel.GetRdaSessionUrl();
                DependencyService.Get<IDialog>().HideProgressDialog();

                switch (session.ApiResult)
                {
                    case ApiResult.Ok:
                        await DependencyService.Get<INavigationService>().PushAsync(new ActivationRdaConfirmViewModel(session, RdaCompleteAction, RdaRetryAction));
                        break;
                    case ApiResult.NoDocumentsFound:
                        await DependencyService.Get<INavigationService>().PushAsync(new ConfirmViewModel(new ConfirmModel(ConfirmActions.RdaNoDocumentsFound), false, async result => await NoDocumentsResultAction(result, true, RdaCompleteAction)));
                        return;
                    default:
                        if (HttpSession.ActivationSessionData?.ActivationMethod != ActivationMethod.RequestNewDigidAccount)
                            await ActivationHelper.StartActivationWithAlternative();
                        else
                            await RdaCompleteAction(false);

                        return;
                }
            }
        }

        private static async Task NoDocumentsResultAction(bool result, bool isActivation, Func<bool, Task> completeAction)
        {
            if (result)
                await DependencyService.Get<INavigationService>().PushAsync(new AP109ViewModel(completeAction, RdaRetryAction));
            else if (isActivation)
            {
                await DependencyService.Get<IEnrollmentServices>().SkipRda();

                if (HttpSession.ActivationSessionData?.ActivationMethod != ActivationMethod.RequestNewDigidAccount)
                    await ActivationHelper.StartActivationWithAlternative();
                else
                    await completeAction(false);
            }
            else
                await DependencyService.Get<INavigationService>().ShowMessagePage(MessagePageType.NoValidDocuments);
        }

        private static async Task RdaCompleteAction(bool res)
        {
            if (HttpSession.ActivationSessionData?.ActivationMethod == ActivationMethod.RequestNewDigidAccount)
                await ActivationHelper.HandlePendingCompletion();
            else if (res)
            {
                var verifyResult = await DependencyService.Get<IEnrollmentServices>().VerifyRDAActivation();

                if (verifyResult.ApiResult == ApiResult.Ok)
                    await ActivationHelper.AskPushNotificationPermissionAsync(ActivationMethod.RDA);
                else
                {
                    DependencyService.Get<IMobileSettings>().Reset();
                    await DependencyService.Get<INavigationService>()
                        .ShowMessagePage(MessagePageType.UnknownError);
                }
            }
            else if (HttpSession.ActivationSessionData != null)
                await DependencyService.Get<INavigationService>().PushAsync(new ActivationRdaFailedViewModel(HttpSession.ActivationSessionData.ActivationMethod));
            else
            {
                DependencyService.Get<IMobileSettings>().Reset();
                await DependencyService.Get<INavigationService>()
                    .ShowMessagePage(MessagePageType.UpgradeAccountWithNFCFailed);
            }
        }

        private static async Task RdaRetryAction()
        {
            if (HttpSession.ActivationSessionData != null)
                await DependencyService.Get<INavigationService>()
                    .PushAsync(new RdaScanFailedViewModel(HttpSession.ActivationSessionData.ActivationMethod));
            else
                await DependencyService.Get<INavigationService>().ShowMessagePage(MessagePageType.UnknownError);
        }

        private static async Task CompleteAction(bool status)
        {
            if (DeviceInfo.Platform == DevicePlatform.Android && DeviceInfo.Version.Major >= 10)
            {
                await DependencyService.Get<IA11YService>()
                    .Speak(status
                        ? AppResources.UpgradeAccountWithNFCSuccessHeader
                        : AppResources.UpgradeAccountWithNFCFailedHeader);
            }

            await DependencyService.Get<INavigationService>().ShowMessagePage(status
                ? MessagePageType.UpgradeAccountWithNFCSuccess
                : MessagePageType.UpgradeAccountWithNFCFailed);
        }
    }
}
