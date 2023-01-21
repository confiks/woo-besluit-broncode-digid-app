namespace DigiD.Models
{
    internal class CheckEmailModel
    {
        public string Action { get; set; }
        public bool Confirmed { get; set; }
        public string EmailAddress { get; set; }
    }
}
