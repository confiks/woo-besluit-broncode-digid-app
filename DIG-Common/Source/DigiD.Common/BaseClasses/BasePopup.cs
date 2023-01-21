using Xamarin.CommunityToolkit.UI.Views;

namespace DigiD.Common.BaseClasses
{
    public class BasePopup : Popup
    {
        public bool IsOpened { get; set; }
    }

    public class BasePopup<T> : Popup<T>
    {
        public bool IsOpened { get; set; }
    }
}
