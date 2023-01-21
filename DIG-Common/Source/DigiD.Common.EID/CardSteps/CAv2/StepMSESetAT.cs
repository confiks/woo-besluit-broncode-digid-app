using System;
using System.Linq;
using System.Threading.Tasks;
using DigiD.Common.EID.BaseClasses;
using DigiD.Common.EID.Cards;
using DigiD.Common.EID.Helpers;
using DigiD.Common.EID.Interfaces;
using DigiD.Common.EID.Models.CardFiles;
using DigiD.Common.NFC.Enums;

namespace DigiD.Common.EID.CardSteps.CAv2
{
    /// <summary>
    /// Send a MSE command to the PCA to set up CA
    /// See step 12, page 25 of the PCA implementation guidelines.
    /// </summary>
    internal class StepMseSetAT : BaseSecureStep, IStep
    {
        public StepMseSetAT(ISecureCardOperation operation) : base(operation)
        {

        }

        public async Task<bool> Execute()
        {
            byte[] oid;
            byte[] keyReference;

            switch (Operation.GAP.Card.DocumentType)
            {
                case DocumentType.DrivingLicense:
                    {
                        var card = (DrivingLicense)Operation.GAP.Card;
                        oid = card.CardSecurity.OID.GetEncoded().Skip(2).ToArray();
                        keyReference = card.CardSecurity.KeyReference;
                        break;
                    }
                case DocumentType.IDCard:
                    {
                        var dg14 = ((DG14)Operation.GAP.Card.DataGroups[14]);
                        oid = dg14.ChipAuthenticationInfo.OID.GetEncoded().Skip(2).ToArray();
                        keyReference = dg14.ChipAuthenticationInfo.KeyId;
                        break;
                    }
                default:
                    throw new NotImplementedException();
            }

            var command = CommandApduBuilder.GetMSESetATCommand(oid, keyReference, Operation.GAP.SMContext);
            var response = await CommandApduBuilder.SendAPDU("CA Set AT", command, Operation.GAP.SMContext);

            return response.SW == 0x9000;
        }
    }
}
