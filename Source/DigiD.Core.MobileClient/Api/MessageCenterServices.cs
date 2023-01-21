using System.Threading.Tasks;
using DigiD.Common.Helpers;
using DigiD.Common.Models.RequestModels;
using DigiD.Common.Models.ResponseModels;
using DigiD.Common.Settings;
using DigiD.Interfaces;
using Xamarin.Forms;

namespace DigiD.Api
{
    public class MessageCenterServices : IMessageCenterServices
    {
        public async Task<MessageCenterResponse> GetMessages()
        {
            var requestData = new MessageCenterRequest
            {
                Language = DependencyService.Get<IGeneralPreferences>().Language
            };

            return await HttpHelper.PostAsync<MessageCenterResponse>("/apps/notifications/get_notifications", requestData);
        }
    }
}
