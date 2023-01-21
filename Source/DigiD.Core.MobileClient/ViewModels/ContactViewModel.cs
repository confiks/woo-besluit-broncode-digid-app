using System.Threading.Tasks;
using System.Windows.Input;
using DigiD.Common;
using DigiD.Common.Helpers;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Models;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;

namespace DigiD.ViewModels
{
    public class ContactViewModel : BaseViewModel
    {
        public const string ContactViaPhone = "phone";
        public const string ContactViaTwitter = "twitter";
        public const string ContactViaContactForm = "contactForm";
        public const string ContactViaDigiDWebsite = "digidwebsite";

        private const string Key = "SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS";

        private readonly ObfuscatedString _contactViaPhoneUri = new ObfuscatedString(Key, "SSSSSSSSSSSSSSSSSSSSSSSS"); //SSSSSSSSSSSSSSSS
        private readonly ObfuscatedString _contactViaTwitterUri = new ObfuscatedString(Key, "SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS"); //twitter://user?screen_name=digidwebcare
        private readonly ObfuscatedString _contactViaTwitterWebUri = new ObfuscatedString(Key, "SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS"); //https://twitter.com/digidwebcare
        private readonly ObfuscatedString _contactViaDigiDWebsiteUri = new ObfuscatedString(Key, "SSSSSSSSSSSSSSSSSSSSSSSSSSSS"); //https://www.digid.nl
        
        public ContactViewModel()
        {
            PageId = "AP093";
            HeaderText = AppResources.ContactHeader;
            HasBackButton = true;
            NavCloseCommand = new AsyncCommand(async () => await NavigationService.PopToRoot());
        }
        public ICommand ZoekContactVia => new AsyncCommand<string>(async contactVia =>
        {
            if (!CanExecute)
                return;

            CanExecute = false;

            switch (contactVia)
            {
                case ContactViaPhone:
                    PiwikHelper.Track("contact", "phone", "AP093");
                    await Launcher.OpenAsync(_contactViaPhoneUri.ToString());
                    break;
                case ContactViaTwitter:
                    PiwikHelper.Track("contact", "twitter", "AP093");
                    await OpenAppOrWebsite(_contactViaTwitterUri.ToString(), _contactViaTwitterWebUri.ToString());
                    break;
                case ContactViaContactForm:
                    PiwikHelper.Track("contact", "contactform", "AP093");
                    await NavigationService.PushModalAsync(new WebViewViewModel(AppResources.ContactFormUrl));
                    break;
                case ContactViaDigiDWebsite:
                    PiwikHelper.Track("contact", "website", "AP093");
                    await Launcher.OpenAsync(_contactViaDigiDWebsiteUri.ToString());
                    break;
            }

            CanExecute = true;
        }, u => CanExecute);


        private static async Task OpenAppOrWebsite(string contactViaAppUri, string contactViaWebUri)
        {
            if (!await Launcher.TryOpenAsync(contactViaAppUri))
                await Launcher.OpenAsync(contactViaWebUri);
        }
    }
}
