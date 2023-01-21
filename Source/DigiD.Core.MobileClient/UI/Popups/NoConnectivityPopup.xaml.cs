using System;
using DigiD.Common.Helpers;
using DigiD.Common.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiD.UI.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoConnectivityPopup : DigiD.Common.BaseClasses.BasePopup
    {
        public NoConnectivityPopup()
        {
            var isInVoiceOvermode = DependencyService.Get<IA11YService>().IsInVoiceOverMode();

            var height = DisplayHelper.Height * (isInVoiceOvermode ? .8 : .7);
            var width = DisplayHelper.Width * (isInVoiceOvermode ? .9 : .8);

            if (Device.Idiom == TargetIdiom.Tablet)
            {
                width = 340;
            }
            Size = new Size(width, height);

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
    }


}
