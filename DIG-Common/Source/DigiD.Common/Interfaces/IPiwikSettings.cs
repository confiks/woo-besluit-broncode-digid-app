using System;

namespace DigiD.Common.Interfaces
{
    /// <summary>
    /// Voor intern gebruik.
    /// </summary>
    public interface IPiwikSettings
    {
        int SessionsCount { get; set; }
        DateTime FirstVisit { get; set; }
        DateTime LastVisit { get; set; }

        void Save();
    }
}
