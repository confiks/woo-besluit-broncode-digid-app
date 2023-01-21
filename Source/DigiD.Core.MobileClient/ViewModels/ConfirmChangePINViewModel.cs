using System.Globalization;
using System.Windows.Input;
using DigiD.Common;
using DigiD.Common.Enums;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Models;
using DigiD.Helpers;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DigiD.ViewModels
{
    public class ConfirmChangePinViewModel : BaseViewModel
    {
        public bool IsChangeAppPinActive { get; set; } = true;

        public AsyncCommand ChangeAppPINCommand { get; set; }
        public AsyncCommand ChangeWidPINCommand { get; set; }
        
        public ConfirmChangePinViewModel()
        {
            HasBackButton = true;

            ChangeAppPINCommand = new AsyncCommand(async () =>
            {
                await ChangePinHelper.StartChangePIN();
            });

            ChangeWidPINCommand = new AsyncCommand(async () =>
            {
                await NavigationService.GoToNFCScannerPage(new NfcScannerModel
                {
                    Action = PinEntryType.ChangePIN_PIN
                });
            });

            NavCloseCommand = new AsyncCommand(async () =>
            {
                await NavigationService.PopToRoot();
            });

            SetText();
        }

        private void SetText()
        {
            if (IsChangeAppPinActive)
            {
                PageId = "AP073";
                HeaderText = string.Format(CultureInfo.InvariantCulture, AppResources.WIDChangePINHeader, "app");
                FooterText = AppResources.ChangeAppPINConfirmMessage;
            }
            else
            {
                PageId = "AP410";
                HeaderText = string.Format(CultureInfo.InvariantCulture, AppResources.WIDChangePINHeader, AppResources.eID.ToLowerInvariant());
                FooterText = string.Format(CultureInfo.InvariantCulture, AppResources.WIDChangePINMessage, AppResources.eID.ToLowerInvariant());
            }
        }

        public ICommand TabbedChangedCommand => new Command<string>((tab) =>
        {
            if (tab == "app" && !IsChangeAppPinActive)
                {IsChangeAppPinActive = true;}
            else if (tab == "wid" && IsChangeAppPinActive)
                IsChangeAppPinActive = false;
            
            SetText();
            TrackView();
        });
    }
}
