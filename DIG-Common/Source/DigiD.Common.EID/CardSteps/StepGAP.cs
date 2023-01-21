using System;
using System.Threading.Tasks;
using DigiD.Common.EID.BaseClasses;
using DigiD.Common.EID.CardSteps.Randomize;
using DigiD.Common.EID.Enums;
using DigiD.Common.EID.Interfaces;
using DigiD.Common.EID.Models;

namespace DigiD.Common.EID.CardSteps
{
    public class StepGap : IStep
    {
        private readonly CardCredentials _credentials;
        private readonly PasswordType _passwordType;
        private readonly GAPType _type;
        private readonly BaseSecureCardOperation _operation;
        private readonly Action _progressChangedAction;
        private readonly EidSessionData _eidSessionData;

        public StepGap(
            CardCredentials credentials, 
            PasswordType passwordType, 
            GAPType type, 
            BaseSecureCardOperation operation,
            Action progressChangedAction, 
            EidSessionData eidSessionData = null)
        {
            _credentials = credentials;
            _passwordType = passwordType;
            _operation = operation;
            _progressChangedAction = progressChangedAction;
            _eidSessionData = eidSessionData;
            _type = type;
        }

        public async Task<bool> Execute()
        {
            _operation.GAP = new Gap(_operation, _credentials, _passwordType, _progressChangedAction, _eidSessionData);

            if (!await _operation.GAP.Init(_type))
                return false;

            if (!await _operation.GAP.Execute())
                return false;

            return true;
        }
    }
}
