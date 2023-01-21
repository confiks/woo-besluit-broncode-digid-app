using System;
using System.Threading.Tasks;
using DigiD.Common.EID.BaseClasses;
using DigiD.Common.EID.Cards;
using DigiD.Common.EID.Constants;
using DigiD.Common.EID.Helpers;
using DigiD.Common.EID.Interfaces;
using DigiD.Common.EID.Models.Network.Requests;
using DigiD.Common.EID.Models.Network.Responses;
using DigiD.Common.EID.SessionModels;
using DigiD.Common.Http.Enums;
using DigiD.Common.Http.Helpers;
using DigiD.Common.NFC.Models;

namespace DigiD.Common.EID.CardSteps.Randomize
{
    internal class StepRandomizeNik : BaseSecureStep, IStep
    {
        public StepRandomizeNik(ISecureCardOperation operation) : base(operation)
        {
        }

        public async Task<bool> Execute()
        {
            var card = (eNIK)Operation.GAP.Card;

            var index = card.TACommands.Count - 1;
            ResponseApdu latestResponse = null;

            foreach (var commandString in card.PCACommands)
            {
                index++;

                var command = new CommandApdu(Convert.FromBase64String(commandString));
                latestResponse = await CommandApduBuilder.SendAPDU($"APDUCommand {index}", command, null);

                if (latestResponse.SW != 0x9000)
                    break;
            }

            var requestData = new
            {
                counter = index,
                apdu = latestResponse != null ? Convert.ToBase64String(latestResponse.Bytes) : null
            };

            var response = await EIDSession.Client.PostAsync<EIDBaseResponse>(new Uri($"{Operation.GAP.SessionData.ServerAddress}{WidConstants.NIK_POLYMORPHIC_DATA_URI}"), new EIDBaseRequest(requestData, Operation.GAP.SessionData.SessionId));
            Operation.GAP.ApiResult = response.ApiResult;

            Operation.GAP.SMContext = null;
            return response.ApiResult == ApiResult.Ok;
        }
    }
}
