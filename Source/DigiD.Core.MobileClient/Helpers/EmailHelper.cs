using System.Threading.Tasks;
using DigiD.Common.Constants;
using DigiD.Common.Enums;
using DigiD.Common.Interfaces;
using DigiD.Common.Models;
using DigiD.Interfaces;
using DigiD.Models;
using DigiD.ViewModels;
using Xamarin.Forms;

namespace DigiD.Helpers
{
    internal static class EmailHelper
    {
        internal static async Task<CheckEmailModel> CheckEmailStatus()
        {
            var response = await DependencyService.Get<IEmailService>().Status();
            var result = new CheckEmailModel();
            var tcs = new TaskCompletionSource<CheckEmailModel>();

            if (response.UserActionNeeded)
            {
                switch (response.EmailStatus)
                {
                    case EmailStatus.Verified:
                        {
                            await DependencyService.Get<INavigationService>().PushAsync(new ConfirmViewModel(new ConfirmModel(ConfirmActions.ConfirmEmailAddress, response.EmailAddress), false, async isConfirmed =>
                            {
                                await DependencyService.Get<IEmailService>().Confirm(isConfirmed);
                                result.Action = ConfirmActions.ConfirmEmailAddress;
                                result.Confirmed = isConfirmed;
                                tcs.TrySetResult(result);
                            }));
                            break;
                        }
                    case EmailStatus.NotVerified:
                        {
                            await DependencyService.Get<INavigationService>().PushAsync(new ConfirmViewModel(new ConfirmModel(ConfirmActions.ConfirmEmailAddress, response.NoVerifiedEmailAddress), false, async isConfirmed =>
                            {
                                await Task.Delay(0);
                                result.Action = ConfirmActions.ConfirmExistingEmailAddress;
                                result.EmailAddress = response.NoVerifiedEmailAddress;
                                result.Confirmed = isConfirmed;
                                tcs.TrySetResult(result);
                            }));
                            break;
                        }
                    case EmailStatus.None:
                        {
                            await DependencyService.Get<INavigationService>().PushAsync(new ConfirmViewModel(new ConfirmModel(ConfirmActions.RegisterEmailAddress), false, async isValid =>
                            {
                                await Task.Delay(0);
                                result.Action = ConfirmActions.RegisterEmailAddress;
                                result.Confirmed = isValid;
                                tcs.TrySetResult(result);
                            }));
                            break;
                        }
                }
            }
            else
                tcs.SetResult(null);

            return await tcs.Task;
        }

        internal static async Task Manage(bool fromLanding)
        {
            var emailStatus = await DependencyService.Get<IEmailService>().Status();

            if (emailStatus.EmailStatus == EmailStatus.None)
            {
                await DependencyService.Get<INavigationService>().PushAsync(new EmailRegisterViewModel(new RegisterEmailModel
                {
                    AbortAction = async () =>
                    {
                        if (fromLanding)
                            await DependencyService.Get<INavigationService>().ResetMainPage(new AP087ViewModel());
                        else
                            await DependencyService.Get<INavigationService>().ResetMainPage(new AP106ViewModel());
                    },
                    NextAction = async () => await DependencyService.Get<INavigationService>().ShowMessagePage(MessagePageType.EmailRegistrationSuccess)
                }));
            }
            else
            {
                var model = new ConfirmModel(ConfirmActions.ConfirmExistingEmailAddress, emailStatus.EmailStatus == EmailStatus.Verified ? emailStatus.EmailAddress : emailStatus.NoVerifiedEmailAddress);
                await DependencyService.Get<INavigationService>().PushAsync(new ConfirmViewModel(model, true,
                    async confirmResult =>
                    {
                        if (confirmResult)
                        {
                            if (emailStatus.EmailStatus == EmailStatus.Verified)
                            {
                                await DependencyService.Get<IEmailService>().Confirm(true);
                                await DependencyService.Get<INavigationService>().GoBack();
                            }
                            else
                                await DependencyService.Get<INavigationService>().PushAsync(new EmailConfirmViewModel(new RegisterEmailModel(true)
                                    {
                                        NextAction = async () =>
                                            await DependencyService.Get<INavigationService>()
                                                .ShowMessagePage(MessagePageType.EmailRegistrationSuccessfulVerified),
                                        AbortAction = async () =>
                                        {
                                            if (fromLanding)
                                                await DependencyService.Get<INavigationService>()
                                                    .ResetMainPage(new AP087ViewModel());
                                            else
                                                await DependencyService.Get<INavigationService>()
                                                    .ResetMainPage(new AP106ViewModel());
                                        }
                                    }, emailStatus.EmailAddress ?? emailStatus.NoVerifiedEmailAddress, false));
                        }
                        else
                            await DependencyService.Get<INavigationService>().PushAsync(new EmailRegisterViewModel(
                                new RegisterEmailModel
                                {
                                    AbortAction = async () =>
                                    {
                                        if (fromLanding)
                                            await DependencyService.Get<INavigationService>()
                                                .ResetMainPage(new AP087ViewModel());
                                        else
                                            await DependencyService.Get<INavigationService>()
                                                .ResetMainPage(new AP106ViewModel());
                                    },
                                    NextAction = async () =>
                                        await DependencyService.Get<INavigationService>()
                                            .ShowMessagePage(MessagePageType.EmailRegistrationSuccess)
                                }));
                    }));
            }
        }
    }
}
