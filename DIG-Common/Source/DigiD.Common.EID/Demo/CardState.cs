using DigiD.Common.EID.Constants;

namespace DigiD.Common.EID.Demo
{
    public class CardState
    {
        public int PINTries { get; set; } = WidConstants.PINTries;
        public double CANDelay { get; set; } = 1;
        public string PIN { get; set; } = "SSSSS";
        
        public string NewPIN { get; set; }
        public bool IsSuspended => PINTries == 1;
        public bool IsBlocked => PINTries == 0;
        public bool ChangePinRequired { get; set; } = true;
    }
}
