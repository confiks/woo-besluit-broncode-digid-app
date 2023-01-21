using System;
using System.Threading.Tasks;
using DigiD.Common.Helpers;
using DigiD.Common.Interfaces;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace DigiD.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenuPage : BaseContentPage
    {
        public MainMenuPage()
        {
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(false);
            InitializeComponent();
            BindingContext = new MainMenuViewModel();
            
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            grid.TranslationX =- DisplayHelper.Width;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            DeviceDisplay.MainDisplayInfoChanged += DeviceDisplay_MainDisplayInfoChanged;
            await grid.TranslateTo(0, 0, 250, Easing.Linear);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            DeviceDisplay.MainDisplayInfoChanged -= DeviceDisplay_MainDisplayInfoChanged;
        }

        private void DeviceDisplay_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            var percentage = (e.DisplayInfo.Orientation == DisplayOrientation.Portrait ? .8 : .45);
            MenuView.LayoutTo(new Rectangle(0, 0, DisplayHelper.Width * percentage, DisplayHelper.Height));
        }

        public async Task CloseMenu()
        {
            await grid.TranslateTo(-DisplayHelper.Width, 0, 250, Easing.Linear);
            await DependencyService.Get<INavigationService>().PopCurrentModalPage(false) ;
        }

        private async void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            await CloseMenu();
        }

        private async void MenuView_OnClose(object sender, EventArgs e)
        {
            await CloseMenu();
        }
    }
}
