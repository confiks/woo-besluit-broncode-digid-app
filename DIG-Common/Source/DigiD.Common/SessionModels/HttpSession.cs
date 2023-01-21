using DigiD.Common.Enums;
using DigiD.Common.Settings;
using Xamarin.Forms;

namespace DigiD.Common.SessionModels
{
    public static class HttpSession
    {
        public static string HostName => $"https://{DependencyService.Get<IGeneralPreferences>().SelectedHost}";

        public static string SourceApplication { get; set; }
        public static string AppSessionId { get; set; }
        public static string WebService { get; set; }

        public static string VerificationCode { get; set; }
        public static TempSessionData TempSessionData { get; set; }
        public static ActivationSessionData ActivationSessionData { get; set; }
        public static RdaSession RDASessionData { get; set; }
        public static Browser Browser { get; set; }
        public static bool IsApp2AppSession { get; set; }
        public static bool IsWeb2AppSession { get; set; }
        public static bool IsApp2AppSessionStarted { get; set; }
    }

#if READ_PHOTO
    public class MrzCodeResponse : DigiD.Common.Http.Models.BaseResponse
    {
        //public readonly static Lazy<MrzCodeResponse> Instance = new Lazy<MrzCodeResponse>(() => new MrzCodeResponse());

        //private MrzCodeResponse()
        //{
        //    //var appId = DependencyService.Get<IMobileSettings>().AppId;
        //    //var mrzCodes = new MrzCodeResponse(); //DependencyService.Get<IStubServices>().Mrz(appId);
        //}
        public string MrzDrivingLicense { get; set; }   //SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS
        public string MrzIdentityCard { get; set; } //SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS
    }
#endif
}
