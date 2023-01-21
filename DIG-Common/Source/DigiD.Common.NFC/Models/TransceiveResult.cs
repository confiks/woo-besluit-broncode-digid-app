using System;
using DigiD.Common.NFC.Exceptions;

namespace DigiD.Common.NFC.Models
{
    public class TransceiveResult
    {
        public byte[] Data { get; set; }
        public Exception Exception { get; set; }
        public bool Success => Exception == null;
        public bool IsCardLost => Exception is CardLostException;
    }
}
