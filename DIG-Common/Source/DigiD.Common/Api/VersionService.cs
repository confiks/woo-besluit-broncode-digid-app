using System.Threading.Tasks;
using DigiD.Common.Helpers;
using DigiD.Common.Interfaces;
using DigiD.Common.Models.ResponseModels;

namespace DigiD.Common.Api
{
    public class VersionService : IVersionService
    {
        /// <summary>
        /// Laat zien of de app versie die de app mee stuurt momenteel actief is, daarnaast bestaat er de mogelijkheid om per app versie een api version te kiezen.
        /// </summary>
        /// <returns>The version.</returns>
        public async Task<VersionCheckResponse> CheckVersion()
        {
            return await HttpHelper.GetAsync<VersionCheckResponse>("/apps/version", false, 5000);
        }
    }
}
