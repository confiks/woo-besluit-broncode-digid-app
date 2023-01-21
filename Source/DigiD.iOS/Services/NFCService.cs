using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CoreNFC;
using DigiD.Common;
using DigiD.Common.NFC.Enums;
using DigiD.Common.NFC.Exceptions;
using DigiD.Common.NFC.Interfaces;
using DigiD.Common.NFC.Models;
using DigiD.iOS.Helpers;
using DigiD.iOS.Services;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(NfcService))]
namespace DigiD.iOS.Services
{
    public class NfcService : INfcService
    {
        private NFCTagReaderSession _session;
        private ReaderSession _nfcReaderSession;
        private bool? _hasSupport;
        private string _baseMessage;

        public Task<bool> HasNFCSupport()
        {
            if (_hasSupport == null)
            {
                _hasSupport = UIDevice.CurrentDevice.CheckSystemVersion(13, 0) && NFCReaderSession.ReadingAvailable;
            }
            
            return Task.FromResult(_hasSupport.Value);
        }

        public Task<string[]> GetReaders()
        {
            return Task.FromResult(new[] { "iPhone Reader" });
        }

        public Task<bool> IsTagConnected()
        {
            return Task.FromResult(_session?.ConnectedTag != null);
        }

        public bool IsNFCEnabled => true;
        public bool IsCancelled { get ; set ; }

        public void OpenNFCSettings()
        {
           //No implementation needed for iOS
        }

        public Task<bool> StartScanningAsync(bool retry, Func<Task> tagFoundAction, Func<NfcError, Task> errorAction)
        {
            if (retry)
            {
                _baseMessage = string.Format(CultureInfo.InvariantCulture,AppResources.RDAReStartScanning_iOS, AppResources.eID.ToLowerInvariant(), "iPhone");
                _nfcReaderSession?.Reset();
                _session.AlertMessage = _baseMessage;
                _session.RestartPolling();

                return Task.FromResult(true);
            }

            _nfcReaderSession = new ReaderSession(tagFoundAction, errorAction, () =>
            {
                _session?.Dispose();
                _session = null;
                _nfcReaderSession = null;
            });

            _session = new NFCTagReaderSession(NFCPollingOption.Iso14443 | NFCPollingOption.Iso15693, _nfcReaderSession, null);
            _baseMessage = string.Format(CultureInfo.InvariantCulture, AppResources.RDAStartScanning_iOS, AppResources.eID.ToLowerInvariant(), "iPhone");
            _session.AlertMessage = _baseMessage;
            _session.BeginSession();

            return Task.FromResult(true);
        }

        public Task StopScanningAsync(string message = null)
        {
            if (!string.IsNullOrEmpty(message))
                _session?.InvalidateSession(message);
            else
            {
                if (_session != null)
                    _session.AlertMessage = string.Empty;
                _session?.InvalidateSession();
            }


            _session?.Dispose();
            _session = null;
            _nfcReaderSession = null;

            return Task.CompletedTask;
        }

        public Task<TransceiveResult> SendApduAsync(byte[] command)
        {
            var task = new TaskCompletionSource<TransceiveResult>();

            var handler = new NFCIso7816SendCompletionHandler((responseData, sw1, sw2, error) =>
            {
                var result = new TransceiveResult();

                if (IsCancelled)
                    result.Exception = new TaskCanceledException();
                else if (error != null)
                {
                    result.Exception = error.Code == 100 ? new CardLostException() : new Exception(error.ToString());
                }
                else
                    result.Data = responseData.ToArray().Concat(new[] { sw1, sw2 }).ToArray();

                task.TrySetResult(result);
            });

            if (_nfcReaderSession == null || _nfcReaderSession.Tag == null)
                return Task.FromResult(new TransceiveResult { Exception = new CardLostException() });

            _nfcReaderSession.Tag.SendCommand(new NFCIso7816Apdu(NSData.FromArray(command)), handler);

            return task.Task;
        }

        public Task<byte[]> GetATR()
        {
            var bytes = _nfcReaderSession?.Tag?.HistoricalBytes?.ToArray();
            return Task.FromResult(bytes);
        }   

        public void Disconnect() 
        {
            _session.InvalidateSession();
            _session.Dispose();
            _session = null;
            _nfcReaderSession = null;
        }

        public void UpdateStatus(double percentage)
        {
            if (_session != null)
            {
                var percentagePart = GetPercentageRounds(percentage);
                _session.AlertMessage = $"{percentagePart}\r\n{_baseMessage}";
            }
        }

        private static string GetPercentageRounds(double percentage)
        {
            if (percentage == 0)
                return "⚪⚪⚪⚪⚪⚪⚪⚪⚪⚪";
            if (percentage > 0.0 && percentage <= 0.1)
                return "🔵⚪⚪⚪⚪⚪⚪⚪⚪⚪";
            if (percentage > 0.1 && percentage <= 0.2)
                return "🔵🔵⚪⚪⚪⚪⚪⚪⚪⚪";
            if (percentage > 0.2 && percentage <= 0.3)
                return "🔵🔵🔵⚪⚪⚪⚪⚪⚪⚪";
            if (percentage > 0.3 && percentage <= 0.4)
                return "🔵🔵🔵🔵⚪⚪⚪⚪⚪⚪";
            if (percentage > 0.4 && percentage <= 0.5)
                return "🔵🔵🔵🔵🔵⚪⚪⚪⚪⚪";
            if (percentage > 0.5 && percentage <= 0.6)
                return "🔵🔵🔵🔵🔵🔵⚪⚪⚪⚪";
            if (percentage > 0.6 && percentage <= 0.7)
                return "🔵🔵🔵🔵🔵🔵🔵⚪⚪⚪";
            if (percentage > 0.7 && percentage <= 0.8)
                return "🔵🔵🔵🔵🔵🔵🔵🔵⚪⚪";
            if (percentage > 0.8 && percentage <= 0.9)
                return "🔵🔵🔵🔵🔵🔵🔵🔵🔵⚪";

            return "🔵🔵🔵🔵🔵🔵🔵🔵🔵🔵";
        }
    }
}
