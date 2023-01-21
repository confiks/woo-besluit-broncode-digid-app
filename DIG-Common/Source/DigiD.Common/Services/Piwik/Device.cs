namespace DigiD.Common.Services.Piwik
{
    /// <summary>
    /// Device information used by PIWIK.
    /// </summary>
    public class Device
    {
        public Device()
        {
            OsVersion = Xamarin.Essentials.DeviceInfo.VersionString;
            Platform = Xamarin.Essentials.DeviceInfo.Manufacturer + " " + Xamarin.Essentials.DeviceInfo.Model;
            HumanReadablePlatformName = Xamarin.Essentials.DeviceInfo.Model;
        }
        /// <summary>
        /// Platform (device hardware model)
        /// </summary>
        /// <value>The platform.</value>
        public string Platform { get; set; }

        /// <summary>
        /// Leesbare naam van het toestel.
        /// </summary>
        /// <value>The name of the human readable platform.</value>
        public string HumanReadablePlatformName { get; set; }

        /// <summary>
        /// Versie van het systeem.
        /// </summary>
        /// <value>The os version.</value>
        public string OsVersion { get; set; }
        /// <summary>
        /// Screen size as string: hxw
        /// </summary>
        /// <value>The size of the screen.</value>
        public string ScreenSize { get; set; }

        /// <summary>
        /// Screen size in native units
        /// </summary>
        /// <value>The size of the native screen.</value>
        public string NativeScreenSize { get; set; }

        /// <summary>
        /// The user agent to report to PIWIK.
        /// </summary>
        /// <value>The user agent.</value>
        public string UserAgent { get; set; }
    }
}
