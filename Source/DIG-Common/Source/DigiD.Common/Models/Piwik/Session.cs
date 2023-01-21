using System;
using DigiD.Common.Interfaces;
using Xamarin.Forms;

namespace DigiD.Common.Models.Piwik
{
    /// <summary>
    /// Session properties. Class internally used by Piwik, no need to use this class in your code.
    /// </summary>
    public class Session
    {
        public int SessionsCount { get; set; }
        public DateTime LastVisit { get; set; }
        public DateTime FirstVisit { get; set; }
        public DateTime CurrentVisit { get; set; }

        public Session()
        {
            DependencyService.Get<IPiwikSettings>().SessionsCount += 1;

            FirstVisit = DependencyService.Get<IPiwikSettings>().FirstVisit;
            LastVisit = DependencyService.Get<IPiwikSettings>().LastVisit;
            CurrentVisit = DateTime.Now;

            DependencyService.Get<IPiwikSettings>().Save();
        }
    }
}
