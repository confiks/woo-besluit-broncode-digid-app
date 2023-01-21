using System.Threading.Tasks;
using DigiD.Common.Models.ResponseModels;

namespace DigiD.Interfaces
{
    public interface IMessageCenterServices
    {
        Task<MessageCenterResponse> GetMessages();
    }
}
