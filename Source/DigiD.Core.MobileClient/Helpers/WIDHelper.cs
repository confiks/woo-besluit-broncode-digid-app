using System;
using System.Threading.Tasks;
using DigiD.Common.Constants;
using DigiD.Common.Enums;
using DigiD.Common.Http.Enums;
using DigiD.Common.Interfaces;
using DigiD.Common.Models;
using DigiD.Common.Models.RequestModels;
using DigiD.Common.SessionModels;
using DigiD.ViewModels;
using Xamarin.Forms;

namespace DigiD.Helpers
{
    internal static class WidHelper
    {
        internal static async Task AuthenticateAsync(SessionData sessionData)
        {
            if (!App.HasNfc)
            {
                await DependencyService.Get<IEIDServices>().AbortAuthentication(new AbortAuthenticationRequest(AbortConstants.NoNFC));
                await DependencyService.Get<INavigationService>().ShowMessagePage(MessagePageType.NoNFCSupport);

                return;
            }

            var code = string.Empty;

            if (!string.IsNullOrEmpty(sessionData.verification_code))
                code = sessionData.verification_code;

            if (string.IsNullOrEmpty(code) || sessionData.verification_code.Equals(HttpSession.VerificationCode, StringComparison.CurrentCultureIgnoreCase))
                await StartSession();
            else
            {
                await DependencyService.Get<IEIDServices>()
                    .AbortAuthentication(new AbortAuthenticationRequest(AbortConstants.VerificationCodeFailed));
                await DependencyService.Get<INavigationService>()
                    .ShowMessagePage(MessagePageType.VerificationCodeFailed);
            }
        }

        private static async Task StartSession()
        {
            var response = await DependencyService.Get<IEIDServices>().InitSessionEID(new WidSessionRequest());

            switch (response.ApiResult)
            {
                case ApiResult.Ok:
                    {
                        if (!string.IsNullOrEmpty(response.WebService))
                        {
                            await DependencyService.Get<INavigationService>().PushAsync(new WebserviceConfirmViewModel(null, response));
                        }
                        else if (!string.IsNullOrEmpty(response.Action))
                        {
                            var model = new ConfirmModel(response.Action)
                            {
                                WIDSessionResponse = response,
                            };
                            await DependencyService.Get<INavigationService>().PushAsync(new ConfirmViewModel(model, false));
                        }

                        break;
                    }
                case ApiResult.Nok:
                    await DependencyService.Get<INavigationService>().ShowMessagePage(MessagePageType.ErrorOccoured);
                    break;
                case ApiResult.Forbidden:
                    await App.CheckVersion();
                    break;
                case ApiResult.HostNotReachable:
                    await DependencyService.Get<INavigationService>().ShowMessagePage(MessagePageType.NoInternetConnection);
                    break;
                case ApiResult.SSLPinningError:
                    await DependencyService.Get<INavigationService>().ShowMessagePage(MessagePageType.SSLException);
                    break;
            }
        }
    }
}
