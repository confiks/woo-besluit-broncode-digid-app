using System.Collections.Generic;
using DigiD.Common.Mobile.Models;

namespace DigiD.Common.Mobile.Constants
{
    /// <summary>
    ///SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS
    ///SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS
    ///SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS
    /// Activate app via municipality: enter one of the above credentials
    /// ID-Check will always pass, when a driving license or id-card is offered. If 10 seconds no documents are offered, the alternative activation method will be followed
    /// </summary>
    public static class DemoConstants
    {
        public const string ACTIVATION_CODE = "SSSSSSSSS";
        public const string SMS_CODE = "SSSSSS";
        public const string EMAIL_ACTIVATION_CODE = "SSSSSSSSS";

        public static readonly List<DemoUser> DemoUsers = new List<DemoUser>
        {
            new DemoUser {UserId = 1,UserName = "SSSSS", Password = "SSSSS", ActivationMethod = "standalone", EmailAddress = "SSSSSSSSSSSSSS"},
            new DemoUser {UserId = 2,UserName = "SSSSS", Password = "SSSSS", ActivationMethod = "standalone", RdaFailed = true},
            new DemoUser {UserId = 3,UserName = "SSSSS", Password = "SSSSS", ActivationMethod = "standalone", UserActionNeeded = true, IsEmailAddressVerified = false, EmailAddress = "SSSSSSSSSSSSSSSSSSSSS"},
            new DemoUser {UserId = 4,UserName = "SSSSS", Password = "SSSSS", ActivationMethod = "request_for_account", EmailAddress = "SSSSSSSSSSSSSS"},
            new DemoUser {UserId = 5,UserName = "SSSSS", Password = "SSSSS", ActivationMethod = "request_for_account", EmailAddress = "SSSSSSSSSSSSSS", IsSmsCheckRequested = true},
            new DemoUser {UserId = 6,UserName = "SSSSS", Password = "SSSSS", ActivationMethod = "password", HasMessages = false},
            new DemoUser {UserId = 7,UserName = "SSSSS", Password = "SSSSS", ActivationMethod = "password", RdaFailed = true},
            new DemoUser {UserId = 8,UserName = "SSSSS", Bsn = "SSSSSSSSS", HouseNumber = "1", Postalcode = "SSSSSS"},
            new DemoUser {UserId = 9,UserName = "SSSSS", Password = "SSSSS", ActivationMethod = "standalone", HasNoDocuments = true, DocumentNumber = "SSSSSSSSS"},
            new DemoUser {UserId = 10,UserName = "SSSSS", Password = "SSSSS", ActivationMethod = "standalone", UserActionNeeded = true},
            new DemoUser {UserId = 11,UserName = "SSSSS", Password = "SSSSS", ActivationMethod = "standalone", EIDASUser = true},
            new DemoUser {UserId = 12,UserName = "SSSSS", Password = "SSSSS", ActivationMethod = "standalone", EIDASUser = true, EIDASSuccess = false},
            new DemoUser {UserId = 13,UserName = "SSSSS", Password = "SSSSS", ActivationMethod = "standalone", TwoFactorEnabled = true},
        };
    }
}
