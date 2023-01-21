using DigiD.Common;
using DigiD.Common.Constants;
using DigiD.Common.Enums;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Models;
using DigiD.Common.SessionModels;
using DigiD.Helpers;
using Xamarin.CommunityToolkit.ObjectModel;

namespace DigiD.ViewModels
{
    public class ActivationRdaCompletedViewModel : BaseViewModel
    {
        private readonly RegisterEmailModel _model;
        public string Header2Text { get; }
        public bool ShowPossibleIds => _model.ActivationMethod == ActivationMethod.RDA;

#if A11YTEST
        public ActivationRdaCompletedViewModel() : this(new RegisterEmailModel(Common.Enums.ActivationMethod.RDA, true)) { }
#endif

        public ActivationRdaCompletedViewModel(RegisterEmailModel model)
        {
            _model = model;
            PageId = $"AP007 - {model.ActivationMethod}";
            Header2Text = model.ActivationMethod == ActivationMethod.AccountAndApp ? AppResources.AP086Header3 : AppResources.AP086Header2;

            //Clear activationdata
            HttpSession.ActivationSessionData = null;
        }

        public new AsyncCommand ButtonCommand
        {
            get
            {
                return new AsyncCommand(async () =>
                {
                    CanExecute = false;
                    await RdaHelper.Init(false);
                    CanExecute = true;
                }, () => CanExecute);
            }
        }

        public AsyncCommand CancelCommand
        {
            get
            {
                return new AsyncCommand(async () =>
                {
                    CanExecute = false;

                    await NavigationService.PushAsync(new ConfirmViewModel(new ConfirmModel(ConfirmActions.IDCheckPostponeAppActivation), true));
                    CanExecute = true;
                }, () => CanExecute);
            }
        }

    }
}
