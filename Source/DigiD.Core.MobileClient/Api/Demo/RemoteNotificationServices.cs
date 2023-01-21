using System.Threading.Tasks;
using DigiD.Common.Http.Enums;
using DigiD.Common.Http.Models;
using DigiD.Common.Interfaces;
using DigiD.Common.Mobile.Helpers;
using DigiD.Common.Models.RequestModels;
using DigiD.Common.Settings;
using Xamarin.Forms;

namespace DigiD.Api.Demo
{
    public class RemoteNotificationServices : IRemoteNotificationServices
    {
        /// <summary>
        /// /apps/notifications/register
        /// </summary>
        /// <param name="token"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public async Task<BaseResponse> RegisterNotificationToken(string token, bool enabled)
        {
            await Task.Delay(0);

            return DemoHelper.Log("/apps/notifications/register", new NotificationRegisterRequest
            {
                NotificationToken = token,
                Enabled = enabled
            }, new BaseResponse
            {
                ApiResult = ApiResult.Ok
            });
            
        }

        /// <summary>
        /// /apps/notifications/update
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<BaseResponse> UpdateNotificationToken(string token)
        {
            await Task.Delay(0);

            return DemoHelper.Log($"/apps/notifications/update", new NotificationUpdateRequest
            {
                NotificationToken = token,
                InstanceId = DependencyService.Get<IGeneralPreferences>().InstanceId,
                UserAppId = DependencyService.Get<IMobileSettings>().AppId
            }, new BaseResponse
            {
                ApiResult = ApiResult.Ok
            });
        }
    }
}
