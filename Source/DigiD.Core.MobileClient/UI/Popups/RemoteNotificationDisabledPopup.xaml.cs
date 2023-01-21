using System;
using DigiD.Common.Helpers;
using DigiD.Common.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiD.UI.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RemoteNotificationDisabledPopup : DigiD.Common.BaseClasses.BasePopup
    {
        public RemoteNotificationDisabledPopup()
        {
            var isInVoiceOvermode = DependencyService.Get<IA11YService>().IsInVoiceOverMode();

            var height = DisplayHelper.Height * (isInVoiceOvermode ? .9 : .8);
            var width = DisplayHelper.Width * (isInVoiceOvermode ? .9 : .8);

            if (Device.Idiom == TargetIdiom.Tablet)
            {
                width = 340;
            }
            Size = new Size(width,height);

            InitializeComponent();
        }

        private void Close_Clicked(object sender, EventArgs e)
        {
            Dismiss(null);
            IsOpened = false;
        }

        protected override void LightDismiss()
        {
            base.LightDismiss();
            IsOpened = false;
        }


        private void OpenSettingsButton_OnClicked(object sender, EventArgs e)
        {
            DependencyService.Get<IDevice>().OpenSettings();
        }
    }
}
