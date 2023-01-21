using DigiD.Common.Interfaces;
using DigiD.iOS.Helpers;
using DigiD.iOS.Services;
using Xamarin.Essentials;

[assembly: Xamarin.Forms.Dependency(typeof(SigningService))]
namespace DigiD.iOS.Services
{
    internal class SigningService : ISigningService
    {
        private const string Key = "SSSSSS";
        private const string TestKey = "SSSSSSS";

        public bool RemoveKeyPair()
        {
            return SecureEnclaveHelper.Remove(Key);
        }

        public bool IsSupported
        {
            get
            {
                if (DeviceInfo.DeviceType != DeviceType.Physical)
                    return false;

                try
                {
                    if (!SecureEnclaveHelper.Generate(TestKey))
                        return false;

                    var result = SecureEnclaveHelper.Export(TestKey) != null;

                    return result;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    SecureEnclaveHelper.Remove(TestKey);
                }
            }
        }

        public bool HardwareSupport => true;

        public bool GenerateKeyPair()
        {
            return SecureEnclaveHelper.Generate(Key);
        }

        public byte[] ExportPublicKey()
        {
            return SecureEnclaveHelper.Export(Key);
        }

        public byte[] Sign(string data)
        {
            return SecureEnclaveHelper.Sign(Key, data);
        }
    }
}
