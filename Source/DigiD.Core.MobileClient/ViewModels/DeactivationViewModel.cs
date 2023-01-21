using DigiD.Common;
using DigiD.Common.Enums;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Services;
using DigiD.Common.Settings;
using DigiD.Helpers;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DigiD.ViewModels
{
    internal class DeactivationViewModel : BaseViewModel
    {
        public bool IsLetterActivation { get; set; }
        public string ImageSource => IsLetterActivation ? "resource://DigiD.Resources.afbeelding_activeringscode_brief.svg" : "resource://DigiD.Resources.digid_afbeelding_digid_app_deactiveren.svg";
        public DeactivationViewModel()
        {
            PageId = "AP010";
            HeaderText = AppResources.DeactivateHeader;
            HasBackButton = true;
            IsLetterActivation = DependencyService.Get<IMobileSettings>().ActivationStatus == ActivationStatus.Pending;
            FooterText = IsLetterActivation ? AppResources.DeactivateFooterLetter : AppResources.DeactivateFooter;
            
            NavCloseCommand = new AsyncCommand(async () =>
            {
                await NavigationService.PopToRoot();
            });
        }

        public AsyncCommand CancelCommand
        {
            get
            {
                return new AsyncCommand(async () => await NavigationService.GoBack());
            }
        }

        public AsyncCommand DeactivateCommand
        {
            get
            {
                return new AsyncCommand( async () =>
                {
                    CanExecute = false;

                    try
                    {
                        if (await DependencyService.Get<IAlertService>().DisplayAlert(AppResources.DeactivateHeader,
                            AppResources.DeactivateConfirmMessage, AppResources.ConfirmOK, AppResources.ConfirmCancel))
                        {
                            await DeactivationHelper.DeactivateApp();
                            await NavigationService.ShowMessagePage(MessagePageType.DeactivateAppSuccess);
                        }
                    }
                    catch (System.Exception e)
                    {
                        FooterText = $"Fout tijdens openen Popup scherm: {e.Message}";
                    }

                    CanExecute = true;
                }, () => CanExecute);
            }
        }

        public override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}
