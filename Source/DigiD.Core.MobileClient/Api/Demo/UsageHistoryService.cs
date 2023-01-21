using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigiD.Common.Interfaces;
using DigiD.Common.Mobile.Helpers;
using DigiD.Common.Models;
using DigiD.Common.Settings;
using Xamarin.Forms;

namespace DigiD.Api.Demo
{
    public class UsageHistoryService : IUsageHistoryService
    {
        private readonly List<UsageHistoryModel> _recordsNL = new List<UsageHistoryModel>();
        private readonly List<UsageHistoryModel> _recordsEN = new List<UsageHistoryModel>();
        private const int PageSize = 10;

        private readonly List<string> _optionsNL = new List<string>
        {
            "Bekijkt gebruiksgeschiedenis",
            "Pincode succesvol ingevoerd in DigiD app met ID-check voor webdienst Mijn DigiD (telefoon)",
            "Inloggen gelukt voor activeren app met een andere app",
            "SSO authenticatie domein MijnOverheid gelukt",
            "Inloggen met DigiD app met ID-check gelukt bij webdienst MijnOverheid",
            "Inloggen met identiteitskaart succesvol voor webdienst Mijn DigiD",
            "DigiD app geactiveerd"
        };

        private readonly List<string> _optionsEN = new List<string>
        {
            "Views usage history",
            "Pin code successfully entered in DigiD app with ID check for web service My DigiD (telephone)",
            "Sign in successful for activating app with another app",
            "SSO authentication domain MijnOverheid succeeded",
            "Log in with DigiD app with ID check successful at web service MijnOverheid",
            "Successful login with identity card for web service My DigiD",
            "DigiD app activated"
        };

        public UsageHistoryService()
        {
            var rnd = new Random();
            
            for (var x = 1; x <= 50; x++)
            {
                _recordsNL.Add(new UsageHistoryModel {DateTime = DateTime.Now.AddDays(-x), Message = _optionsNL[rnd.Next(0, 6)]});
                _recordsEN.Add(new UsageHistoryModel {DateTime = DateTime.Now.AddDays(-x), Message = _optionsEN[rnd.Next(0, 6)]});
            }
        }

        public async Task<List<UsageHistoryModel>> GetUsageHistory(int pageIndex = 0)
        {
            await Task.Delay(100);

            var model = new UsageHistoryRequestModel
            {
                Language = DependencyService.Get<IGeneralPreferences>().Language.ToUpper(),
                PageId = pageIndex
            };

            return DemoHelper.Log("/apps/logs/get_logs", model, (model.Language == "NL" ? _recordsNL : _recordsEN).Skip(model.PageId * PageSize).Take(PageSize).ToList());
        }
    }
}
