using DigiD.Common.BaseClasses;
using DigiD.Common.Models;
using DigiD.Common.NFC.Enums;
using Xamarin.CommunityToolkit.ObjectModel;

namespace DigiD.Common.ViewModels
{
    public class WidSuspendedViewModel : CommonBaseViewModel
    {
        public string AnimationSource { get; set; }

        public WidSuspendedViewModel(PinCodeModel model)
        {
            HeaderText = AppResources.WidSuspended_Header;

            switch (model.Card.DocumentType)
            {
                case DocumentType.DrivingLicense:
                    PageId = "AP404";
                    FooterText = AppResources.AP404_Message;
                    AnimationSource = "animatie_CAN_code_uitleg_rijbewijs.json";
                    break;
                case DocumentType.IDCard:
                    PageId = "AP406";
                    AnimationSource = "animatie_CAN_code_uitleg_IDkaart.json";
                    FooterText = AppResources.AP406_Message;
                    break;
            }
            
            ButtonCommand = new AsyncCommand(async () =>
            {
                await NavigationService.GoToPincodePage(model);
            });
        }
    }
}
