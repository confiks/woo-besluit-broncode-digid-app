using System;
using DigiD.Common.Constants;
using DigiD.Common.Interfaces;
using DigiD.Common.Mobile.BaseClasses;
using Xamarin.Forms;

namespace DigiD.UI.Pages
{
    public partial class DebugSettingsPage : BaseContentPage
    {
        public DebugSettingsPage()
        {
            InitializeComponent();
            lblDisplayTimeoutValue.Text = $"Display Lock {AppConfigConstants.DisplayLockTimeout.TotalSeconds}s";
            lblSessionTimeoutValue.Text = $"Session Timeout {AppConfigConstants.SessionTimeout.TotalSeconds}s";
            sliderDisplay.Value = AppConfigConstants.DisplayLockTimeout.TotalSeconds;
            sliderSession.Value = AppConfigConstants.SessionTimeout.TotalSeconds;
        }

        private void Switch_OnToggled(object sender, ToggledEventArgs e)
        {
            DebugConstants.IsPresentingMode = ((Switch)sender).IsToggled;
            DependencyService.Get<INavigationService>().PopToRoot(true);
        }

        private void SwitchClassifiedDeceased_OnToggled(object sender, ToggledEventArgs e)
        {
            DebugConstants.IsClassifiedDeceased = ((Switch)sender).IsToggled;
        }

        private void SliderDisplay_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            AppConfigConstants.DisplayLockTimeout = TimeSpan.FromSeconds((int)e.NewValue);
            lblDisplayTimeoutValue.Text = $"Display Lock {AppConfigConstants.DisplayLockTimeout.TotalSeconds}s";
        }

        private void SliderSession_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            AppConfigConstants.SessionTimeout = TimeSpan.FromSeconds((int)e.NewValue);
            lblSessionTimeoutValue.Text = $"Session Timeout {AppConfigConstants.SessionTimeout.TotalSeconds}s";
        }
    }
}
