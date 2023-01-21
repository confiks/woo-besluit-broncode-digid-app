using System;
using System.Diagnostics;
using DigiD.Common.EID.CardSteps;
using DigiD.Common.EID.Interfaces;

namespace DigiD.Common.EID.BaseClasses
{
    public class BaseSecureCardOperation : BaseCardOperation, ISecureCardOperation
    {
        private readonly Action<float> _progressChangedAction;
        internal int TotalSteps;
        private int _currentIndex = 1;

        public BaseSecureCardOperation(Action<float> progressChangedAction = null)
        {
            _progressChangedAction = progressChangedAction;
        }

        public BaseSecureCardOperation(Gap operation)
        {
            GAP = operation;
        }

        public override void StepCompleted(IStep stepNumber)
        {
            ChangeProgress();
        }

        internal void ChangeProgress()
        {
            Debug.WriteLine($"{_currentIndex}/{TotalSteps}");
            _progressChangedAction?.Invoke((float)_currentIndex / TotalSteps);
            _currentIndex++;
        }

        public Gap GAP { get; set; }
    }
}
