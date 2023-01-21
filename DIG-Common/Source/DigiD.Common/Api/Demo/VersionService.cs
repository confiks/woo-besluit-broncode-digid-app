using System.Threading.Tasks;
using DigiD.Common.Http.Enums;
using DigiD.Common.Interfaces;
using DigiD.Common.Models.ResponseModels;

namespace DigiD.Common.Api.Demo
{
    public class VersionService : IVersionService
    {
        public async Task<VersionCheckResponse> CheckVersion()
        {
            await Task.Delay(100);
            return new VersionCheckResponse
            {
                ApiResult = ApiResult.Ok,
                Action = "active"
            };
        }
    }
}
