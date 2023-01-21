using System;
using DigiD.Common;
using DigiD.Common.Enums;
using DigiD.Common.Http.Enums;
using DigiD.Common.Interfaces;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Models.RequestModels;
using DigiD.Common.SessionModels;
using DigiD.Common.Settings;
using DigiD.Helpers;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DigiD.ViewModels
{
    public class NoLetterReceivedViewModel : BaseViewModel
    {
        public bool IsValid { get; set; }
        public bool ReRequestLetterAllowed { get; }

#if A11YTEST
        public NoLetterReceivedViewModel() : this(new AuthenticateChallengeResponse () { IV = "1234567890" } ) { }
#endif

        public NoLetterReceivedViewModel()
        {
            PageId = "AP048";

            ReRequestLetterAllowed = DependencyService.Get<IPreferences>().LetterReRequestAllowed;

            HeaderText = AppResources.NoLetterReceivedHeader;
            FooterText = ReRequestLetterAllowed ? AppResources.NoLetterReceivedMessage : AppResources.NoLetterReceivedNewAccountMessage;
            ButtonText = ReRequestLetterAllowed ? AppResources.NoLetterRequestCommand : AppResources.NoLetterRequestAccountCommand;

            var hours = (DependencyService.Get<IPreferences>().LetterRequestDate
                .AddDays(App.Configuration.LetterRequestDelay).ToLocalTime() - DateTime.UtcNow).TotalHours;

            hours = Math.Ceiling(hours);

            IsValid = hours <= 0;

            if (!IsValid)
            {
                var forbiddenMessage = ReRequestLetterAllowed ? AppResources.AP048_RequestNewLetterForbiddenMessage : AppResources.AP048_RequestNewLetterAccountForbiddenMessage;
                FooterText += string.Format(forbiddenMessage, hours);
            }

            ButtonCommand = new AsyncCommand(async () =>
            {
                if (!CanExecute)
                    return;

                CanExecute = false;

                if (ReRequestLetterAllowed)
                {
                    var response = await DependencyService.Get<IEnrollmentServices>().InitSessionActivationCode(new ActivationCodeSessionRequest
                    {
                        AppId = DependencyService.Get<IMobileSettings>().AppId,
                        RequestNewLetter = true
                    });

                    switch (response.ApiResult)
                    {
                        case ApiResult.Ok:
                            HttpSession.AppSessionId = response.SessionId;
                            await NavigationService.PushAsync(new ActivationLetterViewModel(true));
                            break;
                        default:
                            await NavigationService.ShowMessagePage(MessagePageType.ActivationFailed);
                            break;
                    }
                }
                else
                {
                    await DeactivationHelper.DeactivateApp();
                    await NavigationService.PushAsync(new AP079ViewModel());
                }

                CanExecute = true;
            }, () => IsValid && CanExecute);

            NavCloseCommand = new AsyncCommand(async () =>
            {
                await NavigationService.PopToRoot(true);
            });
        }
    }
}
