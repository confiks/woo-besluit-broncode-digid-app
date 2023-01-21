using DigiD.Common.Models.ResponseModels;

namespace DigiD.Common.Models
{
    public class ConfirmModel
    {
        public ConfirmModel(string action, object data = null)
        {
            Action = action;
            ConfirmData = data;
        }

        public WebSessionInfoResponse SessionInfo { get; set; }
        public WidSessionResponse WIDSessionResponse { get; set; }
        public RegisterEmailModel RegisterEmailModel { get; set; }
        
        public string Action { get; }
        public object ConfirmData { get; }
    }
}
