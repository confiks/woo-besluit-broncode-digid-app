using Xamarin.Forms.Xaml;

namespace DigiD.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmailRegisterPage : BaseActivationPage
    {
        public EmailRegisterPage()
        {
            InitializeComponent();
            buttonPanel.ViewOrder = new[] { primaryButton, secondaryButton };
        }
    }
}
