using System.Threading;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace DigiD.Models
{
    public class WhatsNewPageModel : BindableObject
    {
        public string AlternateText { get; set; }
        public bool IsAnimation => Source.EndsWith(".json", true, Thread.CurrentThread.CurrentUICulture);
        public bool PlayAnimation { get; set; }
        public string Source { get; set; }

        private string _text;
        public string Text
        {
            get
            {
                if (string.IsNullOrEmpty(_text))
                    return Device.RuntimePlatform == Device.Android
                        ? AndroidSpecificText
                        : iOSSpecificText;

                return _text;
            }
            set => _text = value;
        }

        private string _title;
        public string Title
        {
            get
            {
                if (string.IsNullOrEmpty(_title))
                    return Device.RuntimePlatform == Device.Android
                        ? AndroidSpecificTitle
                        : iOSSpecificTitle;

                return _title;
            }
            set => _title = value;
        }

        public string AndroidSpecificTitle { get; set; }
        public string iOSSpecificTitle { get; set; }
        public string AndroidSpecificText { get; set; }
        public string iOSSpecificText { get; set; }

        [JsonIgnore]
        public string AnimationSource => IsAnimation ? Source : null;

        [JsonIgnore]
        public string ImageSource => IsAnimation ? null : Source;
    }
}
