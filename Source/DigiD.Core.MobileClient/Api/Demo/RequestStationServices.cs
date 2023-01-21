using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DigiD.Common.Http.Enums;
using DigiD.Common.Interfaces;
using DigiD.Common.Mobile.Helpers;
using DigiD.Common.Models.RequestModels;
using DigiD.Common.Models.ResponseModels;
using Xamarin.Forms;

namespace DigiD.Api.Demo
{
    public class RequestStationServices : IRequestStationServices
    {
        /// <summary>
        /// /apps/request_station/request_session
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<RequestStationSessionResponse> InitSessionRequestStation(RequestStationSessionRequest request)
        {

            await Task.Delay(0);

            var user = Common.Mobile.Constants.DemoConstants.DemoUsers.FirstOrDefault(x => x.UserName == request.UserName && x.Password == request.Password);

            RequestStationSessionResponse result;

            if (user != null)
            {
                DependencyService.Get<IDemoSettings>().UserId = user.UserId;

                result = new RequestStationSessionResponse
                {
                    ApiResult = ApiResult.Ok,
                    ActivationCode = "R12345",
                    AppSessionId = Guid.NewGuid().ToString()
                };
            }
            else
                result = new RequestStationSessionResponse
                {
                    ApiResult = ApiResult.Nok,
                    ErrorMessage = "invalid"
                };

            return DemoHelper.Log("/apps/request_station/request_session", request, result);
        }

        /// <summary>
        /// /apps/request_station/complete_activation_poll
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<CompleteActivationPollResponse> CompleteActivationPoll(CompleteActivationPollRequest request, CancellationToken ct)
        {
            
            for (var x = 0; x <= 5; x++)
            {
                if (ct.IsCancellationRequested)
                    break;

                await Task.Delay(1000, CancellationToken.None);
            }

            return DemoHelper.Log("/apps/request_station/complete_activation_poll", request, new CompleteActivationPollResponse
            {
                ApiResult = ct.IsCancellationRequested ? ApiResult.Aborted : ApiResult.Ok,
                AppId = Guid.NewGuid().ToString(),
            });
        }
    }
}
