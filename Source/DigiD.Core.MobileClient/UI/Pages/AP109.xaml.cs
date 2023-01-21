using System;
using System.Threading.Tasks;
using DigiD.Common.Interfaces;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.RDA.ViewModels;
using DigiD.Common.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiD.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AP109 : BaseContentPage
    {
        public AP109()
        {
            InitializeComponent();
        }

        public ScrollOrientation ScrollOrientation { get; set; } = ScrollOrientation.Vertical;

        protected async void txt_Completed(object sender, EventArgs e)
        {
            if (sender == null) throw new ArgumentNullException(nameof(sender));
            if (sender is ICustomEntry cef && BindingContext is AP109ViewModel vm)
            {
                var name = cef.AutomationId;
                vm.Validate(name);
                if (!cef.IsValid && DependencyService.Get<IA11YService>().IsInVoiceOverMode())
                    await InformUser(cef, cef.ErrorText);
            }
        }

        private async Task InformUser(ICustomEntry entryfield, string errorText, bool longDelay = false)
        {
            if (DependencyService.Get<IA11YService>().IsInVoiceOverMode())
            {
                await Task.Delay(100);
                entryfield.Focus();
                // vertraging voldoende voor uitspreken bij invalid geb.datum inclusief invoer vd gebruiker
                await DependencyService.Get<IA11YService>().Speak(errorText, longDelay ? 6000 : 3000);
            }
        }
    }
}
