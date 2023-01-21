using System.Threading.Tasks;
using Android.Content;
using DigiD.Droid.CustomRenderers;
using DigiD.Droid.Views;
using DigiD.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(QRScannerView), typeof(QRScannerViewRenderer))]
namespace DigiD.Droid.CustomRenderers
{
    internal class QRScannerViewRenderer : ViewRenderer<QRScannerView, QRCodeDetectionView>
    {
        public QRScannerViewRenderer(Context context) : base(context)
        {
            
        }

        protected override void OnElementChanged(ElementChangedEventArgs<QRScannerView> e)
        {
            base.OnElementChanged(e);

            if (Control == null && e.OldElement == null)
            {
                var control = new QRCodeDetectionView(Context)
                {
                    ObjectCaptured = QRCodeCapturedHandler,
                };
                SetNativeControl(control);
            }
        }

        private async Task QRCodeCapturedHandler(string qrData)
        {
            await Device.InvokeOnMainThreadAsync(async () =>
            {
                if (Element?.ScanCompleteCommand?.CanExecute(qrData) == true)
                    await Element?.ScanCompleteCommand.ExecuteAsync(qrData);
            });
        }
    }
}
