using System.Threading.Tasks;
using DigiD.Common.BaseClasses;
using DigiD.Common.Enums;
using DigiD.Common.Models;
using Xamarin.Forms;

namespace DigiD.Common.Interfaces
{
    public interface INavigationService
    {
        Page GetPage(CommonBaseViewModel viewModel);
        void SetPage(CommonBaseViewModel viewModel);
        Task PushAsync(CommonBaseViewModel viewModel, bool animate = true);
        Task<bool> ConfirmAsync(string confirmAction, RegisterEmailModel model = null);
        Task PushModalAsync(CommonBaseViewModel viewModel, bool animate = true, bool nav = true);
        Task GoBack(bool animate = true);
        Task PopCurrentModalPage(bool animate = true);
        Task ShowMessagePage(MessagePageType pageType, object data = null);
        Task GoToPincodePage(PinCodeModel model, bool animate = true);
        Task GoToNFCScannerPage(NfcScannerModel model);
        Task PopToRoot(bool force = false);
        Task ResetMainPage(params CommonBaseViewModel[] viewModels);
        string CurrentPageId { get; }
        Task<T> OpenPopup<T>(BasePopup<T> popup);
        void OpenPopup(BasePopup popup);
#if A11YTEST
        System.Collections.Generic.Dictionary<System.Type, System.Type> RegisteredPages { get; }
#endif
    }
}
