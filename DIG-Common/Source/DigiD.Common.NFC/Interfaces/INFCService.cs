using System;
using System.Threading.Tasks;
using DigiD.Common.NFC.Enums;
using DigiD.Common.NFC.Models;

namespace DigiD.Common.NFC.Interfaces
{
    public interface INfcService
    {
        Task<bool> HasNFCSupport();
        Task<string[]> GetReaders();
        Task<bool> IsTagConnected();
        bool IsNFCEnabled { get; }
        void OpenNFCSettings();
        Task<bool> StartScanningAsync(bool retry, Func<Task> tagFoundAction, Func<NfcError, Task> errorAction);
        Task StopScanningAsync(string message = null);
        Task<TransceiveResult> SendApduAsync(byte[] command);
        Task<byte[]> GetATR();
        bool IsCancelled { get; set; }
        void Disconnect();
        void UpdateStatus(double percentage);
    }
}
