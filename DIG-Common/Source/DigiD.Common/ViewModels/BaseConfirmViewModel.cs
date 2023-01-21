using DigiD.Common.BaseClasses;
using DigiD.Common.Constants;
using DigiD.Common.Enums;
using DigiD.Common.Helpers;
using DigiD.Common.Interfaces;
using DigiD.Common.Models;
using System;
using System.Globalization;
using System.Windows.Input;
using DigiD.Common.EID.Helpers;
using DigiD.Common.Http.Enums;
using DigiD.Common.NFC.Enums;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DigiD.Common.ViewModels
{
    public class BaseConfirmViewModel : CommonBaseViewModel
    {
        public string ImageSource { get; set; }
        public string CancelText { get; set; } = AppResources.ConfirmCancel;
        protected bool IsNextCommandExecuted;

        public ConfirmModel Model { get; }

        public BaseConfirmViewModel(ConfirmModel model)
        {
            Model = model ?? throw new ArgumentNullException(nameof(model));

            CancelCommand = new AsyncCommand(async () =>
            {
                if (!CanExecute)
                    return;

                CanExecute = false;
                if (Model.WIDSessionResponse != null)
                    await DependencyService.Get<IEIDServices>().CancelAuthenticate();

                Cancel();
                await NavigationService.PopToRoot(true);
                CanExecute = true;
            }, () => CanExecute);

            SetWidActions();
        }

        private void SetWidActions()
        {
            switch (Model.Action)
            {
                case AuthenticationActions.ACTIVATE_DRIVING_LICENSE:
                    {
                        ButtonCommand = ShowPINForEIDCommand;
                        PageId = Device.Idiom == TargetIdiom.Desktop ? "DA016" : "AP408";
                        HeaderText = AppResources.WIDActivateConfirmHeader;
                        FooterText = string.Format(CultureInfo.InvariantCulture, AppResources.WIDActivateConfirmMessage, DocumentType.DrivingLicense.Translate().ToLowerInvariant());
                        ImageSource = "resource://DigiD.Common.Resources.afbeelding_document_activeren.svg?assembly=DigiD.Common";
                        ButtonText = AppResources.ConfirmOK;

                        CardHelper.SetCard(DocumentType.DrivingLicense);
                        break;
                    }
                case AuthenticationActions.ACTIVATE_IDENTITY_CARD:
                    {
                        PageId = Device.Idiom == TargetIdiom.Desktop ? "DA016" : "AP408";
                        HeaderText = AppResources.WIDActivateConfirmHeader;
                        FooterText = string.Format(CultureInfo.InvariantCulture, AppResources.WIDActivateConfirmMessage, DocumentType.IDCard.Translate().ToLowerInvariant());
                        ImageSource = "resource://DigiD.Common.Resources.afbeelding_document_activeren.svg?assembly=DigiD.Common";
                        ButtonText = AppResources.ConfirmOK;
                        ButtonCommand = ShowPINForEIDCommand;
                        CardHelper.SetCard(DocumentType.IDCard);
                        break;
                    }
                case AuthenticationActions.REACTIVATE_DRIVING_LICENSE:
                case AuthenticationActions.REACTIVATE_IDENTITY_CARD:
                    {
                        var card = Model.Action == AuthenticationActions.REACTIVATE_DRIVING_LICENSE
                            ? DocumentType.DrivingLicense
                            : DocumentType.IDCard;

                        PageId = Device.Idiom == TargetIdiom.Desktop ? "DA037" : "AP413";
                        HeaderText = Device.Idiom == TargetIdiom.Desktop ? AppResources.DA037_Header : string.Format(CultureInfo.InvariantCulture, AppResources.WIDReactivateConfirmHeader, card.Translate().ToLowerInvariant());
                        FooterText = string.Format(CultureInfo.InvariantCulture, Device.Idiom == TargetIdiom.Desktop ? AppResources.DA037_Message : AppResources.WIDReactivateConfirmMessage, card.Translate().ToLowerInvariant());
                        ImageSource = "resource://DigiD.Common.Resources.afbeelding_document_vernieuwen.svg?assembly=DigiD.Common";
                        ButtonText = AppResources.ConfirmOK;
                        ButtonCommand = ShowPINForEIDCommand;
                        CardHelper.SetCard(card);
                        break;
                    }
                case ConfirmActions.ChangeWIDPIN:
                    {
                        PageId = "AP410";
                        HeaderText = string.Format(CultureInfo.InvariantCulture, AppResources.WIDChangePINHeader, AppResources.eID.ToLowerInvariant());
                        FooterText = string.Format(CultureInfo.InvariantCulture, AppResources.WIDChangePINMessage, AppResources.eID.ToLowerInvariant());
                        ImageSource = "resource://DigiD.Common.Resources.digid_afbeelding_pincode_wijzigen.svg?assembly=DigiD.Common";
                        ButtonText = AppResources.Continue;
                        CancelText = string.Empty;

                        ButtonCommand = new Command(() =>
                            {
                                NavigationService.GoToNFCScannerPage(new NfcScannerModel
                                {
                                    Action = PinEntryType.ChangePIN_PIN
                                });
                            });
                        NavCloseCommand = new Command(() => NavigationService.PopToRoot());
                        break;
                    }
            }
        }

        public virtual void Cancel()
        {

        }

        public ICommand CancelCommand { get; set; }

        public AsyncCommand ShowPINForEIDCommand
        {
            get
            {
                return new AsyncCommand(async () =>
                {
                    if (!CanExecute)
                        return;

                    CanExecute = false;
                    var result = await DependencyService.Get<IEIDServices>().Confirm();
                    switch (result)
                    {
                        case ApiResult.Ok when Model.WIDSessionResponse != null:
                            {
                                var model = new NfcScannerModel
                                {
                                    IsActivation = Model.Action == AuthenticationActions.ACTIVATE_DRIVING_LICENSE || Model.Action == AuthenticationActions.ACTIVATE_IDENTITY_CARD,
                                    Action = PinEntryType.Authentication,
                                    EIDSessionResponse = Model.WIDSessionResponse,
                                };

                                await NavigationService.GoToNFCScannerPage(model);
                                break;
                            }
                        case ApiResult.Nok:
                            await NavigationService.ShowMessagePage(MessagePageType.LoginCancelled);
                            break;
                        default:
                            await NavigationService.ShowMessagePage(MessagePageType.UnknownError);
                            break;
                    }

                    CanExecute = true;
                }, () => CanExecute);
            }
        }
    }
}
