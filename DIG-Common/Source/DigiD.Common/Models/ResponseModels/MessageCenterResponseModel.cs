using System.Collections.Generic;
using DigiD.Common.Http.Models;

namespace DigiD.Common.Models.ResponseModels
{
    public class MessageCenterResponseModel : BaseResponse
    {
        public List<Message> Messages { get; set; } = new List<Message>();
    }
}
