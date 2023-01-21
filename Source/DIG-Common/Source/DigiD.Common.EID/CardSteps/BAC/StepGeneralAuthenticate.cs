using System.Linq;
using System.Threading.Tasks;
using DigiD.Common.EID.Helpers;
using DigiD.Common.EID.Interfaces;
using DigiD.Common.EID.Models;
using DigiD.Common.NFC.Enums;
using DigiD.Common.NFC.Models;

namespace DigiD.Common.EID.CardSteps.BAC
{
    public class StepGeneralAuthenticate : IStep
    {
        private readonly Gap _gap;
        private readonly BacOperation _operation;

        public StepGeneralAuthenticate(Gap gap, BacOperation operation)
        {
            _gap = gap;
            _operation = operation;
        }

        private const int EIC_LENGTH = 32;
        private const int RND_LENGTH = 8;
        private const int KIC_LENGTH = 16;

        public async Task<bool> Execute()
        {
            // generate random 8 byte
            var rndIfd = CryptoHelper.GenerateRandom(8);

            // generate random 16 byte
            var kIfd = CryptoHelper.GenerateRandom(16);

            // concat rndInf, rndIc en kIfc
            var s = rndIfd.Concat(_operation.RND_IC).Concat(kIfd).ToArray();

            //Encrypt S with 3DES key KEnc
            var eIfd = CryptoDesHelper.TDesProvider(s, _operation.KEnc, true);

            //Compute MAC over EIFD with 3DES key KMAC
            var mIfd = CryptoDesHelper.ComputeMacTDes(eIfd, _operation.KMac);

            var cmd_data = eIfd.Concat(mIfd).ToArray();

            var cmd = new CommandApdu(CLA.PLAIN, INS.EXTERNAL_AUTHENTICATE, (int)P1.SELECT_MF, (int)P2.DEFAULT_CHANNEL, cmd_data, 0x28);
            var response = await CommandApduBuilder.SendAPDU("GA BAC", cmd, null);

            if (response.SW == 0x9000)
            {
                var eIc = response.Data.Take(EIC_LENGTH).ToArray();
                var sIc = CryptoDesHelper.TDesProvider(eIc, _operation.KEnc, false);
                var rndIc_Resp = sIc.Take(RND_LENGTH).ToArray();
                var rndIfd_Resp = sIc.Skip(rndIc_Resp.Length).Take(RND_LENGTH).ToArray();
                var kIc = sIc.Skip(KIC_LENGTH).Take(KIC_LENGTH).ToArray();

                if (rndIfd.SequenceEqual(rndIfd_Resp))
                {
                    var kSeed = ByteHelper.Xor(kIfd, kIc);

                    // calculate KSenc and KSmac
                    var ksEnc = CryptoDesHelper.ComputeKey(kSeed, BacOperation.ENC_C);
                    var ksMac = CryptoDesHelper.ComputeKey(kSeed, BacOperation.MAC_C);

                    // calculate SSC with least significant 4 bytes of rndIc and rndIfd
                    var ssc = rndIc_Resp.Skip(4).Concat(rndIfd_Resp.Skip(4)).ToArray();

                    var smContext = new SMContext(ksEnc, ksMac, ssc);
                    _gap.SMContext = smContext;

                    return true;
                }
            }

            return false;
        }
    }
}
