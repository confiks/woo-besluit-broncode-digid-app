using DigiD.Common.Enums;

namespace DigiD.Common.Settings
{
    public interface IMobileSettings
    {
        bool? NotificationPermissionSet { get; set; }
        string NotificationToken { get; set; }
        string SymmetricKey { get; set; }
        string AppId { get; set; }
        string MaskCode { get; set; }
        
        ActivationMethod ActivationMethod { get; set; } 
        ActivationStatus ActivationStatus { get; set; }
        LoginLevel LoginLevel { get; set; }

        void Reset();
        void Save();
    }
}
