using System;
using DigiD.Common;
using DigiD.Common.Enums;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.SessionModels;
using DigiD.Common.Settings;
using DigiD.Common.ViewModels;
using DigiD.Helpers;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DigiD.ViewModels
{
    public class QRScannerViewModel : BaseViewModel
    {
#if A11YTEST
        public QRScannerViewModel() : this(false, true) { }
#endif

        public QRScannerViewModel(bool eIDAuthentication, bool canGoBack)
        {
            HasBackButton = canGoBack;
            PageId = "AP014";
            FooterText = AppResources.ScannerFooter;

            NavCloseCommand = new AsyncCommand(async () =>
            {
                await NavigationService.PopToRoot();
            });

            if (eIDAuthentication)
                HeaderText = AppResources.WIDQRScannerHeader;
            else
            {
                switch (DependencyService.Get<IMobileSettings>().ActivationStatus)
                {
                    case ActivationStatus.Activated:
                        HeaderText = AppResources.ScannerHeaderAuthenticate;
                        break;
                    case ActivationStatus.Pending:
                        HeaderText = AppResources.ScannerHeaderPending;
                        break;
                    case ActivationStatus.NotActivated:
                        HeaderText = AppResources.ScannerHeaderActivation;
                        break;
                }
            }
        }
        
        public AsyncCommand<string> ScanResultCommand
        {
            get
            {
                return new AsyncCommand<string>(async data =>
                {
                    if (!CanExecute)
                        return;
                    
                    Xamarin.Essentials.Vibration.Vibrate(100);
                    CanExecute = false;

                    DialogService.ShowProgressDialog();

                    var d = data.Replace("://", ":");

                    if (Uri.TryCreate(d, UriKind.Absolute, out var uri))
                    {
                        HttpSession.Browser = Browser.Unknown;
                        await IncomingDataHelper.ScanCompletedAsync(uri, false);
                    }   
                    else
                        await NavigationService.ShowMessagePage(MessagePageType.InvalidScan);

                    DialogService.HideProgressDialog();

                    CanExecute = true;
                }, d => CanExecute);
            }
        }

        public AsyncCommand HelpCommand => new AsyncCommand(async () =>
        {
            await NavigationService.PushModalAsync(new VideoPlayerViewModel(VideoFile.QrCode), true, false);
        });
    }
}

