using DigiD.iOS.CustomRenderers;
using DigiD.iOS.Views;
using DigiD.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(QRScannerView), typeof(QRScannerViewRenderer))]
namespace DigiD.iOS.CustomRenderers
{
    public class QRScannerViewRenderer : ViewRenderer<QRScannerView, QRCodeDetectionView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<QRScannerView> e)
        {
            base.OnElementChanged(e);
            if (Control == null && e.OldElement == null)
            {
                var control = new QRCodeDetectionView
                {
                    ObjectCaptured = ObjectCapturedHandler,
                };
                SetNativeControl(control);
            }
        }

        private void ObjectCapturedHandler(string qrData)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (Element.ScanCompleteCommand?.CanExecute(qrData) == true)
                    await Element.ScanCompleteCommand.ExecuteAsync(qrData);
            });
        }
    }
}
