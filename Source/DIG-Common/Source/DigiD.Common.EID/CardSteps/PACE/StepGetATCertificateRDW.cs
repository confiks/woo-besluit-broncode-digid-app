using System;
using System.Threading.Tasks;
using DigiD.Common.EID.Cards;
using DigiD.Common.EID.Constants;
using DigiD.Common.EID.Enums;
using DigiD.Common.EID.Interfaces;
using DigiD.Common.EID.Models;
using DigiD.Common.EID.Models.Network.Requests;
using DigiD.Common.EID.Models.Network.Responses;
using DigiD.Common.EID.SessionModels;
using DigiD.Common.Http.Enums;
using DigiD.Common.Http.Helpers;

namespace DigiD.Common.EID.CardSteps.PACE
{
    public class StepGetATCertificateRdw : IStep
    {
        private readonly Gap _gap;

        /// <summary>
        /// Will retrieve the AT certificate based on the UserConsent from the server
        /// </summary>
        /// <param name="gap"></param>
        public StepGetATCertificateRdw(Gap gap)
        {
            _gap = gap;
        }
        public async Task<bool> Execute()
        {
            if (_gap.SessionData.Consent == UserConsent.NotNeeded)
                throw new ArgumentException("UserConsent need to be set", nameof(_gap.UserConsent));

            var requestData = new
            {
                userConsentType = _gap.UserConsent == UserConsent.PP ? "PP" : "PIP",
                documentType = Convert.ToBase64String(_gap.Card.CardAID)
            };

            var response = await EIDSession.Client.PostAsync<AtCertificateResponse>(new Uri(_gap.SessionData.ServerAddress + WidConstants.RDW_GET_CERTIFICATE_URI), new EIDBaseRequest(requestData, _gap.SessionData.SessionId));
            _gap.ApiResult = response.ApiResult;

            if (response.ApiResult == ApiResult.Ok)
            {
                ((DrivingLicense)_gap.Card).ATCertificate = new Certificate(Convert.FromBase64String(response.Data.AtCertificate));
                return true;
            }

            return false;
        }
    }
}
