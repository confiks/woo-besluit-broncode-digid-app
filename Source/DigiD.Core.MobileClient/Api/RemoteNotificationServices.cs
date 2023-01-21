using System.Threading.Tasks;
using DigiD.Common.Helpers;
using DigiD.Common.Http.Enums;
using DigiD.Common.Http.Models;
using DigiD.Common.Interfaces;
using DigiD.Common.Models.RequestModels;
using DigiD.Common.Settings;
using Xamarin.Forms;

namespace DigiD.Api
{
    public class RemoteNotificationServices : IRemoteNotificationServices
    {
        public async Task<BaseResponse> RegisterNotificationToken(string token, bool enabled)
        {
            if (string.IsNullOrEmpty(token) && enabled)
                return new BaseResponse {ApiResult = ApiResult.Nok};

            return await HttpHelper.PostAsync<BaseResponse>("/apps/notifications/register", new NotificationRegisterRequest
            {
                NotificationToken = token,
                Enabled = enabled
            });
        }

        public async Task<BaseResponse> UpdateNotificationToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return new BaseResponse {ApiResult = ApiResult.Nok};

            return await HttpHelper.PostAsync<BaseResponse>("/apps/notifications/update", new NotificationUpdateRequest
            {
                NotificationToken = token,
                InstanceId = DependencyService.Get<IGeneralPreferences>().InstanceId,
                UserAppId = DependencyService.Get<IMobileSettings>().AppId
            });
        }
    }
}
