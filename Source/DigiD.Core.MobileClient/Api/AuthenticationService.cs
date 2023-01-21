using System.Threading.Tasks;
using DigiD.Common.Helpers;
using DigiD.Common.Http.Enums;
using DigiD.Common.Http.Models;
using DigiD.Common.Interfaces;
using DigiD.Common.Models.RequestModels;
using DigiD.Common.Models.ResponseModels;
using DigiD.Common.SessionModels;

namespace DigiD.Api
{
    public class AuthenticationService : IAuthenticationService
    {
        public async Task<BaseResponse> SessionStatus()
        {
            if (string.IsNullOrEmpty(AppSession.AuthenticationSessionId))
                return new BaseResponse {ApiResult = ApiResult.SessionNotFound};

            return await HttpHelper.PostAsync<StartSessionBaseResponse>("/apps/session_status", new BaseRequest());
        }

        public async Task<StartSessionBaseResponse> InitSessionAuthentication()
        {
            return await HttpHelper.GetAsync<StartSessionBaseResponse>("/apps/request_session");
        }

        public async Task<ChallengeResponse> Challenge(ChallengeRequest requestData)
        {
            return await HttpHelper.PostAsync<ChallengeResponse>("/apps/challenge_auth", requestData);
        }

        public async Task<ValidatePinResponse> ValidatePin(ValidatePinRequest requestData)
        {
            return await HttpHelper.PostAsync<ValidatePinResponse>("/apps/check_pincode", requestData);
        }

        public async Task<ConfirmResponse> Confirm(ConfirmRequest requestData)
        {
            return await HttpHelper.PostAsync<ConfirmResponse>("/apps/confirm", requestData);
        }

        public async Task<WebSessionInfoResponse> SessionInfo()
        {
            var model = new WebSessionInfoRequest();
            return await HttpHelper.PostAsync<WebSessionInfoResponse>("/apps/web_session_information", model);
        }

        public async Task<ApiResult> AbortAuthentication(string abortCode)
        {
            return (await HttpHelper.PostAsync<BaseResponse>("/apps/abort_authentication", new AbortAuthenticationRequest(abortCode))).ApiResult;
        }
    }
}
