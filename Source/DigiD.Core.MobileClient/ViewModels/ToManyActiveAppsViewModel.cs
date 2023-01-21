using System;
using System.Globalization;
using System.Threading.Tasks;
using DigiD.Common;
using DigiD.Common.Enums;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Models.ResponseModels;
using Xamarin.CommunityToolkit.ObjectModel;

namespace DigiD.ViewModels
{
    public class ToManyActiveAppsViewModel : BaseViewModel
    {
        public string AppInformation { get; set; }

        public IAsyncCommand CancelCommand { get; set; }

#if A11YTEST
        public ToManyActiveAppsViewModel() : this(3) { }
#endif

        public ToManyActiveAppsViewModel(ActivationResponse model, Func<Task> confirm)
        {
            PageId = "AP094";
            FooterText = string.Format(CultureInfo.InvariantCulture,AppResources.AP094_Message, model.MaxAmount);
            AppInformation = string.Format(CultureInfo.InvariantCulture,AppResources.AP094_AppInformation, model.DeviceName, model.LastUsed);

            CancelCommand = new AsyncCommand(async () =>
            {
                await App.CancelSession(true, async () => await NavigationService.ShowMessagePage(MessagePageType.ActivationFailedTooManyDevices));
            });

            ButtonCommand = new AsyncCommand(async () =>
            {
                await NavigationService.PopCurrentModalPage();
                await confirm.Invoke();
            });
        }

        public ToManyActiveAppsViewModel(int maxAmount)
        {
            PageId = "AP200";
            FooterText = string.Format(AppResources.AP200_ActivationConfirmTooManyDevicesMessage, maxAmount);

            ButtonCommand = new AsyncCommand(async () =>
            {
                await App.CancelSession(true, async () => await NavigationService.ShowMessagePage(MessagePageType.ActivationCancelled));
            });
        }
    }
}
