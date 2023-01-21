namespace DigiD.Common.Models.RequestModels
{
	public class StartApp2AppRequest : BaseRequest
    {
        public string SAMLRequest { get; set; }
        public string RelayState { get; set; }
        public string SigAlg { get; set; }
        public string Signature { get; set; }
        public string Type { get; set; }
    }
}
