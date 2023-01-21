using System.Runtime.Serialization;

namespace DigiD.Common.Enums
{
    public enum EmailStatus
    {
        [EnumMember(Value = "NOT_VERIFIED")]
        NotVerified,
        [EnumMember(Value = "VERIFIED")]
        Verified,
        [EnumMember(Value = "NONE")]
        None
    }
}
