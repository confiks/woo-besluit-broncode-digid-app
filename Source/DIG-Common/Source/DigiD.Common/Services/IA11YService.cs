using System.Threading.Tasks;
using Xamarin.Forms;

namespace DigiD.Common.Services
{
    public interface IA11YService
    {
        bool IsInVoiceOverMode();
        Task Speak(string text, int pauseInMs = 0);
        Task NotifyForNewPage();
        Task ChangeA11YFocus(VisualElement visualElement);
    }
}
