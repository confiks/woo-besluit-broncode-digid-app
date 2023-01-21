using System.Threading.Tasks;
using DigiD.Common;
using DigiD.Common.Interfaces;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Services;
using DigiD.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiD.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AP079 : BaseContentPage
    {
        bool _isValidating;

        public double ScrollScale { get; set; } = 1.0;
        public StackOrientation HouseNrStackOrientation { get; set; } = StackOrientation.Horizontal;

        public AP079()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(100);  // anders leest ie eerst iets ander voor
        }

#pragma warning disable S3776 //Code Smell: Refactor this method to reduce its Cognitive Complexity from 17 to the 15 allowed.
        protected async void NextButton_Clicked(System.Object sender, System.EventArgs e)
#pragma warning restore S3776 //Code Smell: Refactor this method to reduce its Cognitive Complexity from 17 to the 15 allowed.
        {
            if (_isValidating)
                return;

            _isValidating = true;

            if (BindingContext is AP079ViewModel vm)
            {
                if (vm.IsValid && vm.ButtonCommand.CanExecute(null))
                    vm.ButtonCommand.Execute(null);
                else
                {
                    if (DependencyService.Get<IA11YService>().IsInVoiceOverMode())
                    {
                        vm.Validate("All");

                        if (string.IsNullOrEmpty(txt_bsn.Text) || !vm.IsValidBsn)
                            await InformUser(txt_bsn, txt_bsn.ErrorText);
                        else if (string.IsNullOrEmpty(txt_dob.Text) || !vm.IsValidDob)
                            await InformUser(txt_dob, txt_dob.ErrorText);
                        else if (string.IsNullOrEmpty(txt_zipcode.Text) || !vm.IsValidPostalcode)
                            await InformUser(txt_zipcode, txt_zipcode.ErrorText);
                        else if (string.IsNullOrEmpty(txt_housenumber.Text) || !vm.IsValidHouseNumber)
                            await InformUser(txt_housenumber, $"{AppResources.InvalidInput}, {txt_housenumber.LabelText}");
                    }
                }
            }
            
            _isValidating = false;
        }

        private static async Task InformUser(ICustomEntry entry, string errorText, bool longDelay = false)
        {
            if (DependencyService.Get<IA11YService>().IsInVoiceOverMode())
            {
                await Task.Delay(100);
                entry.Focus();
                // vertraging voldoende voor uitspreken bij invalid geb.datum inclusief invoer vd gebruiker
                await DependencyService.Get<IA11YService>().Speak(errorText, longDelay ? 6000 : 3000);
            }
        }

        protected async void txt_Completed(object sender, System.EventArgs e)
        {
            if (sender is ICustomEntry cef && BindingContext is AP079ViewModel vm)
            {
                var name = cef.AutomationId;
                vm.Validate(name);
                if (!cef.IsValid && DependencyService.Get<IA11YService>().IsInVoiceOverMode())
                    await InformUser(cef, cef.ErrorText);
            }
        }
    }
}
