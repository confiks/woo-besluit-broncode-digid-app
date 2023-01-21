using DigiD.Common.Interfaces;
using DigiD.Security;

namespace DigiD.Helpers
{
    internal static class SigningHelper
    {
        internal static ISigningService GetService()
        {
            ISigningService gen = new SecureElementImplementation();

            //If secure-enclave is not supported (iPhone 4S, 5, and 5C) the use the BouncyCastle implementation
            if (!gen.IsSupported)
                gen = new BouncyCastleImplementation();

            return gen;
        }
    }
}
