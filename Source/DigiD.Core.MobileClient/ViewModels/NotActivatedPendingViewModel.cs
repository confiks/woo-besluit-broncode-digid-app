using System.Windows.Input;
using DigiD.Common;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Models;
using DigiD.Helpers;
using Xamarin.CommunityToolkit.ObjectModel;

namespace DigiD.ViewModels
{
    public class NotActivatedPendingViewModel : BaseViewModel
    {
        private readonly App2AppSessionData _data;
        public bool IsApp2App { get; set; }

#if A11YTEST
        public NotActivatedPendingViewModel() : this(null) { }
#endif
        public NotActivatedPendingViewModel(App2AppSessionData data)
        {
            IsApp2App = data != null;
            _data = data;
            
            HeaderText = IsApp2App ? AppResources.NotActivatedHeader : AppResources.NotActivatedPendingHeader;
            PageId = IsApp2App ? "AP021" : "AP022";

            ButtonCommand = new AsyncCommand(async() =>
            {
                CanExecute = false;
                App.ClearSession();

                DialogService.ShowProgressDialog();

                await ActivationHelper.ContinueLetterActivation();
                    
                DialogService.HideProgressDialog();

                CanExecute = true;
            }, () => CanExecute);
        }

        public ICommand CancelCommand
        {
            get
            {
                return new AsyncCommand(async () =>
                {
                    CanExecute = false;
                    DialogService.ShowProgressDialog();
                    await IncomingDataHelper.ProcessData(_data, false, false);
                    await NavigationService.PopToRoot(true);
                    DialogService.HideProgressDialog();
                    CanExecute = true;
                }, () => CanExecute);
            }
        }
    }
}
