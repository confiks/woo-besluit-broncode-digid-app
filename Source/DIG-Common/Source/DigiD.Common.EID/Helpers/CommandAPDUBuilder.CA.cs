using System.Linq;
using DigiD.Common.EID.Models;
using DigiD.Common.NFC.Enums;
using DigiD.Common.NFC.Models;

namespace DigiD.Common.EID.Helpers
{
    internal static partial class CommandApduBuilder
    {
        internal static CommandApdu GetMSESetATCommand(byte[] caOID, byte[] keyreference, SMContext context)
        {
            var data80 = new DataObject(new byte[] { 0x80 }, caOID).GetEncoded();
            var data84 = new DataObject(new byte[] { 0x84 }, keyreference).GetEncoded();

            var data = data80.Concat(data84).ToArray();

            var command = new CommandApdu(CLA.PLAIN, INS.MANAGE_SEC_ENV, (int)P1.COMPUTE_DIGITAL_SIGNATURE, (int)P2.SET_AT, data);
            return command.Encrypt(context);
        }

        internal static CommandApdu GetGeneralAuthenticate(byte[] publicKey, SMContext context)
        {
            var data80 = new DataObject(new byte[] { 0x80 }, publicKey);
            var data7C = new DataObject(new byte[] { 0x7c }, data80);

            return new CommandApdu(CLA.PLAIN, INS.GENERAL_AUTH, 0, 0, data7C.GetEncoded(), 0x16).Encrypt(context);

        }
    }
}
