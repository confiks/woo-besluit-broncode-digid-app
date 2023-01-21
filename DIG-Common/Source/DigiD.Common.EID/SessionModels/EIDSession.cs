using System.Net.Http;
using DigiD.Common.EID.Models;
using DigiD.Common.NFC.Interfaces;

namespace DigiD.Common.EID.SessionModels
{
    public class EIDSession
    {
        public static void Init(INfcService nfcService, HttpClient httpClient, bool isDesktop)
        {
            NfcService = nfcService;
            Client = httpClient;
            IsDesktop = isDesktop;
        }

        public static INfcService NfcService { get; private set; }
        public static HttpClient Client { get; private set; }
        public static bool IsDesktop { get; private set; }
        public static Card Card { get; set; }

#if READ_PHOTO
        public static System.Security.SecureString MrzDrivingLicense { get; set; }
        public static System.Security.SecureString MrzIdentityCard { get; set; }
#endif
    }
}
