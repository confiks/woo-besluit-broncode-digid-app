using System.Threading.Tasks;
using DigiD.Common.Helpers;
using DigiD.Common.Http.Models;
using DigiD.Common.Models.RequestModels;
using DigiD.Common.Models.ResponseModels;
using DigiD.Interfaces;

namespace DigiD.Api
{
    public class EmailService : IEmailService
    {
        public async Task<EmailStatusResponse> Status()
        {
            return await HttpHelper.PostAsync<EmailStatusResponse>("/apps/email/status", new BaseMijnDigiDRequest());
        }

        public async Task<EmailRegisterResponse> Register(string emailAddress)
        {
            var requestData = new EmailRegisterRequest
            {
                EmailAddress = emailAddress
            };

            return await HttpHelper.PostAsync<EmailRegisterResponse>("/apps/email/register", requestData);
        }

        public async Task<EmailVerifyResponse> Verify(string code)
        {
            var requestData = new EmailVerifyRequest
            {
                Code = code
            };

            return await HttpHelper.PostAsync<EmailVerifyResponse>("/apps/email/verify", requestData);
        }

        public async Task<BaseResponse> Confirm(bool isValid)
        {
            var requestData = new EmailConfirmRequest
            {
                IsConfirmed = isValid
            };

            return await HttpHelper.PostAsync<EmailVerifyResponse>("/apps/email/confirm", requestData);
        }
    }
}
