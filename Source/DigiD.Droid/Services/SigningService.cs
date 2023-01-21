using DigiD.Common.Helpers;
using DigiD.Common.Interfaces;
using DigiD.Droid.Helpers;
using DigiD.Droid.Services;

[assembly: Xamarin.Forms.Dependency(typeof(SigningService))]
namespace DigiD.Droid.Services
{
    internal class SigningService : ISigningService
    {
        private const string Key = "SSSSSS";
        private const string TestKey = "SSSSSSS";

        public bool GenerateKeyPair()
        {
            return KeyStoreHelper.CreateKeyPairForSigning(Key);
        }

        public byte[] ExportPublicKey()
        {
            return KeyStoreHelper.Export(Key);
        }

        public byte[] Sign(string data)
        {
            return KeyStoreHelper.Sign(data.GetBytes(), Key);
        }

        public bool IsSupported
        {
            get
            {
                try
                {
                    if (!KeyStoreHelper.CreateKeyPairForSigning(TestKey))
                        return false;

                    return KeyStoreHelper.Export(TestKey)?.Length > 0;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    KeyStoreHelper.Delete(TestKey);
                }
            }
        }
        public bool HardwareSupport => KeyStoreHelper.SupportHardwareBackedKeyStore();
        
        public bool RemoveKeyPair()
        {
            return KeyStoreHelper.Delete(Key);
        }
    }
}
