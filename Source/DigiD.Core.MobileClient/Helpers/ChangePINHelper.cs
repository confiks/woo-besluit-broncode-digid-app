using System.Threading.Tasks;
using DigiD.Common.Enums;
using DigiD.Common.Interfaces;
using DigiD.Common.Models;
using DigiD.ViewModels;
using Xamarin.Forms;

namespace DigiD.Helpers
{
    public static class ChangePinHelper
    {
        internal static async Task StartChangePIN()
        {
            var model = new PinCodeModel(PinEntryType.Authentication)
            {
                ChangeAppPin = true
            };

            await DependencyService.Get<INavigationService>().PushAsync(new PinCodeViewModel(model));
        }
    }
}
