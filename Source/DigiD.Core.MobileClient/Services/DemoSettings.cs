using System;
using DigiD.Common.BaseClasses;
using DigiD.Common.Interfaces;
using DigiD.Services;

[assembly: Xamarin.Forms.Dependency(typeof(DemoSettings))]
namespace DigiD.Services
{
    public class DemoSettings : BaseAppSettings, IDemoSettings
    {
        public string DemoPin
        {
            get => GetValue(string.Empty);
            set => SetValue(value);
        }

        public bool IsDemo => !string.IsNullOrEmpty(DemoPin);
        
        public string EmailAddress 
        {
            get => GetValue(string.Empty);
            set => SetValue(value);
        }

        public int UserId 
        {
            get => GetValue(0);
            set => SetValue(value);
        }

        public DateTime? EmailCheckDate
        {
            get => GetValue(default(DateTime?));
            set => SetValue(value);
        }

        public bool? EmailAddressVerified 
        {
            get => GetValue(default(bool));
            set => SetValue(value);
        }

        public bool? TwoFactorEnabled 
        {
            get => GetValue(default(bool?));
            set => SetValue(value);
        }
    }
}
