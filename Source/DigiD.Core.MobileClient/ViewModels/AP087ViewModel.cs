using System.Collections.Generic;
using System.Windows.Input;
using DigiD.Common;
using DigiD.Common.Constants;
using DigiD.Common.Enums;
using DigiD.Common.Interfaces;
using DigiD.Common.Models;
using DigiD.Common.SessionModels;
using DigiD.Common.Settings;
using DigiD.Helpers;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DigiD.ViewModels
{
    public class AP087ViewModel : BaseNotificationsSettingsViewModel
    {
        public List<PreferenceBlock> Blocks { get; set; }
        public bool HideOptions { get; set; }

        public string HideOptionsText => Blocks.Count == 1
            ? MobileAppResources.AP087_HideOption_Text
            : MobileAppResources.AP087_HideOptions_Text;

        public new string FooterText => Blocks.Count == 1
            ? MobileAppResources.AP087_Message_Single
            : MobileAppResources.AP087_Message_Multiple;

        public Command HideOptionsCommand { get; }

        public AP087ViewModel() : base(false)
        {
            HeaderText = MobileAppResources.AP087_Header;

            HasBackButton = true;
            HideOptionsCommand = new Command(() =>
            {
                HideOptions = !HideOptions;
                DependencyService.Get<IPreferences>().ShowPreferenceOptions = !HideOptions;
            });
        }

        public override async void OnAppearing()
        {
            DialogService.ShowProgressDialog();
            var status = await DependencyService.Get<IAccountInformationServices>().GetAccountStatus();
            AppSession.SetAccountStatus(status);

            var blocks = new List<PreferenceBlock>();

            if (AppSession.AccountStatus.OpenTasks.Contains(AccountTask.RDA))
            {
                blocks.Add(new PreferenceBlock
                {
                    Header = MobileAppResources.AP087_Item_IDCheck_Header,
                    Message = MobileAppResources.AP087_Item_IDCheck_Message,
                    ButtonText = MobileAppResources.AP087_Item_IDCheck_ButtonText,
                    Command = new AsyncCommand(async () =>
                    {
                        if (!App.HasNfc)
                        {
                            await NavigationService.PushAsync(new IdCheckNoNfcViewModel());
                        }
                        else
                        {
                            await SessionHelper.StartSession(async () =>
                            {
                                var model = new ConfirmModel(ConfirmActions.IDcheckStartFromMenu);
                                await NavigationService.PushAsync(new ConfirmViewModel(model, false));
                            });
                        }
                    })
                });
            }

            if (AppSession.AccountStatus.OpenTasks.Contains(AccountTask.Email))
            {
                blocks.Add(new PreferenceBlock
                {
                    Header = MobileAppResources.AP087_Item_Email_Header,
                    Message = MobileAppResources.AP087_Item_Email_Message,
                    ButtonText = MobileAppResources.AP087_Item_Email_ButtonText,
                    Command = new AsyncCommand(async () =>
                    {
                        await SessionHelper.StartSession(() => EmailHelper.Manage(true));
                    })
                });
            }

            if (AppSession.AccountStatus.OpenTasks.Contains(AccountTask.Notification))
            {
                blocks.Add(new PreferenceBlock
                {
                    Header = MobileAppResources.AP087_Item_Messages_Header,
                    Message = MobileAppResources.AP087_Item_Messages_Message,
                    ButtonText = MobileAppResources.AP087_Item_Messages_ButtonText,
                    Command = OpenSettingsCommand
                });
            }

            if (AppSession.AccountStatus.OpenTasks.Contains(AccountTask.TwoFactor))
            {
                blocks.Add(new PreferenceBlock
                {
                    Header = MobileAppResources.AP087_Item_2fa_Header,
                    Message = MobileAppResources.AP087_Item_2fa_Message,
                    ButtonText = MobileAppResources.AP087_Item_2fa_ButtonText,
                    Command = new AsyncCommand(async () =>
                    {
                        await NavigationService.PushAsync(new AP120ViewModel(false));
                    })
                });
            }

            Blocks = blocks;

            if (Blocks.Count == 0)
                await NavigationService.GoBack();

            base.OnAppearing();

            DialogService.HideProgressDialog();
        }
    }

    public class PreferenceBlock : BindableObject
    {
        public string Header { get; set; }
        public string Message { get; set; }
        public string ButtonText { get; set; }
        public ICommand Command { get; set; }
    }
}
