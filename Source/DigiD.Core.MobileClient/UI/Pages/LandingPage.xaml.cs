using System.Threading.Tasks;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Mobile.Controls;
using DigiD.UI.Popups;
using DigiD.ViewModels;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NavigationPage = Xamarin.Forms.NavigationPage;
using Application = Xamarin.Forms.Application;

namespace DigiD.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LandingPage : BaseContentPage
    {
        public MainMenuPopup MenuPopup { get; private set; }
        public MainMenuPage MenuPage { get; private set; }

        public LandingPage()
        {
            InitializeComponent();

            if (NavigationPage.GetTitleView(this) is HeaderView headerView)
            {
                headerView.MenuCommand = new AsyncCommand(async () =>
                {
                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        MenuPage ??= new MainMenuPage();
                        await Application.Current.MainPage.Navigation.PushModalAsync(MenuPage, false);
                    }
                    else
                    {
                        MenuPopup ??= new MainMenuPopup();
                        await Application.Current.MainPage.Navigation.ShowPopupAsync(MenuPopup);
                    }
                });
            }
        }

        public async Task CloseMenu()
        {
            if (Device.RuntimePlatform == Device.Android)
                MenuPopup?.Close();
            else if (Navigation.ModalStack.Count > 0 && MenuPage != null)
                await MenuPage.CloseMenu();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
#if !PROD
            if (letterImage.GestureRecognizers.Count == 0)
            {
                letterImage.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = ((LandingViewModel)BindingContext).ChangeLetterRequestDate,
                    NumberOfTapsRequired = 4
                });
            }
#endif

            if (NavigationPage.GetTitleView(this) is HeaderView headerView)
            {
                headerView.LogoutCommand = ((LandingViewModel)BindingContext).LogoutCommand;
            }
        }
    }
}
