using System.Threading.Tasks;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Services;
using DigiD.Helpers;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DigiD.ViewModels
{
    internal class CameraPermissionViewModel : BaseViewModel
    {
        private bool _initialized;
        private readonly bool _eIDAuthentication;

#if A11YTEST
        public CameraPermissionViewModel() : this(false) { }
#endif

        public CameraPermissionViewModel(bool eIDAuthentication)
        {
            PageId = "AP003";
            _eIDAuthentication = eIDAuthentication;
            ButtonCommand = new AsyncCommand(async () => await RequestPermission());
            
            NavCloseCommand = new AsyncCommand(async () =>
            {
                await NavigationService.PopToRoot();
            });
        }

        public override async void OnAppearing()
        {
            base.OnAppearing();

            if (!_initialized)
                return;

            _initialized = true;

            await RequestPermission();
        }

        private async Task RequestPermission()
        {
            var requestResult = await Permissions.RequestAsync<Permissions.Camera>();

            if (requestResult == PermissionStatus.Granted)
            {
                await QRCodeScannerHelper.ShowScannerPage(false, _eIDAuthentication);
            }
            else
                DependencyService.Get<IDevice>().OpenSettings();
        }
    }
}
