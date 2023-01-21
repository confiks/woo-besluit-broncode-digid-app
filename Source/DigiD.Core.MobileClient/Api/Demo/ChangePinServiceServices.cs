using System.Threading.Tasks;
using DigiD.Common.Constants;
using DigiD.Common.Helpers;
using DigiD.Common.Http.Enums;
using DigiD.Common.Http.Models;
using DigiD.Common.Mobile.Helpers;
using DigiD.Common.Models.RequestModels;
using DigiD.Common.Models.ResponseModels;
using DigiD.Interfaces;

namespace DigiD.Api.Demo
{
    public class ChangePinServiceServices : IChangePinService
    {
        /// <summary>
        /// /apps/change_pin/request_session
        /// </summary>
        /// <returns></returns>
        public async Task<StartSessionBaseResponse> InitSession()
        {
            await Task.Delay(0);

            return DemoHelper.Log("/apps/change_pin/request_session", new BaseRequest(), new StartSessionBaseResponse
            {
                ApiResult = ApiResult.Ok,
                AppSessionId = DemoHelper.NewSession(AuthenticationActions.ChangeAppPIN),
                At = DateHelper.GetEpochSeconds()
            });
        }

        /// <summary>
        /// /apps/change_pin/request_pin_change
        /// </summary>
        /// <param name="encryptedPIN"></param>
        /// <returns></returns>
        public async Task<BaseResponse> ChangePIN(string encryptedPIN)
        {
            await Task.Delay(0);
            return DemoHelper.Log($"/apps/change_pin/request_pin_change", new ChangePinRequest(encryptedPIN), new BaseResponse
            {
                ApiResult = ApiResult.Ok
            });
        }
    }
}
