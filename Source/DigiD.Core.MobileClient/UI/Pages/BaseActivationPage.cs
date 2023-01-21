using DigiD.Common.Mobile.BaseClasses;
using DigiD.ViewModels;
using Xamarin.Forms;

namespace DigiD.UI.Pages
{
    public class BaseActivationPage : BaseContentPage
    {
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            ((BaseActivationViewModel)BindingContext).NavigationBarVisibleEvent += OnNavigationBarVisibleEvent;
        }

        ~BaseActivationPage()
        {
            if (BindingContext != null)
                ((BaseActivationViewModel)BindingContext).NavigationBarVisibleEvent -= OnNavigationBarVisibleEvent;
        }

        private void OnNavigationBarVisibleEvent(object sender, bool e)
        {
            NavigationPage.SetHasNavigationBar(this, e);
        }
    }
}
