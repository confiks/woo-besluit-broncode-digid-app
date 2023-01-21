using System.Security;
using DigiD.Common.EID.BaseClasses;
using DigiD.Common.EID.CardSteps.FileOperations;
using DigiD.Common.EID.Interfaces;

namespace DigiD.Common.EID.CardSteps.BAC
{
    public class BacOperation : BaseCardOperation, IStep
    {
        internal const string ENC_C = "00-00-00-01";
        internal const string MAC_C = "00-00-00-02";

        private readonly Gap _operation;
        
        public SecureString Mrz { get; protected set; }
        public byte[] KSeed { get; set; }
        public byte[] KEnc { get; set; }
        public byte[] KMac { get; set; }
        public byte[] RND_IC { get; set; }

        internal BacOperation(Gap operation, SecureString mrz)
        {
            _operation = operation;
            Mrz = mrz;
        }

        internal override void Init()
        {
            base.Init();

            Steps.Add(new StepSelecteDLApplication(_operation));
            Steps.Add(new StepCalculateKSeed(this));
            Steps.Add(new StepCalculateBasicAccessKeys(this));
            Steps.Add(new StepRequestRandomNumber(this));
            Steps.Add(new StepGeneralAuthenticate(_operation, this));
            Steps.Add(new StepReadEFCOM(_operation));
        }
    }
}
