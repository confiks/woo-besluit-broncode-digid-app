using System.Threading.Tasks;
using DigiD.Common.Helpers;
using DigiD.Common.Http.Enums;
using DigiD.Common.Http.Models;
using DigiD.Common.Interfaces;
using DigiD.Common.Models.RequestModels;
using DigiD.Common.Models.ResponseModels;
using DigiD.Common.SessionModels;


namespace DigiD.Common.Api
{
    public class EIDServices : IEIDServices
    {

        /// <summary>
        /// De app stuurt een bericht hiernaar toe om aan te geven dat de app nooit de RDA server heeft kunnen bereiken.
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        public async Task<WidSessionResponse> InitSessionEID(WidSessionRequest requestData)
        {
            var response = await HttpHelper.PostAsync<WidSessionResponse>("/apps/wid/new", requestData);
            return response;
        }

        /// <summary>
        /// Bevestigen van authenticatie voor Hoog
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult> Confirm()
        {
            return (await HttpHelper.PostAsync<BaseResponse>("/apps/wid/confirm", new BaseEIDRequest())).ApiResult;
        }

        /// <summary>
        /// Hoog cancelled endpoint
        /// </summary>
        public async Task<ApiResult> CancelAuthenticate()
        {
            if (string.IsNullOrEmpty(HttpSession.AppSessionId))
                return ApiResult.Ok;

            if (!string.IsNullOrEmpty(HttpSession.HostName))
                return (await HttpHelper.PostAsync<BaseResponse>("/apps/wid/cancel_authentication", new BaseEIDRequest())).ApiResult;
            
            return ApiResult.Aborted;
        }

        /// <summary>
        /// Hoog abort endpoint
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        public async Task<ApiResult> AbortAuthentication(AbortAuthenticationRequest requestData)
        {
            if (string.IsNullOrEmpty(HttpSession.AppSessionId))
                return ApiResult.Ok;

            if (!string.IsNullOrEmpty(HttpSession.HostName))
                return (await HttpHelper.PostAsync<BaseResponse>("/apps/wid/abort_authentication", requestData)).ApiResult;

            return ApiResult.Aborted;
        }

        /// <summary>
        /// Polling endpoint voor de app om te checken of het inloggen met rijbewijs gelukt is
        /// </summary>
        /// <returns></returns>
        public async Task<BaseResponse> Poll()
        {
            var result = await HttpHelper.PostAsync<BaseResponse>("/apps/wid/poll", new BaseEIDRequest());

            while (result.ApiResult == ApiResult.Pending)
            {
                await Task.Delay(1000);
                result = await HttpHelper.PostAsync<BaseResponse>("/apps/wid/poll", new BaseEIDRequest());
            }

            return result;
        }
    }
}
