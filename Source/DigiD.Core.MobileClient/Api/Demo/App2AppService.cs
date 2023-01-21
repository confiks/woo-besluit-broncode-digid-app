using System.Threading.Tasks;
using DigiD.Common.Http.Enums;
using DigiD.Common.Interfaces;
using DigiD.Common.Mobile.Helpers;
using DigiD.Common.Models;
using DigiD.Common.Models.RequestModels;
using DigiD.Common.Models.ResponseModels;
using DigiD.Common.SessionModels;

namespace DigiD.Api.Demo
{
    public class App2AppService : IApp2AppService
    {
        public async Task<InitApp2AppResponse> InitSessionApp2App(App2AppRequest requestData)
        {
            await Task.Delay(0);
            var result = new InitApp2AppResponse
            {
                ApiResult = ApiResult.Ok,
                WebSessionId = DemoHelper.NewSession("app2app"),
                ImagedDomain = requestData.ReturnUrl
            };

            result.Apps.Add(new Common.Models.ResponseModels.App
            {
                Url = requestData.ReturnUrl,
                Name = "DigiD Demo App"
            });

            DemoHelper.Log(requestData.Destination, requestData, result);

            return result;
        }

        public async Task<SamlArtifactResponse> GetSamlArtifact(SamlArtifactRequest requestData)
        {
            await Task.Delay(0);

            var result = new SamlArtifactResponse
            {
                ApiResult = ApiResult.Ok,
                SAMLart = "demo",
                RelayState = App2AppSession.RelayState
            };

            DemoHelper.Log("/saml/idp/app_to_app_artifact", requestData, result);

            return result;
        }
    }
}
