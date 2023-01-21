using System.Threading.Tasks;
using DigiD.Common.Interfaces;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Services;
using DigiD.Helpers;
using DigiD.Models;
using DigiD.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiD.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WhatsNewTourPage : BaseContentPage
    {
        private WhatsNewTourViewModel ViewModel => (WhatsNewTourViewModel)BindingContext;

        public WhatsNewTourPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            WhatsNewPagesCarousel.ScrollTo(ViewModel.Index);

            if (Device.RuntimePlatform == Device.Android)
            {
                await Task.Delay(100);
                BackgroundColor = (Color)Application.Current.Resources["Black30Transparent"];
            }
        }

        protected override async void OrientationChanged(DisplayOrientation orientation)
        {
            // KTR dit lijkt de enige manier te zijn om de carousel view werkend te krijgen bij oriëntatie wissel
            await DependencyService.Get<INavigationService>().PopCurrentModalPage();
            await Task.Delay(10);
            await WhatsNewHelper.Show(whatsNewTourViewModel: BindingContext as WhatsNewTourViewModel);
        }

        private void WhatsNewPagesCarousel_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            DependencyService.Get<IA11YService>().Speak(ViewModel.PageInfo, Device.RuntimePlatform == Device.iOS ? 2000 : 0);
            ChangeAnimation((WhatsNewPageModel)e.CurrentItem, (WhatsNewPageModel)e.PreviousItem);
        }

        private static void ChangeAnimation(WhatsNewPageModel currentItem, WhatsNewPageModel previousItem)
        {
            if (previousItem != null)
                previousItem.PlayAnimation = false;

            if (currentItem != null)
                currentItem.PlayAnimation = currentItem.IsAnimation;
        }

        private void WhatsNewPagesCarousel_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            // Onderstaande code alleen nodig om de juiste property changed events af te laten gaan zodat de indicator view en
            // de static vd buttons op de juiste wijze getoond worden
            if (DependencyService.Get<IA11YService>().IsInVoiceOverMode() && e.CenterItemIndex != ViewModel.Index)
            {
                ViewModel.CurrentPageItem = ViewModel.WhatsNewPages[e.CenterItemIndex];
            }
        }
    }
}
