using System;
using System.Threading.Tasks;
using DigiD.Common.BaseClasses;
using DigiD.Common.Enums;
using DigiD.Common.Http.Enums;
using DigiD.Common.Interfaces;
using DigiD.Common.Models;
using DigiD.Common.Services;
using DigiD.Common.SessionModels;
using DigiD.ViewModels;
using Xamarin.Forms;

namespace DigiD.Helpers
{
    internal static class SessionHelper
    {
        internal static async Task StartSession(Func<Task> nextAction)
        {
            DependencyService.Get<IDialog>().ShowProgressDialog();

            var status = await DependencyService.Get<IAuthenticationService>().SessionStatus();

            switch (status.ApiResult)
            {
                case ApiResult.Ok:
                    await nextAction.Invoke();
                    break;
                case ApiResult.SessionNotFound:
                case ApiResult.Nok when status.ErrorMessage == "no_session":
                    {
                        AppSession.ManagementAction = nextAction;

                        var model = new PinCodeModel(PinEntryType.Authentication)
                        {
                            IsAppAuthentication = true
                        };

                        if (Application.Current.MainPage is CustomNavigationPage cnp &&
                            cnp.CurrentPage.BindingContext is PinCodeViewModel)
                        {
                            //user is already on pincode view, so view change is not needed
                        }
                        else
                            DependencyService.Get<INavigationService>().SetPage(new PinCodeViewModel(model));
                        break;
                    }
                default:
                    AppSession.AuthenticationSessionId = null;
                    await DependencyService.Get<INavigationService>().ShowMessagePage(MessagePageType.UnknownError);
                    break;
            }

            DependencyService.Get<IDialog>().HideProgressDialog();
        }
    }
}
