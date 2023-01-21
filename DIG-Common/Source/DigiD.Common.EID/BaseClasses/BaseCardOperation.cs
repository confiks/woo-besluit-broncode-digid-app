using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DigiD.Common.EID.Exceptions;
using DigiD.Common.EID.Interfaces;
using DigiD.Common.NFC.Exceptions;
using DigiD.Common.NFC.Helpers;

namespace DigiD.Common.EID.BaseClasses
{
    public class BaseCardOperation : ICardOperation
    {
        private bool _isInitialized;

        internal virtual void Init()
        {
            _isInitialized = true;
        }

        public List<IStep> Steps { get; } = new List<IStep>();

        public virtual async Task<bool> Execute()
        {
            if (!_isInitialized)
                Init();

            foreach (var step in Steps)
            {
                Debugger.WriteLine($"{step.GetType()}: Started");

                try
                {
                    var stepResult = await step.Execute();

                    if (!stepResult)
                    {
                        Debugger.WriteLine($"{step.GetType()}: Failed");
                        return false;
                    }
                }
                catch (CardLostException)
                {
                    throw;
                }
                catch (TimeoutException)
                {
                    throw;
                }
                catch (CardNotSupportedException)
                {
                    throw;
                }
#pragma warning disable CA1031 // Do not catch general exception types
                catch (Exception ex)
#pragma warning restore CA1031 // Do not catch general exception types
                {
#if DEBUG
                    Debugger.WriteLine(ex.ToString());
#endif
                    return false;
                }

                StepCompleted(step);
                Debugger.WriteLine($"{step.GetType()}: Completed");
            }

            return true;
        }

        public virtual void StepCompleted(IStep stepNumber){}
    }
}
