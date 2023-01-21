using DigiD.Common;
using DigiD.Common.Enums;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Models;
using DigiD.Helpers;
using Xamarin.CommunityToolkit.ObjectModel;

namespace DigiD.ViewModels
{
    public class ActivationCompletedViewModel : BaseViewModel
    {
        public string HeaderSubText { get; }

#if A11YTEST
        public ActivationCompletedViewModel() : this (new RegisterEmailModel (ActivationMethod.SMS, true)) {}
#endif 

        public ActivationCompletedViewModel(RegisterEmailModel model) 
        {
            PageId = $"AP007 - {model.ActivationMethod}";
            HeaderText = AppResources.ActivationHeader;
            HeaderSubText = model.ActivationMethod == ActivationMethod.AccountAndApp ? AppResources.ActivationCompletedReadyByApp : AppResources.ActivationCompletedReady;

            ButtonCommand = new AsyncCommand(async () =>
            {
                await ActivationHelper.CompleteActivation();
            });

        }
    }
}
