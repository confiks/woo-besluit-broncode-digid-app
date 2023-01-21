using System;
using System.Threading.Tasks;
using DigiD.Common.EID.BaseClasses;
using DigiD.Common.EID.Constants;
using DigiD.Common.EID.Helpers;
using DigiD.Common.EID.Interfaces;
using DigiD.Common.EID.Models.Network.Requests;
using DigiD.Common.EID.Models.Network.Responses;
using DigiD.Common.EID.SessionModels;
using DigiD.Common.Http.Enums;
using DigiD.Common.Http.Helpers;

namespace DigiD.Common.EID.CardSteps.TA.RDW
{
    /// <summary>
    /// The client sends the signature it got from the server to the PCA to verify it.
    /// See steps 9 and 10, page 25 of the PCA implementation guidelines.
    /// </summary>
    internal class StepExternalAuthentication : BaseSecureStep, IStep
    {
        private readonly TerminalAuthenticationRdw _operation;

        public StepExternalAuthentication(ISecureCardOperation operation) : base(operation)
        {
            _operation = (TerminalAuthenticationRdw)Operation;
        }

        public async Task<bool> Execute()
        {
            var challenge = _operation.Ricc;

            var requestData = new
            {
                challenge = Convert.ToBase64String(challenge)
            };

            var challengeRequest = new EIDBaseRequest(requestData, Operation.GAP.SessionData.SessionId);
            var response = await EIDSession.Client.PostAsync<PcaChallengeResponse>(new Uri(Operation.GAP.SessionData.ServerAddress + WidConstants.RDW_SIGN_CHALLENGE_URI), challengeRequest);

            Operation.GAP.ApiResult = response.ApiResult;

            if (response.ApiResult == ApiResult.Ok)
            {
                var signatureFromServer = ByteHelper.GetSignature(Convert.FromBase64String(response.ChallengeResponse.Signature));
                var command = CommandApduBuilder.GetExternalAuthenticate(signatureFromServer, Operation.GAP.SMContext);
                var responseAPDU = await CommandApduBuilder.SendAPDU("TA ExternalAuthentication", command, Operation.GAP.SMContext);

                return responseAPDU.SW == 0x9000;
            }

            return false;
        }
    }
}
