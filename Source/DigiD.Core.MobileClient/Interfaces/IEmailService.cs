using System.Threading.Tasks;
using DigiD.Common.Http.Models;
using DigiD.Common.Models.ResponseModels;

namespace DigiD.Interfaces
{
    public interface IEmailService
    {
        Task<EmailStatusResponse> Status();
        Task<EmailRegisterResponse> Register(string emailAddress);
        Task<EmailVerifyResponse> Verify(string code);
        Task<BaseResponse> Confirm(bool isValid);
    }
}
