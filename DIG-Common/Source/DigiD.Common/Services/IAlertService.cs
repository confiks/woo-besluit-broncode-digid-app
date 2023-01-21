using System.Threading.Tasks;

namespace DigiD.Common.Services
{
    public interface IAlertService
    {
        Task<bool> DisplayAlert(string title, string message, string accept = null, string cancel = null, bool invertCallToAction = false);
    }
}
