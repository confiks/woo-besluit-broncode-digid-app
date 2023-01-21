using System.Threading.Tasks;
using DigiD.Common.ViewModels;

namespace DigiD.Common.Interfaces
{
    public interface IPopupService
    {
        Task OpenPopup(InfoViewModel viewModel);
    }
}
