using DigiD.Common.EID.Helpers;
using DigiD.Common.EID.Models.SecurityInfoObjects;
using Org.BouncyCastle.Asn1;

namespace DigiD.Common.EID.Models.CardFiles
{
    public class EFCardAccess : File
    {
        public PaceInfo PaceInfo { get; private set; }
        public CAInfo CAInfo { get; private set; }
        public CADomainParameterInfo CADomainParameters { get; private set; }
        public TAInfo TAInfo { get; private set; }

        public EFCardAccess(byte[] data) : base(data, "EF.EF_CardAccess")
        {
        }

        public override bool Parse()
        {
            if (Bytes == null)
                return false;

            foreach (DerSequence securityInfo in (Asn1Set)Asn1Object.FromByteArray(Bytes))
            {
                var info = InfoFactory.Parse(securityInfo);

                switch (info)
                {
                    case TAInfo ta:
                        TAInfo = ta;
                        break;
                    case CAInfo ca:
                        CAInfo = ca;
                        break;
                    case PaceInfo pi:
                        PaceInfo = pi;
                        break;
                    case CADomainParameterInfo d:
                        CADomainParameters = d;
                        break;
                }
            }

            return true;
        }
    }
}
