using System.Threading;
using System.Threading.Tasks;
using DigiD.Common.Helpers;
using DigiD.Common.Http.Enums;
using DigiD.Common.Interfaces;
using DigiD.Common.Models.RequestModels;
using DigiD.Common.Models.ResponseModels;

namespace DigiD.Api
{
    public class RequestStationServices : IRequestStationServices
    {
        public async Task<RequestStationSessionResponse> InitSessionRequestStation(RequestStationSessionRequest request)
        {
            return await HttpHelper.PostAsync<RequestStationSessionResponse>("/apps/request_station/request_session", request);
        }

        public async Task<CompleteActivationPollResponse> CompleteActivationPoll(CompleteActivationPollRequest request, CancellationToken ct)
        {
            var result = await HttpHelper.PostAsync<CompleteActivationPollResponse>("/apps/request_station/complete_activation_poll", request);

            while (result.ApiResult == ApiResult.Pending && !ct.IsCancellationRequested)
            {
                result = await HttpHelper.PostAsync<CompleteActivationPollResponse>("/apps/request_station/complete_activation_poll", request);
                await Task.Delay(1000, CancellationToken.None);
            }

            if (ct.IsCancellationRequested)
                result.ApiResult = ApiResult.Aborted;

            return result;
        }
    }
}
