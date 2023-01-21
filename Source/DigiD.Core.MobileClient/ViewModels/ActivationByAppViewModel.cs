﻿using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DigiD.Common;
using DigiD.Common.Constants;
using DigiD.Common.Enums;
using DigiD.Common.Http.Enums;
using DigiD.Common.Interfaces;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Models;
using DigiD.Common.Models.ResponseModels;
using DigiD.Common.SessionModels;
using DigiD.Common.Settings;
using DigiD.Helpers;
using QRCoder;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
#if A11YTEST
using DigiD.Common.Helpers;
#endif

namespace DigiD.ViewModels
{
    internal class ActivationByAppViewModel : BaseViewModel
    {
        public ImageSource QrCodeImage { get; set; }
        public bool QRCodeVisible { get; set; }

        private bool _init;
        private bool _sessionReceived;
        private bool _pendingConfirmed;
        private bool _removeOldApp;

        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

#if A11YTEST
        public ActivationByAppViewModel() : this(new StartSessionResponse
        { AppSessionId = "a11ytest_session_id"
            , At = (int)DateTimeOffset.Now.ToUnixTime()
        }) { }
#endif

        public ActivationByAppViewModel(StartSessionResponse sessionResponse)
        {
            HeaderText = AppResources.AP83Header;
            FooterText = AppResources.AP83Footer;

            PageId = "AP053";

            NavCloseCommand = CloseCommand;

            HttpSession.AppSessionId = sessionResponse.AppSessionId;
            QRCodeVisible = true;

            //Generate QR-code URL
            var data = $"digid-app-auth://app_session_id={sessionResponse.AppSessionId}&host={DependencyService.Get<IGeneralPreferences>().SelectedHost}&verification_code={sessionResponse.VerificationCode}";

            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            var qRCode = new PngByteQRCode(qrCodeData);
            var qrCodeBytes = qRCode.GetGraphic(20);
            
            QrCodeImage = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));

            Device.StartTimer(TimeSpan.FromMinutes(AppConfigConstants.QRCodeValidationTime), () =>
            {
                if (_cts.IsCancellationRequested || !QRCodeVisible)
                {
                    return false;
                }

                _cts?.Cancel(false);

                NavigationService.PopToRoot();
                return false;
            });
        }

        public IAsyncCommand CloseCommand
        {
            get
            {
                return new AsyncCommand(async () =>
                {
                    _cts?.Cancel(false);
                    await App.CancelSession(true, async () => await NavigationService.ShowMessagePage(MessagePageType.ActivationCancelled));
                });
            }
        }

        private async Task StartActivation()
        {
            DialogService.ShowProgressDialog();
            var result = await ActivationHelper.InitSession(_removeOldApp);

            if (result.ApiResult == ApiResult.Ok)
            {
                var activationResult = await ActivationHelper.StartChallenge(result.AppId);

                if (activationResult.ApiResult == ApiResult.Ok)
                {
                    await NavigationService.PushAsync(new PinCodeViewModel(new PinCodeModel(PinEntryType.Enrollment)
                    {
                        IV = activationResult.IV
                    }));
                    DialogService.HideProgressDialog();
                    return;
                }
            }
            else
            {
                switch (result.ErrorMessage)
                {
                    case "too_many_active":
                        await NavigationService.PushModalAsync(new ToManyActiveAppsViewModel(result, async () =>
                        {
                            _removeOldApp = true;
                            await StartActivation();
                        }));
                        break;
                    default:
                        await NavigationService.ShowMessagePage(MessagePageType.ActivationFailed);
                        break;
                }
            }

            DialogService.HideProgressDialog();
        }

        public override void OnAppearing()
        {
            base.OnAppearing();

            if (_init)
                return;

            _init = true;

            Task.Run(async () =>
            {
                //Poll kern to get the status of the app authentication
                var statusResponse = await DependencyService.Get<IEnrollmentServices>().ActivationByAppPolling();

                while ((statusResponse.ApiResult == ApiResult.Pending || statusResponse.ApiResult == ApiResult.PendingConfirmed) && !_cts.IsCancellationRequested)
                {
                    await Task.Delay(1000);
                    statusResponse = await DependencyService.Get<IEnrollmentServices>().ActivationByAppPolling();

                    if (statusResponse.SessionReceived && !_sessionReceived)
                    {
                        SetStep();
                        _sessionReceived = true;
                    }
                    else if (statusResponse.ApiResult == ApiResult.PendingConfirmed && !_pendingConfirmed)
                    {
                        _pendingConfirmed = true;
                    }
                }

                await Task.Delay(100);

                if (_cts.IsCancellationRequested)
                    return;

                switch (statusResponse.ApiResult)
                {
                    //When authentication is success, goto pincode page
                    case ApiResult.Ok:
                        await StartActivation();
                        break;
                    case ApiResult.Cancelled:
                        await NavigationService.ShowMessagePage(MessagePageType.ActivationCancelled);
                        break;
                    case ApiResult.Nok:
                        await NavigationService.ShowMessagePage(MessagePageType.ActivationFailed);
                        break;
                    default:
                        await NavigationService.ShowMessagePage(MessagePageType.ActivationFailed);
                        break;
                }
            });
        }

        private void SetStep()
        {
            QRCodeVisible = false;

            PageId = "AP054";
            TrackView();

            HeaderText = AppResources.AP84Header;
            FooterText = AppResources.AP84Footer;
        }
    }
}
