using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DigiD.UI.Views
{
    public class QRScannerView : ContentView
    {
        public static readonly BindableProperty ScanCompleteCommandProperty = BindableProperty.Create(nameof(ScanCompleteCommand), typeof(IAsyncCommand<string>), typeof(QRScannerView), defaultBindingMode: BindingMode.OneWay);
        public IAsyncCommand<string> ScanCompleteCommand
        {
            get => (IAsyncCommand<string>)GetValue(ScanCompleteCommandProperty);
            set => SetValue(ScanCompleteCommandProperty, value);
        }
    }
}
