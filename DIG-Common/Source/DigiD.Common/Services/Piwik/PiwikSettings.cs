using System;
using DigiD.Common.BaseClasses;
using DigiD.Common.Interfaces;
using DigiD.Common.Services.Piwik;

[assembly: Xamarin.Forms.Dependency(typeof(PiwikSettings))]
namespace DigiD.Common.Services.Piwik
{
    /// <summary>
    /// Piwik settings. Do not use this class directly as it is only intended to use internally.
    /// </summary>
    public class PiwikSettings : BaseAppSettings, IPiwikSettings
    {
        public int SessionsCount
        {
            get => GetValue(0);
            set => SetValue(value);
        }

        public DateTime FirstVisit
        {
            get => GetValue(DateTime.Now);
            set => SetValue(value);
        }

        public DateTime LastVisit
        {
            get => GetValue(DateTime.Now);
            set => SetValue(value);
        }
    }
}
