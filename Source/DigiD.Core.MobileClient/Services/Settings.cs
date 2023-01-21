using System;
using DigiD.Common.BaseClasses;
using DigiD.Common.Enums;
using DigiD.Common.Settings;
using DigiD.Helpers;
using DigiD.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(Settings))]
namespace DigiD.Services
{
    public class Settings : BaseAppSettings, IMobileSettings
    {
        public bool? NotificationPermissionSet
        {
            get => GetValue((bool?)null);
            set => SetValue(value);
        }

        public string NotificationToken
        {
            get => GetValue(string.Empty);
            set => SetValue(value);
        }

        public string NewNotificationToken
        {
            get => GetValue(string.Empty);
            set => SetValue(value);
        }

        public string SymmetricKey
        {
            get => GetValue(string.Empty);
            set => SetValue(value);
        }

        public string AppId
        {
            get => GetValue(string.Empty);
            set => SetValue(value);
        }

        public string MaskCode
        {
            get => GetValue(string.Empty);
            set => SetValue(value);
        }

        public ActivationMethod ActivationMethod
        {
            get => Enum.Parse<ActivationMethod>(Store.FindValueForKey(nameof(ActivationMethod), ActivationMethod.Unsupported.ToString()).Replace("\"", string.Empty));
            set => Store.Insert(value.ToString(), nameof(ActivationMethod));
        }

        public ActivationStatus ActivationStatus
        {
            get => Enum.Parse<ActivationStatus>(Store.FindValueForKey(nameof(ActivationStatus), ActivationStatus.NotActivated.ToString()).Replace("\"", string.Empty));
            set => Store.Insert(value.ToString(), nameof(ActivationStatus));
        }

        public LoginLevel LoginLevel
        {
            get => Enum.Parse<LoginLevel>( Store.FindValueForKey(nameof(LoginLevel), LoginLevel.Unknown.ToString()).Replace("\"", string.Empty));
            set => Store.Insert(value.ToString(), nameof(LoginLevel));
        }

        public override void Reset()
        {
            base.Reset();

            SigningHelper.GetService().RemoveKeyPair();
            App.RegisterServices(AppMode.Normal);
        }
    }
}

