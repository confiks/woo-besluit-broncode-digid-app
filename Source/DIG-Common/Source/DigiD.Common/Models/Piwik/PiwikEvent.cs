using System;
using System.Collections.Generic;
using DigiD.Common.SessionModels;
using DigiD.Common.Settings;
using Xamarin.Forms;

namespace DigiD.Common.Models.Piwik
{
    /// <summary>
    /// Class met alle gegevens voor een Piwik event.
    /// </summary>
    public class PiwikEvent
    {
        public string SiteID { get; set; }
        public Guid Uuid { get; set; }
        public Session Session { get; set; }

        public DateTime Date { get; set; }
        public string Url { get; set; }
        public string[] ActionName { get; set; }
        public string Language { get; set; }
        public bool IsNewSession { get; set; }
        public string EventCategory { get; set; }
        public string EventAction { get; set; }
        public string EventName { get; set; }
        public string EventValue { get; set; }

        public Dictionary<string, string[]> PageCustomVar { get; } = new Dictionary<string, string[]>();

        public PiwikEvent()
        {
            Date = DateTime.Now;
            Uuid = Guid.NewGuid();
            Url = $"https://{DependencyService.Get<IGeneralPreferences>().SelectedHost}/";
            
            if (HttpSession.IsApp2AppSession)
                PageCustomVar.Add("1", new[] {"App2App", "1"});
        }
    }
}
