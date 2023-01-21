using System.Threading.Tasks;
using DigiD.Common.Controls;
using DigiD.Common.Services;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

[assembly: Dependency(typeof(AlertService))]
namespace DigiD.Common.Services
{
    public class AlertService : IAlertService
    {
        public async Task<bool> DisplayAlert(string title, string message, string accept = null, string cancel = null, bool invertCallToAction = false)
        {
            try
            {
                if (cancel == null)
                {
                    await Application.Current.MainPage.Navigation.ShowPopupAsync(new AlertView(title, message, accept ?? AppResources.OK, cancel, invertCallToAction));
                    return await Task.FromResult(true);
                }

                var result = await Application.Current.MainPage.Navigation.ShowPopupAsync(new AlertView(title, message, accept ?? AppResources.OK, cancel, invertCallToAction));
                if (result is bool resultAsBool)
                    return await Task.FromResult(resultAsBool);

                return await Task.FromResult(false);
            }
            catch
            {
                return false;
            }
        }
    }
}
