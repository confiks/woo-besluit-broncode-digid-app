using System.Collections.Generic;
using DigiD.Common.Settings;
using Xamarin.Forms;

namespace DigiD.Models
{
    public class WhatsNewDataModel
    {
        public string Version { get; set; } = "0.0.0";
        public Dictionary<string, IList<WhatsNewPageModel>> PageData { get; set; } = new Dictionary<string, IList<WhatsNewPageModel>>();

        public IList<WhatsNewPageModel> GetPages()
        {
            var language = DependencyService.Get<IGeneralPreferences>().Language;

            if (PageData.ContainsKey(language))
                return PageData[language];
            
            return PageData.ContainsKey("nl") ? PageData["nl"] : new List<WhatsNewPageModel>();
        }
    }
}
