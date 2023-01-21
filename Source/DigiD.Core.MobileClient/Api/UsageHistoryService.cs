using System.Collections.Generic;
using System.Threading.Tasks;
using DigiD.Common.Helpers;
using DigiD.Common.Http.Enums;
using DigiD.Common.Interfaces;
using DigiD.Common.Models;
using DigiD.Common.Settings;
using Xamarin.Forms;

namespace DigiD.Api
{
    public class UsageHistoryService : IUsageHistoryService
    {
        public async Task<List<UsageHistoryModel>> GetUsageHistory(int pageIndex = 0)
        {
            var model = new UsageHistoryRequestModel
            {
                Language = DependencyService.Get<IGeneralPreferences>().Language.ToUpper(),
                PageId = pageIndex
            };

            var result = await HttpHelper.PostAsync<UsageHistoryResponseModel>("/apps/logs/get_logs", model);

            if (result.ApiResult == ApiResult.Ok)
                return result.Items;

            return new List<UsageHistoryModel>();
        }
    }
}
