using System;

namespace DigiD.Common.Interfaces
{
    public interface IDemoSettings
    {
        string DemoPin { get; set; }
        bool IsDemo { get; }
        string EmailAddress { get; set; }
        int UserId { get; set; }
        DateTime? EmailCheckDate { get; set; }
        bool? EmailAddressVerified { get; set; }
        bool? TwoFactorEnabled { get; set; }
        
        void Reset();
        void Save();
    }
}
