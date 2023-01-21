using System.Threading.Tasks;
using DigiD.Common.Helpers;
using DigiD.Common.Http.Models;
using DigiD.Common.Models.RequestModels;
using DigiD.Common.Models.ResponseModels;
using DigiD.Interfaces;

namespace DigiD.Api
{
    public class ChangePinServiceServices : IChangePinService
    {
        public async Task<StartSessionBaseResponse> InitSession()
        {
            var response = await HttpHelper.PostAsync<StartSessionBaseResponse>("/apps/change_pin/request_session", new BaseRequest());
            return response;
        }

        public async Task<BaseResponse> ChangePIN(string encryptedPIN)
        {
            return await HttpHelper.PostAsync<BaseResponse>("/apps/change_pin/request_pin_change", new ChangePinRequest(encryptedPIN));
        }
    }
}
