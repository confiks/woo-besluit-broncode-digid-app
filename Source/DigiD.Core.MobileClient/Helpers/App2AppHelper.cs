﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DigiD.Common;
using DigiD.Common.Enums;
using DigiD.Common.Http.Enums;
using DigiD.Common.Interfaces;
using DigiD.Common.Models;
using DigiD.Common.Models.RequestModels;
using DigiD.Common.SessionModels;
using DigiD.Common.Settings;
using DigiD.ViewModels;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DigiD.Helpers
{
    public static class App2AppHelper
    {
        public static async Task ProcessSamlRequest(string queryData)
        {
            HttpSession.IsApp2AppSessionStarted = false;

            var base64 = queryData.Replace("app-app=", string.Empty);

            if (Common.Helpers.StringHelper.TryGetFromBase64String(base64, out var encodedData))
            {
                var decodedData = Encoding.UTF8.GetString(encodedData);
                var data = JsonConvert.DeserializeObject<App2AppRequest>(decodedData);

                var isValid = await Device.InvokeOnMainThreadAsync(async () =>
                {
                    if (data == null)
                    {
                        await Application.Current.MainPage.DisplayAlert(AppResources.Error, "Session Time-out",
                            AppResources.OK);
                        await DependencyService.Get<INavigationService>().PopToRoot();
                        return false;
                    }

                    if (string.IsNullOrEmpty(data.ReturnUrl))
                    {
                        await Application.Current.MainPage.DisplayAlert(AppResources.Error,
                            "Controleer parameters, ReturnURL is niet aanwezig", AppResources.OK);
                        await DependencyService.Get<INavigationService>().PopToRoot();
                        return false;
                    }

                    if (!data.IsValid())
                    {
                        await Application.Current.MainPage.DisplayAlert(AppResources.Error,
                            "Controleer parameters, SAMLRequest is ongeldig", AppResources.OK);
                        await DependencyService.Get<INavigationService>().PopToRoot();
                        return false;
                    }

                    return true;
                });

                if (!isValid)
                    return;

                App2AppSession.AppReturnUrl = data!.ReturnUrl;

                if ((DateTime.Now - data.IssueInstant).TotalMinutes > 1)
                {
                    await ReturnToClientApp("timeout");
                    return;
                }

                if (string.IsNullOrEmpty(data.Icon))
                {
                    await ReturnToClientApp("icon_missing");
                    return;
                }

                await ProcessApp2AppAsync(data);
            }
            else
            {
                var props = new Dictionary<string, string> { { "query", queryData } };
                Crashes.TrackError(new InvalidDataException(), props);
            }
        }

        private static async Task ProcessApp2AppAsync(App2AppRequest data)
        {
            if (data == null)
            {
                await DependencyService.Get<INavigationService>().ShowMessagePage(MessagePageType.InvalidScan);
                return;
            }

            HttpSession.IsApp2AppSession = true;

            var sessionData = new App2AppSessionData(data);

            await App.GetActualConfiguration();

            AppSession.IsAppActivated = DependencyService.Get<IMobileSettings>().ActivationStatus == ActivationStatus.Activated;

            if (DependencyService.Get<IMobileSettings>().ActivationStatus == ActivationStatus.NotActivated)
            {
                await IncomingDataHelper.ProcessData(sessionData, true, false);
                DependencyService.Get<INavigationService>().SetPage(new ActivationIntro1ViewModel(false));
            }
            else if (DependencyService.Get<IMobileSettings>().ActivationStatus == ActivationStatus.Pending)
                DependencyService.Get<INavigationService>().SetPage(new NotActivatedPendingViewModel(sessionData));
            else
                await IncomingDataHelper.ProcessData(sessionData, false, false);
        }

        internal static async Task ReturnToClientApp(string error = null)
        {
            if (string.IsNullOrEmpty(App2AppSession.SamlArt))
            {
                var samlResponse = await DependencyService.Get<IApp2AppService>().GetSamlArtifact(new SamlArtifactRequest());

                if (samlResponse.ApiResult == ApiResult.Ok)
                {
                    App2AppSession.RelayState = samlResponse.RelayState;
                    App2AppSession.SamlArt = samlResponse.SAMLart;
                }
            }

            var response = new App2AppResponse
            {
                RelayState = App2AppSession.RelayState,
                SAMLart = App2AppSession.SamlArt,
                ErrorMessage = error
            };

            var json = JsonConvert.SerializeObject(response);
            var encodedData = Encoding.UTF8.GetBytes(json);
            var url = $"{App2AppSession.AppReturnUrl}?app-app={Convert.ToBase64String(encodedData)}";

            if (Uri.TryCreate(url, UriKind.Absolute, out var uri))
                await Launcher.OpenAsync(uri);

            App2AppSession.AppReturnUrl = null;
            App2AppSession.AppIconUrl = null;
            App2AppSession.RelayState = null;
            App2AppSession.SamlArt = null;

            await DependencyService.Get<INavigationService>().PopToRoot(true);
        }

        internal static async Task AbortSession(string reason, bool toRoot = true)
        {
            HttpSession.TempSessionData?.SwitchSession();
            await DependencyService.Get<IAuthenticationService>().AbortAuthentication(reason);

            await App.CancelSession(true, async () =>
            {
                if (toRoot)
                    await DependencyService.Get<INavigationService>().PopToRoot();
            });
        }
    }
}
