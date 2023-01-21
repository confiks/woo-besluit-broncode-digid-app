using DigiD.Common.Helpers;
using DigiD.Common.Services;
using DigiD.iOS.Services;
using UIKit;
using Xamarin.Forms;
using DigiD.iOS.Extensions;
using System.Threading.Tasks;

[assembly: Dependency(typeof(A11YService))]
namespace DigiD.iOS.Services
{
    public class A11YService : IA11YService
    {
        public Task ChangeA11YFocus(VisualElement visualElement)
        {
            if (!UIAccessibility.IsVoiceOverRunning || visualElement == null)
                return Task.CompletedTask;

            var nativeView = visualElement.GetViewForAccessibility();
            if (nativeView != null)
                UIAccessibility.PostNotification(UIAccessibilityPostNotification.LayoutChanged, nativeView);

            return Task.CompletedTask;
        }

        public bool IsInVoiceOverMode()
        {
            return UIAccessibility.IsVoiceOverRunning;
        }

        public Task NotifyForNewPage()
        {
            if (UIAccessibility.IsVoiceOverRunning)
            {
                UIAccessibility.PostNotification(UIKit.UIAccessibilityPostNotification.ScreenChanged, null);
            }

            return Task.CompletedTask;
        }

        public async Task Speak(string text, int pauseInMs = 0)
        {
            if (UIAccessibility.IsVoiceOverRunning && !string.IsNullOrEmpty(text))
            {
                if (pauseInMs > 0)
                    await Task.Delay(pauseInMs);

                UIAccessibility.PostNotification(UIAccessibilityPostNotification.Announcement, Foundation.NSObject.FromObject(text.StripHtml()));
            }
        }
    }
}
