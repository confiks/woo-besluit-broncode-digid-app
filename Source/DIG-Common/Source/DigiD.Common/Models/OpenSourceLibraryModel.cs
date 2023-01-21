using System;

namespace DigiD.Common.Models
{
    public class OpenSourceLibraryModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Uri ProjectUrl { get; set; }
        public Uri LicenseUrl { get; set; }

        public string ProjectUrlDescription => string.Format(AppResources.OpenSourceProjectLink, ProjectUrl, Title);
        public string LicenseUrlDescription => string.Format(AppResources.OpenSourceLicenseLink, LicenseUrl, Title);
    }
}
