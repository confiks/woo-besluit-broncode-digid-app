namespace DigiD.Common.Mobile.Models
{
    public class DemoUser
    {
        public int UserId { get; set; }
        public string Bsn { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ActivationMethod { get; set; }
        public string HouseNumber { get; set; }
        public string Postalcode { get; set; }
        public bool HasNoDocuments { get; set; }
        public string DocumentNumber { get; set; }
        public bool RdaFailed { get; set; }
        public string EmailAddress { get; set; }
        public bool UserActionNeeded { get; set; }
        public bool IsEmailAddressVerified { get; set; } = true;
        public bool HasMessages { get; set; } = true;
        public bool IsSmsCheckRequested { get; set; }
        public bool EIDASUser { get; set; }
        public bool EIDASSuccess { get; set; } = true;
        public bool TwoFactorEnabled { get; set; }
    }
}
