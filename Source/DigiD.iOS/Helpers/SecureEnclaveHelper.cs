using System.Collections.Generic;
using DigiD.Common.Constants;
using DigiD.Common.Helpers;
using Foundation;
using Security;
using static DigiD.iOS.Helpers.SecurityStrings;

namespace DigiD.iOS.Helpers
{
    internal static class SecureEnclaveHelper
    {
        internal static bool Remove(string key)
        {
            var result = SecKeyChain.Remove(GetQuery(key, null));
            return result == SecStatusCode.Success;
        }

        internal static bool Generate(string key)
        {
            Remove(key);

            var sacObject = new SecAccessControl(SecAccessible.WhenUnlockedThisDeviceOnly, SecAccessControlCreateFlags.PrivateKeyUsage);

            var privateKey = key;
            var publicKey = key;

            var parameters = new Dictionary<string, NSObject>
            {
                [SecAttrKeyType] = new NSString(SecAttrKeyTypeEC),
                [SecAttrKeySizeInBits] = new NSNumber(256),
                [SecAttrApplicationTag] = NSData.FromString(StorageConstants.AppTag),
                [SecAttrTokenID] = new NSString(SecAttrTokenIDSecureEnclave),
                [SecAttrLabel] = NSData.FromString(key),

                // private key attributes
                [SecPrivateKeyAttrs] = new Dictionary<string, NSObject>
                {
                    [SecAttrAccessControl] = NSObject.FromObject(sacObject),
                    [SecAttrIsPermanent] = new NSNumber(true),
                    [SecAttrLabel] = NSData.FromString(privateKey),
                    [SecAttrCanSign] = new NSNumber(true),
                    [SecAttrApplicationTag] = NSData.FromString(StorageConstants.AppTag),
                }.ToNSDictionary(),

                // public key attributes
                [SecPublicKeyAttrs] = new Dictionary<string, NSObject>
                {
                    [SecAttrIsPermanent] = new NSNumber(true),
                    [SecAttrLabel] = NSData.FromString(publicKey),
                    [SecAttrCanSign] = new NSNumber(false),
                    [SecAttrApplicationTag] = NSData.FromString(StorageConstants.AppTag),
                }.ToNSDictionary(),
            };

            var result = SecKey.GenerateKeyPair(parameters.ToNSDictionary(), out var pubKey, out var priKey);

            return result == SecStatusCode.Success && pubKey != null && priKey != null;
        }

        internal static byte[] Export(string key)
        {
            var keyPair = GetKeyPair(key, false);
            return keyPair?.GetPublicKey().GetExternalRepresentation().ToArray();
        }

        internal static byte[] Decrypt(string key, byte[] cipherText)
        {
            var keyPair = GetKeyPair(key, true);
            var plaintText = keyPair.CreateDecryptedData(SecKeyAlgorithm.EciesEncryptionCofactorVariableIvx963Sha256AesGcm, NSData.FromArray(cipherText), out var error);
            return error == null ? plaintText.ToArray() : null;
        }

        internal static byte[] Sign(string key, string data)
        {
            var keyPair = GetKeyPair(key, true);
            var result = keyPair.RawSign(SecPadding.PKCS1, data.Hash(), out var signature);
            return result == SecStatusCode.Success ? signature : null;
        }

        private static SecRecord GetQuery(string key, bool? privateKey)
        {
            var query = new SecRecord(SecKind.Key)
            {
                ApplicationTag = StorageConstants.AppTag,
                Label = key
            };

            if (privateKey.HasValue)
                query.CanSign = privateKey.Value;

            return query;
        }

        private static SecKey GetKeyPair(string key, bool privateKey)
        {
            var keyPair = (SecKey)SecKeyChain.QueryAsConcreteType(GetQuery(key, privateKey), out var c);

            if (c != SecStatusCode.Success && c != SecStatusCode.ItemNotFound)
                return null;

            return keyPair;
        }
    }
}
