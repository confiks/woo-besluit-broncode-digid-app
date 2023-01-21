using DigiD.Common.Services;
using Xamarin.Forms;

namespace DigiD.Common.BaseClasses
{
    public abstract class BaseContentPage : CommonBaseContentPage
    {
        protected BaseContentPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            SetDynamicResource(BackgroundColorProperty, "PageBackgroundColor");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (DependencyService.Get<IA11YService>().IsInVoiceOverMode())
                DependencyService.Get<IA11YService>().Speak(((BaseViewModel)BindingContext).PageIntroName);
        }
    }
}
