using System.Threading.Tasks;
using DigiD.Common.Http.Models;
using DigiD.Common.Models.ResponseModels;

namespace DigiD.Interfaces
{
    public interface IChangePinService
    {
        Task<StartSessionBaseResponse> InitSession();
        Task<BaseResponse> ChangePIN(string encryptedPIN);
    }
}
