using System;
using System.Threading.Tasks;
using DigiD.Common.Helpers;
using DigiD.Common.Mobile.Helpers;
using DigiD.ViewModels;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiD.UI.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenuPopup : Popup
    {
        const uint Velocity = 250;

        public double MenuWidth => DisplayHelper.Width;
        public double MenuHeight => DisplayHelper.Height;

        public MainMenuPopup()
        {
            ResizeMenu(OrientationHelper.IsInLandscapeMode);

            InitializeComponent();
            BindingContext = new MainMenuViewModel();

            Opened += OpenPopup;

            IsLightDismissEnabled = true;

            DeviceDisplay.MainDisplayInfoChanged += DeviceDisplay_MainDisplayInfoChanged;
        }

        private void DeviceDisplay_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            ResizeMenu(e.DisplayInfo.Orientation == DisplayOrientation.Landscape);
        }

        private void ResizeMenu(bool isInLandscapeMode)
        {
            Size = new Size(isInLandscapeMode ? MenuWidth * .45 : MenuWidth * .8, MenuHeight);
        }

        protected override async void LightDismiss()
        {
            await ClosePopup();
            base.LightDismiss();
        }

        public void Close()
        {
            LightDismiss();
        }

        private async Task ClosePopup()
        {
            await popupGrid.TranslateTo(-MenuWidth, 0, Velocity, Easing.Linear);
        }

        async void OpenPopup(object sender, PopupOpenedEventArgs e)
        {
            await popupGrid.TranslateTo(0, 0, Velocity, Easing.Linear);
        }

        private void PopupGrid_OnClose(object sender, EventArgs e)
        {
            LightDismiss();
        }
    }
}
