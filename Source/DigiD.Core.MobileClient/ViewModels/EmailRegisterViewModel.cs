using System.Text.RegularExpressions;
using DigiD.Common;
using DigiD.Common.Enums;
using DigiD.Common.Http.Enums;
using DigiD.Common.Models;
using DigiD.Common.Services;
using DigiD.Interfaces;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DigiD.ViewModels
{
    public class EmailRegisterViewModel : BaseActivationViewModel
    {
        private readonly RegisterEmailModel _model;

        public IAsyncCommand ButtonSkipCommand => new AsyncCommand(async () =>
        {
            var response = await DependencyService.Get<IAlertService>().DisplayAlert(AppResources.M12_Title,
                AppResources.M12_Message, AppResources.Yes, AppResources.No);

            if (response)
            {
                await DependencyService.Get<IEmailService>().Register(null);

                if (_model.AbortAction != null)
                    await _model.AbortAction.Invoke();
                else
                    await _model.NextAction.Invoke();
            }
        });

        public string EmailAddress { get; set; }

        public bool IsValid => !string.IsNullOrEmpty(EmailAddress) && Regex.IsMatch(EmailAddress,
            "[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])");

#if A11YTEST
        public EmailRegisterViewModel() : this(new RegisterEmailModel(ActivationMethod.SMS, true )) { }
#endif

        public EmailRegisterViewModel(RegisterEmailModel model)
        {
            _model = model;
            PageId = "AP080";
            FooterText = AppResources.AP080_Message;
            HeaderText = AppResources.AP080_Header;

            ButtonCommand = new AsyncCommand(async () =>
            {
                CanExecute = false;
                
                DialogService.ShowProgressDialog();
                var result = await DependencyService.Get<IEmailService>().Register(EmailAddress);

                switch (result.ApiResult)
                {
                    case ApiResult.Ok:
                        await NavigationService.PushAsync(new EmailConfirmViewModel(_model, result.EmailAddress, true));
                        break;
                    case ApiResult.Nok:
                        switch (result.ErrorMessage)
                        {
                            case "too_many_emails":
                                await NavigationService.ShowMessagePage(MessagePageType.EmailRegistrationTooManyMails, _model);
                                break;
                            case "email_address_maximum_reached":
                                await NavigationService.ShowMessagePage(MessagePageType.EmailRegistrationMaximumReached, _model);
                                break;
                            case "email_already_verified":
                                await NavigationService.ShowMessagePage(MessagePageType.EmailRegistrationAlreadyVerified, _model);
                                break;
                        }

                        break;
                }
                
                DialogService.HideProgressDialog();

                CanExecute = true;
            },() => CanExecute);
        }
    }
}
