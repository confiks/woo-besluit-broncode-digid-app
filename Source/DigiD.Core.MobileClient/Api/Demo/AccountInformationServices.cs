using System.Threading.Tasks;
using DigiD.Common.Constants;
using DigiD.Common.Enums;
using DigiD.Common.Http.Enums;
using DigiD.Common.Interfaces;
using DigiD.Common.Mobile.Helpers;
using DigiD.Common.Models.RequestModels;
using DigiD.Common.Models.ResponseModels;
using DigiD.Common.Settings;
using Xamarin.Forms;

namespace DigiD.Api.Demo
{
    public class AccountInformationServices : IAccountInformationServices
    {
        public async Task<TwoFactorResponseModel> GetTwoFactorStatus()
        {
            await Task.Delay(20);

            var result = new TwoFactorResponseModel();

            if (DependencyService.Get<IMobileSettings>().ActivationMethod == ActivationMethod.RequestNewDigidAccount)
            {
                result.ApiResult = ApiResult.Nok;
                result.ErrorMessage = "no_username_password";
                return result;
            }

            result.ApiResult = ApiResult.Ok;
            result.Enabled = DependencyService.Get<IDemoSettings>().TwoFactorEnabled ?? DemoHelper.CurrentUser.TwoFactorEnabled;

            return DemoHelper.Log("/apps/two_factor/get_two_factor", new BaseRequest(), result);
        }

        public async Task<AccountStatusResponse> GetAccountStatus()
        {
            await Task.Delay(20);

            return new AccountStatusResponse
            {
                ApiResult = ApiResult.Ok,
                EmailStatus = EmailStatus.Verified,
                UnreadMessagesCount = 3,
                TwoFactorEnabled = DependencyService.Get<IDemoSettings>().TwoFactorEnabled ?? false,
                ClassifiedDeceased = DebugConstants.IsClassifiedDeceased
            };
        }

        public async Task<ApiResult> SetTwoFactor(bool enabled)
        {
            await Task.Delay(20);
            DependencyService.Get<IDemoSettings>().TwoFactorEnabled = enabled;

            return DemoHelper.Log("/apps/two_factor/change_two_factor", new { enabled }, ApiResult.Ok);
        }
    }
}
