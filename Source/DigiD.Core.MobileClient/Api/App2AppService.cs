using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DigiD.Common.Helpers;
using DigiD.Common.Interfaces;
using DigiD.Common.Models;
using DigiD.Common.Models.RequestModels;
using DigiD.Common.Models.ResponseModels;
using DigiD.Common.Settings;
using Xamarin.Forms;

namespace DigiD.Api
{
    public class App2AppService : IApp2AppService
    {
        public async Task<InitApp2AppResponse> InitSessionApp2App(App2AppRequest requestData)
        {
            if (requestData.Destination.Contains("v4"))
            {
                var request = new HttpRequestMessage(HttpMethod.Post, new Uri(requestData.Destination))
                {
                    Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("SAMLRequest", requestData.SAMLRequest),
                        new KeyValuePair<string, string>("RelayState", requestData.RelayState),
                        new KeyValuePair<string, string>("Type", "app_to_app"),
                    })
                };

                return await HttpHelper.SendAsync<InitApp2AppResponse>(request, true, 10000);
            }

            return await HttpHelper.PostAsync<InitApp2AppResponse>(new Uri(requestData.Destination), new StartApp2AppRequest
            {
                SAMLRequest = requestData.SAMLRequest,
                RelayState = requestData.RelayState,
                SigAlg = requestData.SigAlg,
                Signature = requestData.Signature,
                Type = "app_to_app"
            });
        }

        public async Task<SamlArtifactResponse> GetSamlArtifact(SamlArtifactRequest requestData)
        {
            var host = DependencyService.Get<IGeneralPreferences>().SelectedHost;
            var uri = new Uri($"https://{host}/saml/idp/app_to_app_artifact");
            return await HttpHelper.PostAsync<SamlArtifactResponse>(uri, requestData);
        }
    }
}
