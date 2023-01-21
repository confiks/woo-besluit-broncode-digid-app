namespace DigiD.Common.Interfaces
{
    public interface ISigningService
    {
        bool GenerateKeyPair();
        byte[] ExportPublicKey();
        byte[] Sign(string data);
        bool IsSupported { get; }
        bool HardwareSupport { get; }
        bool RemoveKeyPair();
    }
}
