using DigiD.Common.BaseClasses;
using DigiD.Common.Models;
using System;
using Xamarin.Forms;

namespace DigiD.Common.ViewModels
{
    public class WidChangeTransportPinConfirmViewModel : CommonBaseViewModel
    {
#if A11YTEST
        public WidChangeTransportPinConfirmViewModel() : this(new NfcScannerModel { DocumentType = Enums.DocumentType.DrivingLicense }) { }
#endif

        public WidChangeTransportPinConfirmViewModel(NfcScannerModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            PageId = "AP411";

            HeaderText = AppResources.WIDActivateConfirmHeader;
            FooterText = AppResources.WIDChangeTransportPINSuccessMessage;

            ButtonCommand = new Command(() =>
            {
                NavigationService.GoToNFCScannerPage(model);
            });
        }
    }
}
