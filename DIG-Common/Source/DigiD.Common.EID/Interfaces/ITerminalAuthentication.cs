using DigiD.Common.EID.CardSteps;
using DigiD.Common.EID.Models;
using Org.BouncyCastle.Crypto.Parameters;

namespace DigiD.Common.EID.Interfaces
{
    public interface ITerminalAuthentication
    {
        ECPublicKeyParameters PublicKey { get; set; }
        Gap GAP { get; set; }
        Certificate DvCert { get; set; }
        byte[] Ricc { get; set; }
    }
}
