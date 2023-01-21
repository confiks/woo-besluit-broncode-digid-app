using Xamarin.Forms;

namespace DigiD.Common.Controls
{
    public partial class NumberPadButton : ImageButton
    {
        private int _number = -1;

        public int Number
        {
            get => _number;
            set
            {
                _number = value;
                CommandParameter = _number;

                if (_number <= 9)
                {
                    AutomationProperties.SetName(this, _number.ToString());
                    this.SetOnAppTheme<FileImageSource>(SourceProperty, $"pinbutton_{_number}.png", $"pinbutton_{_number}_dark.png");
                }
                else if (_number == 11)
                {
                    AutomationProperties.SetName(this, AppResources.Remove);
                    this.SetOnAppTheme<FileImageSource>(SourceProperty, "pinbutton_backspace.png", "pinbutton_backspace_dark.png");
                }
            }
        }

        public NumberPadButton()
        {
            InitializeComponent();
        }
    }
}
