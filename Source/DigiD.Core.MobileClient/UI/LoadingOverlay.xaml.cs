using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiD.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingOverlay : ContentView
    {
        public LoadingOverlay()
        {
            InitializeComponent();
            AutomationProperties.SetIsInAccessibleTree(this, false);
        }
    }
}
