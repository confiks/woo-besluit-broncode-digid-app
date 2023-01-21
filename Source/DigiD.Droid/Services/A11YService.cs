using System.Threading.Tasks;
using Android.Content;
using Android.Views.Accessibility;
using DigiD.Common.Helpers;
using DigiD.Common.Services;
using DigiD.Droid.Services;
using Xamarin.Forms;
using DigiD.Droid.Extensions;

[assembly: Dependency(typeof(A11YService))]
namespace DigiD.Droid.Services
{
    public class A11YService : IA11YService
    {
        private readonly AccessibilityManager _accessibilityManager = (AccessibilityManager)Xamarin.Essentials.Platform.CurrentActivity.GetSystemService(Context.AccessibilityService);

        public bool IsInVoiceOverMode()
        {
            return _accessibilityManager.IsTouchExplorationEnabled;
        }

        public async Task Speak(string text, int pauseInMs = 0)
        {
            if (IsInVoiceOverMode() && !string.IsNullOrEmpty(text))
            {
                if (pauseInMs > 0)
                    await Task.Delay(pauseInMs);
                Xamarin.Essentials.Platform.CurrentActivity.Window?.DecorView?.AnnounceForAccessibility(text.StripHtml());
            }
        }

        public async Task NotifyForNewPage()
        {
            if (!IsInVoiceOverMode())
                return;
            System.Diagnostics.Debug.WriteLine("\n====> A11YService.NotifyForNewPage()...");
            await Device.InvokeOnMainThreadAsync(async () =>
            {
                var @event = AccessibilityEvent.Obtain();
                if (@event != null)
                {
                    @event.EventType = EventTypes.WindowsChanged;
                    await Task.Delay(250);
                    _accessibilityManager.SendAccessibilityEvent(@event);
                }
            });
        }

        public async Task ChangeA11YFocus(VisualElement visualElement)
        {
            if (!IsInVoiceOverMode() || visualElement == null)
                return;

            var view = visualElement.GetViewForAccessibility();
            await Task.Delay(250);
            view?.SendAccessibilityEvent(EventTypes.ViewAccessibilityFocused);
        }
    }
}
