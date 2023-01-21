using System.Threading.Tasks;
using DigiD.Common.Interfaces;
using DigiD.Common.SessionModels;
using DigiD.Helpers;
using DigiD.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiD.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashPage : ContentPage
    {
        private readonly bool _validateSession;
        private bool _init;

        public SplashPage(bool validateSession)
        {
            _validateSession = validateSession;

            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (HttpSession.IsApp2AppSession || HttpSession.IsWeb2AppSession)
                return;

            if (!_init && _validateSession)
            {
                _init = true;
                await SessionHelper.StartSession(async () =>
                {
                    await Task.Delay(0);
                    DependencyService.Get<INavigationService>().SetPage(new LandingViewModel());
                });
            }
        }
    }
}
