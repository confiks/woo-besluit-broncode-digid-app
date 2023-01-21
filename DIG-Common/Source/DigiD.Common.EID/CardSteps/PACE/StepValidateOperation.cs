using System.Threading.Tasks;
using DigiD.Common.EID.Interfaces;
using DigiD.Common.EID.Models;
using DigiD.Common.NFC.Enums;
using DigiD.Common.NFC.Helpers;

namespace DigiD.Common.EID.CardSteps.PACE
{
    internal class StepValidateOperation : IStep
    {
        private readonly Gap _gap;

        public StepValidateOperation(Gap gap)
        {
            _gap = gap;
        }
        public Task<bool> Execute()
        {
            if (_gap.Pace.IsSuccess)
            {
                Debugger.WriteLine("PACE was successful");
                
                _gap.SMContext = new SMContext();
                _gap.SMContext.Init(_gap.Pace.KEnc, _gap.Pace.KMac);

                return Task.FromResult(true);
            }

            if (_gap.Card.DocumentType != DocumentType.IDCard && _gap.ChangePinRequired)
            {
                Debugger.WriteLine("PACE partial complete, need change Transport PIN");
                return Task.FromResult(true);
            }

            Debugger.WriteLine($"Terminal token: {_gap.Pace.TerminalAuthToken.ToHexString()}");
            Debugger.WriteLine($"Card token: {_gap.Pace.CardAuthToken.ToHexString()}");
            Debugger.WriteLine("PACE failed");

            return Task.FromResult(false);
        }
    }
}
