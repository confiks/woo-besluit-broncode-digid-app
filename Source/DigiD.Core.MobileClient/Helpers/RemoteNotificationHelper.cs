using System.Threading.Tasks;
using DigiD.Common;
using DigiD.Common.Constants;
using DigiD.Common.Helpers;
using DigiD.Common.Http.Enums;
using DigiD.Common.Interfaces;
using DigiD.Common.Services;
using DigiD.Common.Settings;
using DigiD.Interfaces;
using DigiD.ViewModels;
using Xamarin.Forms;

namespace DigiD.Helpers
{
    public static class RemoteNotificationHelper
    {
        public static void SetToken(string token)
        {
            DependencyService.Get<IMobileSettings>().NotificationToken = token;
            DependencyService.Get<IMobileSettings>().Save();
        }

        public static async Task UpdateToken(string token)
        {
            var result = await DependencyService.Get<IRemoteNotificationServices>().UpdateNotificationToken(token);

            if (result.ApiResult == ApiResult.Ok)
            {
                DependencyService.Get<IMobileSettings>().NotificationToken = token;
                App.NewNotificationToken = null;
                DependencyService.Get<IMobileSettings>().Save();
            }
        }

        public static async Task ConfirmShowNotifications(bool askBeforeShow = false)
        {
            await Device.InvokeOnMainThreadAsync(async () =>
            {
                var showMessages = true;

                if (askBeforeShow)
                    showMessages = await DependencyService.Get<IAlertService>().DisplayAlert(
                    AppResources.NewNotificationsHeader, AppResources.NewNotificationsMessage, AppResources.OK, AppResources.Cancel);

                if (showMessages)
                {
                    DependencyService.Get<IDialog>().ShowProgressDialog();
                    await SessionHelper.StartSession(Manage);
                    DependencyService.Get<IDialog>().HideProgressDialog();
                }
            });
        }

        public static async Task Manage()
        {
            var permissionSet = DependencyService.Get<IMobileSettings>().NotificationPermissionSet;

            var canGoBack = true;

            if (permissionSet == null || !permissionSet.Value)
            {
                canGoBack = false;
                DependencyService.Get<IDialog>().HideProgressDialog();
                await DependencyService.Get<INavigationService>().ConfirmAsync(ConfirmActions.RequestPushNotificationPermission);
            }

            DependencyService.Get<IDialog>().ShowProgressDialog();

            var messages = await DependencyService.Get<IMessageCenterServices>().GetMessages();

            if (messages.ApiResult == ApiResult.Ok)
                await DependencyService.Get<INavigationService>().PushAsync(new MessageCenterViewModel(messages, canGoBack));

            DependencyService.Get<IDialog>().HideProgressDialog();
        }

        public static async Task RequestToken(bool permissionSet)
        {
            PiwikHelper.Track("toestemming_pushnotificatie", permissionSet ? "ja" : "nee", DependencyService.Get<INavigationService>().CurrentPageId);

            DependencyService.Get<IDialog>().ShowProgressDialog();
            string token = null;

            if (permissionSet)
            {
                token = DependencyService.Get<IMobileSettings>().NotificationToken;

                if (string.IsNullOrEmpty(token)) //iOS permission is not granted
                    token = await DependencyService.Get<IPushNotificationService>().GetToken();
            }

            var result = await DependencyService.Get<IRemoteNotificationServices>().RegisterNotificationToken(token, permissionSet);
            DependencyService.Get<IMobileSettings>().NotificationPermissionSet = result.ApiResult == ApiResult.Ok;

            DependencyService.Get<IDialog>().HideProgressDialog();
        }
    }
}
