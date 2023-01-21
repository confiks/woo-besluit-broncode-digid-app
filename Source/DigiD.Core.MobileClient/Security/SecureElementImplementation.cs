using DigiD.Common.Interfaces;
using Xamarin.Forms;

namespace DigiD.Security
{
    internal class SecureElementImplementation : ISigningService
    {
        public bool IsSupported => DependencyService.Get<ISigningService>().IsSupported;
        public bool HardwareSupport => DependencyService.Get<ISigningService>().HardwareSupport;
        public bool RemoveKeyPair()
        {
            return DependencyService.Get<ISigningService>().RemoveKeyPair();
        }

        public bool GenerateKeyPair()
        {
            return DependencyService.Get<ISigningService>().GenerateKeyPair();
        }

        public byte[] ExportPublicKey()
        {
            return DependencyService.Get<ISigningService>().ExportPublicKey();
        }

        public byte[] Sign(string data)
        {
            return DependencyService.Get<ISigningService>().Sign(data);
        }
    }
}
