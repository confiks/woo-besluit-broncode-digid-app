using System;
using System.Threading.Tasks;
using DigiD.Common.EID.BaseClasses;
using DigiD.Common.EID.Cards;
using DigiD.Common.EID.Constants;
using DigiD.Common.EID.Interfaces;
using DigiD.Common.EID.Models.Network.Requests;
using DigiD.Common.EID.Models.Network.Responses;
using DigiD.Common.EID.SessionModels;
using DigiD.Common.Http.Enums;
using DigiD.Common.Http.Helpers;
using DigiD.Common.NFC.Helpers;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace DigiD.Common.EID.CardSteps.CAv2
{
    internal class StepSendFilesToServer : BaseSecureStep, IStep
    {
        internal StepSendFilesToServer(ISecureCardOperation operation) : base(operation)
        {

        }

        public async Task<bool> Execute()
        {
            var card = (eNIK)Operation.GAP.Card;

            var requestData = new
            {
                efCvca = card.CVCA.Base64Encoded,
                dg14 = card.DataGroups[14].Base64Encoded,
                efSOd = card.EF_SOd.Base64Encoded,
                paceIcc = Operation.GAP.Pace.CardAgreedPublicKey.Q.XCoord.GetEncoded().ToBase64()
            };

            var data = new EIDBaseRequest(requestData, Operation.GAP.SessionData.SessionId);

            var response = await EIDSession.Client.PostAsync<TerminalAuthenticationCommandResponses>(new Uri($"{Operation.GAP.SessionData.ServerAddress}{WidConstants.NIK_PREPARE_EAC_URI}"), data);

            if (response.ApiResult != ApiResult.Ok)
                return false;

            Operation.GAP.ApiResult = response.ApiResult;

            card.TACommands = response.Data.Commands;
            card.CardAuthenticationPublicKey = (ECPublicKeyParameters)PublicKeyFactory.CreateKey(Convert.FromBase64String(response.Data.EphemeralKey));

            return true;
        }
    }
}
