using System.Security;
using DigiD.Common.EID.Models;
using DigiD.Common.Enums;
using DigiD.Common.Models.ResponseModels;

namespace DigiD.Common.Models
{
    public class PinCodeModel
    {
        public PinEntryType EntryType { get; set; }
        public WebSessionInfoResponse SessionInfo { get; set; }
        public WidSessionResponse EIDSessionResponse { get; }
        public int? TriesLeft { get; }
        public bool WrongCAN { get; set; }
        public Card Card { get; }
        public bool IsStatusChecked { get; set; }
        public bool IsWIDActivation { get; set; }
        public SecureString PIN { get; set; }
        public string IV { get; set; }
        public bool NeedPinChange { get; set; }
        public bool ChangeAppPin { get; set; }
        public bool IsAppAuthentication { get; set; }
        
        public PinCodeModel(PinEntryType entryType, Card card = null, WidSessionResponse eidSessionResponse = null, int? triesLeft = null)
        {
            EntryType = entryType;
            EIDSessionResponse = eidSessionResponse;
            TriesLeft = triesLeft;
            Card = card;
        }
    }
}
