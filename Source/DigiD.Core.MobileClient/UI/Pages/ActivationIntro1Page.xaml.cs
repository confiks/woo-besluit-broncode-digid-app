using DigiD.Common.Mobile.BaseClasses;
using Xamarin.Forms.Xaml;

namespace DigiD.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivationIntro1Page : BaseContentPage
    {
        public ActivationIntro1Page()
        {
            DefaultElementName = "HeaderText";

            InitializeComponent();
        }
    }
}
