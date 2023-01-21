using System;
using System.Threading.Tasks;
using System.Windows.Input;
using DigiD.Common;
using DigiD.Common.Enums;
using Xamarin.CommunityToolkit.ObjectModel;

namespace DigiD.ViewModels
{
    public class ActivationConfirmViewModel : BaseActivationViewModel
    {
        public string CancelText { get; set; }
        public bool CancelButtonVisible => CancelCommand != null;
        public ICommand CancelCommand { get; set; }

#if A11YTEST
        public ActivationConfirmViewModel() : this(null) { }
#endif
        public ActivationConfirmViewModel(Func<Task> cancelCommand)
        {
            PageId = "AP004";
            ButtonText = AppResources.ActivationConfirmPendingMessageButton;
            CancelText = AppResources.ActivationConfirmPendingCancel;
            HeaderText = AppResources.ActivationConfirmPendingHeader;

            CancelCommand = new AsyncCommand(async () => await cancelCommand.Invoke());

            ButtonCommand = new AsyncCommand(async () =>
            {
                await App.CancelSession(true, async () => await NavigationService.ShowMessagePage(MessagePageType.ActivationCancelled));
            });
        }
    }
}
