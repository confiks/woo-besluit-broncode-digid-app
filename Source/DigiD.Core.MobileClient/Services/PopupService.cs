using System.Threading.Tasks;
using DigiD.Common.Interfaces;
using DigiD.Common.ViewModels;
using DigiD.UI.Pages;
using Xamarin.Forms;

namespace DigiD.Services
{
    internal class PopupService : IPopupService
    {
        public async Task OpenPopup(InfoViewModel viewModel)
        {
            await Device.InvokeOnMainThreadAsync(async () =>
            {
                var page = new HelpPage(viewModel);
                await Application.Current.MainPage.Navigation.PushModalAsync(page, false);
            });
        }
    }
}
 
