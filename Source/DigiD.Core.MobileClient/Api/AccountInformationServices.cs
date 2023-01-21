using System.Threading.Tasks;
using DigiD.Common.Helpers;
using DigiD.Common.Http.Enums;
using DigiD.Common.Http.Models;
using DigiD.Common.Interfaces;
using DigiD.Common.Models.RequestModels;
using DigiD.Common.Models.ResponseModels;

namespace DigiD.Api
{
    public class AccountInformationServices : IAccountInformationServices
    {
        public async Task<TwoFactorResponseModel> GetTwoFactorStatus()
        {
            return await HttpHelper.PostAsync<TwoFactorResponseModel>("/apps/two_factor/get_two_factor", new BaseMijnDigiDRequest());
        }

        public async Task<AccountStatusResponse> GetAccountStatus()
        {
            var result = await HttpHelper.PostAsync<AccountStatusResponse>("/apps/account_status", new BaseMijnDigiDRequest());
            return result;
        }

        public async Task<ApiResult> SetTwoFactor(bool enabled)
        {
            var response = await HttpHelper.PostAsync<BaseResponse>("/apps/two_factor/change_two_factor", new SetTwoFactorRequestModel(enabled));
            
            return response.ApiResult;
        }
    }
}
