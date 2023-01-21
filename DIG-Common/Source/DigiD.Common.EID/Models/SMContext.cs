using System;
using Org.BouncyCastle.Math;

namespace DigiD.Common.EID.Models
{
    public class SMContext
    {
        internal bool IsBAC => _ssc.Length == 8;
        internal byte[] KEnc { get; private set; }
        internal byte[] KMac { get; private set; }

        //Session identifier
        private byte[] _ssc;

        internal byte[] IncrementSSC()
        {
            var increment = new BigInteger("1");
            var newSsc = new BigInteger(_ssc).Add(increment).ToByteArray();
            Buffer.BlockCopy(newSsc, 0, _ssc, _ssc.Length - newSsc.Length, newSsc.Length);
            return _ssc;
        }

        public SMContext()
        {
        }

        public SMContext(byte[] kEnc, byte[] kMac, byte[] ssc)
        {
            KEnc = kEnc;
            KMac = kMac;
            _ssc = ssc;
        }

        internal void Init(byte[] kEnc, byte[] kMac)
        {
            KEnc = kEnc;
            KMac = kMac;
            _ssc = new byte[16];
        }
    }
}
