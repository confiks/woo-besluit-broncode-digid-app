using System.Collections.Generic;
using System.Threading.Tasks;
using DigiD.Common.Http.Enums;
using DigiD.Common.Http.Models;
using DigiD.Common.Interfaces;
using DigiD.Common.Models.ResponseModels;
using DigiD.Common.NFC.Helpers;
using DigiD.Common.SessionModels;

namespace DigiD.Common.Api.Demo
{
    public class GeneralService : IGeneralServices
    {
        public async Task<BaseResponse> Cancel(bool cancelledByUser = false, bool timeout = false)
        {
            if (Http.Helpers.DemoHelper.Sessions.ContainsKey(HttpSession.AppSessionId))
                Http.Helpers.DemoHelper.Sessions.Remove(HttpSession.AppSessionId);

            await Task.Delay(100);

            return new BaseResponse
            {
                ApiResult = ApiResult.Ok
            };
        }

        public async Task<ConfigResponse> GetConfig()
        {
            await Task.Delay(100);
            return new ConfigResponse
            {
                ApiResult = ApiResult.Ok,
                MaxPinChangePerDay = 3,
                RDAEnabled = true,
                RequestStationEnabled = true,
                LetterRequestDelay = 3,
            };
        }

        public Task<bool> SslPinningCheck()
        {
            return Task.FromResult(true);
        }

        public async Task<AppServiceResponse> GetServices()
        {
            Debugger.WriteLine("/apps/services");
            await Task.Delay(0);
            return new AppServiceResponse
            {
                Services = new List<App>
                {
                    new App {Name = Constants.DemoConstants.MijnDigidAppName, Url = "https://mijn.digid.nl/authn_app"}
                },
                ApiResult = ApiResult.Ok,
            };
        }
    }
}
