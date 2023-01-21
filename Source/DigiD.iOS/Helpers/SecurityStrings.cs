using System;
using DigiD.Common.Helpers;
using ObjCRuntime;

namespace DigiD.iOS.Helpers
{
	internal static class SecurityStrings
	{
		static SecurityStrings()
		{
			var handle = Dlfcn.dlopen(Constants.SecurityLibrary, 0);
			
            if (handle == IntPtr.Zero)
            {
                return;
            }

			try
			{
				// keys
				SecAttrTokenID = Dlfcn.GetStringConstant(handle, "kSecAttrTokenID");
				SecAttrKeyType = Dlfcn.GetStringConstant(handle, "kSecAttrKeyType");
				SecAttrKeySizeInBits = Dlfcn.GetStringConstant(handle, "kSecAttrKeySizeInBits");
				SecPrivateKeyAttrs = Dlfcn.GetStringConstant(handle, "kSecPrivateKeyAttrs");
				SecPublicKeyAttrs = Dlfcn.GetStringConstant(handle, "kSecPublicKeyAttrs");
				SecAttrAccessControl = Dlfcn.GetStringConstant(handle, "kSecAttrAccessControl");
				SecAttrIsPermanent = Dlfcn.GetStringConstant(handle, "kSecAttrIsPermanent");
				SecAttrApplicationTag = Dlfcn.GetStringConstant(handle, "kSecAttrApplicationTag");
                SecAttrCanSign = Dlfcn.GetStringConstant(handle, "kSecAttrCanSign");
				SecAttrLabel = Dlfcn.GetStringConstant(handle, "kSecAttrLabel");
                SecUseAuthenticationContext = Dlfcn.GetStringConstant(handle, "kSecUseAuthenticationContext");

                // values
                SecAttrTokenIDSecureEnclave = Dlfcn.GetStringConstant(handle, "kSecAttrTokenIDSecureEnclave");
				SecAttrKeyTypeEC = Dlfcn.GetStringConstant(handle, "kSecAttrKeyTypeEC");
			}
			catch (Exception e)
			{
                AppCenterHelper.TrackError(e);
			}
			finally
			{
				Dlfcn.dlclose(handle);
			}
		}

		public static string SecAttrKeyType { get; }
		public static string SecAttrTokenID { get; }
		public static string SecAttrKeySizeInBits { get; }
		public static string SecPrivateKeyAttrs { get; }
		public static string SecPublicKeyAttrs { get; }
		public static string SecAttrAccessControl { get; }
		public static string SecAttrIsPermanent { get; }
		public static string SecAttrApplicationTag { get; }
        public static string SecAttrCanSign { get; }
		public static string SecAttrTokenIDSecureEnclave { get; }
		public static string SecAttrKeyTypeEC { get; }
		public static string SecAttrLabel { get; }
        public static string SecUseAuthenticationContext { get; }
    }
}
