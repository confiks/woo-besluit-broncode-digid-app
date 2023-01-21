using System;
using System.Linq;
using DigiD.Common.EID.Models;
using DigiD.Common.NFC.Enums;
using DigiD.Common.NFC.Models;

namespace DigiD.Common.EID.Helpers
{
    internal partial class CommandApduBuilder
    {
        internal static CommandApdu GetSetDSTCommand(byte[] car, SMContext context)
        {
            var carD0 = new DataObject(new byte[] { 0x83 }, car);
            return new CommandApdu(CLA.PLAIN, INS.MANAGE_SEC_ENV, (int)P1.SET_DST, (int)P2.COMPUTE_DIGITAL_SIGNATURE, carD0.GetEncoded()).Encrypt(context);
        }

        internal static CommandApdu[] GetPSOVerifyCertificateCommand(Certificate certificate, SMContext context)
        {
            var data = certificate.Body.Concat(certificate.Signature).ToArray();
            return new CommandApdu(CLA.PLAIN, INS.PERFORM_SEC_OP, (int)P1.SELECT_MF, (int)P2.CERTIFICATE, data).EncryptMulti(context);
        }

        internal static CommandApdu GetMSESetATCommand(byte[] publicKey, byte[] taInfoOID, byte[] cardHolderReference, SMContext context)
        {
            var cryptographicMechanismReference = new DataObject(new byte[] { 0x80 }, taInfoOID).GetEncoded();
            var publicKeyReference = new DataObject(new byte[] { 0x83 }, cardHolderReference).GetEncoded();
            var ephemeralPublicKey = new DataObject(new byte[] { 0x91 }, publicKey).GetEncoded();

            var data = cryptographicMechanismReference.Concat(publicKeyReference).Concat(ephemeralPublicKey).ToArray();

            return new CommandApdu(CLA.PLAIN, INS.MANAGE_SEC_ENV, (int)P1.SET_DST, (int)P2.SET_AT, data).Encrypt(context);
        }

        internal static CommandApdu GetChallengeCommand(SMContext context)
        {
            return new CommandApdu(CLA.PLAIN, INS.CHALLENGE, 0, 0, Array.Empty<byte>(), 8).Encrypt(context);
        }

        internal static CommandApdu GetExternalAuthenticate(byte[] signedData, SMContext context)
        {
            return new CommandApdu(CLA.PLAIN, INS.EXTERNAL_AUTHENTICATE, 0, 0, signedData).Encrypt(context);
        }
    }
}
