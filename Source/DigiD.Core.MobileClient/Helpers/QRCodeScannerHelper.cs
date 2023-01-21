using System.Threading.Tasks;
using DigiD.Common.Helpers;
using DigiD.Common.Interfaces;
using DigiD.UI.Pages;
using DigiD.ViewModels;
using Xamarin.Forms;

namespace DigiD.Helpers
{
    internal static class QRCodeScannerHelper
    {
        internal static async Task ShowScannerPage(bool canGoBack = true, bool eIDAuthentication = false)
        {
            if (await PermissionHelper.CheckCameraPermissions())
            {
                if (Xamarin.Forms.Application.Current.MainPage != null && Xamarin.Forms.Application.Current.MainPage.GetType() == typeof(QRScannerPage)) 
                    await Xamarin.Forms.Application.Current.MainPage.Navigation.PopToRootAsync();
                else
                {
                    await  DependencyService.Get<INavigationService>().PushAsync(new QRScannerViewModel(eIDAuthentication, canGoBack));
                }
            }
            else
                await  DependencyService.Get<INavigationService>().PushAsync(new CameraPermissionViewModel(eIDAuthentication));
        }
    }
}
