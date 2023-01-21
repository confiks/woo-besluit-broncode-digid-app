using System.Security;
using DigiD.Common.EID.Models;
using DigiD.Common.Enums;
using DigiD.Common.Models.ResponseModels;

namespace DigiD.Common.Models
{
    public class NfcScannerModel
    {
        public PinEntryType Action { get; set; }
        public WidSessionResponse EIDSessionResponse { get; set; }
        public SecureString PIN { get; set; }
        public SecureString NewPIN { get; set; }
        public bool IsStatusChecked { get; set; }
        public bool NeedPinChange { get; set; }
        public bool IsActivation { get; set; }
        public Card Card { get; set; }
    }
}
