using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using DigiD.Common;
using DigiD.Common.Enums;
using DigiD.Common.Http.Enums;
using DigiD.Common.Interfaces;
using DigiD.Common.Mobile.BaseClasses;
using Microsoft.AppCenter.Crashes;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DigiD.ViewModels
{
    public class NoSmsViewModel : BaseViewModel
    {
        private int _seconds = -1;

#if A11YTEST
        public NoSmsViewModel( ) : this("0611223344")
        {
            FooterText = "Alleen deze melding mag getoond worden.";
            IsError = true;
        }
#endif

        public NoSmsViewModel(string mobileNumber)
        {
            PageId = "AP047";
            HeaderText = AppResources.NoSMSHeader;
            FooterText = string.Format(CultureInfo.InvariantCulture, AppResources.NoSMSReceivedMessage2, mobileNumber);
            HasBackButton = true;
        }

        public AsyncCommand ResendSpokenSMSCommand
        {
            get
            {
                return new AsyncCommand(async () =>
                {
                    await ResendSms(true);
                }, () => _seconds < 0);
            }
        }

        public AsyncCommand ResendSMSCommand
        {
            get
            {
                return new AsyncCommand(async () =>
                {
                    await ResendSms(false);
                }, () => _seconds < 0);
            }
        }


        private async Task ResendSms(bool spoken)
        {
            _seconds = -1;

            var result = await DependencyService.Get<IEnrollmentServices>().ResendSMS(spoken);
            IsError = false;

            switch (result.ApiResult)
            {
                case ApiResult.Ok:
                    {
                        await NavigationService.GoBack();
                        break;
                    }
                case ApiResult.Nok:
                    {
                        IsError = true;
                        switch (result.ErrorMessage)
                        {
                            case "sms_too_fast":
                                try
                                {
                                    _seconds = (int)result.Payload.time_left;

                                    while (_seconds > 0)
                                    {
                                        FooterText = string.Format(CultureInfo.InvariantCulture, AppResources.SingleDeviceSMSTooFast, _seconds);
                                        await Task.Delay(1000);
                                        _seconds--;
                                    }
                                    if (_seconds == 0)
                                    {
                                        FooterText = AppResources.SingleDeviceSMSRetry;
                                        _seconds = -1;
                                    }
                                }
                                catch (Exception e)
                                {
                                    Crashes.TrackError(e, new Dictionary<string, string>
                                    {
                                        {"Error",result.ErrorMessage},
                                        {"Payload",result.Payload ?? "unknown"}
                                    });
                                }

                                return;
                        }
                        break;
                    }
                case ApiResult.HostNotReachable:
                    {
                        IsError = true;
                        FooterText = AppResources.NoInternetConnectionMessage;
                        break;
                    }
                case ApiResult.Unknown:
                    {
                        await NavigationService.ShowMessagePage(MessagePageType.LoginUnknown);
                        break;
                    }
                case ApiResult.SessionNotFound:
                    {
                        await NavigationService.ShowMessagePage(MessagePageType.SessionTimeout);
                        break;
                    }
                case ApiResult.SSLPinningError:
                    {
                        await NavigationService.ShowMessagePage(MessagePageType.SSLException);
                        break;
                    }
                case ApiResult.Forbidden:
                    await App.CheckVersion();
                    break;
            }
        }

        public override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}
