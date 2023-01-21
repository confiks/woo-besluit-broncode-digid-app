using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DigiD.Common.Helpers
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1001:Types that own disposable fields should be disposable", Justification = "<Pending>")]
    public sealed class Timer
    {
        private readonly TimeSpan _timeSpan;
        private readonly Func<Task> _callback;
        private readonly bool _continuous;

        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public Timer(TimeSpan timeSpan, Func<Task> callback, bool continuous = true)
        {
            _timeSpan = timeSpan;
            _callback = callback;
            _continuous = continuous;
        }

        public void Start()
        {
            var cts = _cancellationTokenSource; // safe copy
            
            Device.StartTimer(_timeSpan, () =>
            {
                if (cts.IsCancellationRequested)
                {
                    return false;
                }

                AsyncUtil.RunSync(_callback);
                return _continuous; //true to continuous, false to single use
            });
        }

        public void Stop()
        {
            Interlocked.Exchange(ref _cancellationTokenSource, new CancellationTokenSource()).Cancel();
        }
    }
}
