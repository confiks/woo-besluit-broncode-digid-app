using System;
using System.Windows.Input;
using DigiD.Common.EID.Demo;
using DigiD.Common.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DemoHelper = DigiD.Common.EID.Demo.DemoHelper;

namespace DigiD.UI.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardStatePopup : DigiD.Common.BaseClasses.BasePopup
    {
        private int _tries;
        private bool _isSuspended;
        private bool _isBlocked;

        public bool ChangePinRequired { get; set; }
        
        public string PIN => DemoHelper.CardState.Value.PIN;
        public int Tries => IsSuspended ? 1 : IsBlocked ? 0 : _tries;
        
        public bool IsSuspended
        {
            get => _isSuspended;
            set
            {
                if (IsBlocked)
                    IsBlocked = false;

                _isSuspended = value;
                OnPropertyChanged(nameof(IsSuspended));
            }
        }
        
        public bool IsBlocked
        {
            get => _isBlocked;
            set
            {
                if (IsSuspended)
                    IsSuspended = false;

                _isBlocked = value;
                OnPropertyChanged(nameof(IsBlocked));
            }
        }

        public CardStatePopup()
        {
            var width = DisplayHelper.Width * .8;

            if (Device.Idiom == TargetIdiom.Tablet)
            {
                width = 340;
            }
            Size = new Size(width, 300);

            InitializeComponent();
            BindingContext = this;

            _tries = DemoHelper.CardState.Value.PINTries;
            IsSuspended = DemoHelper.CardState.Value.IsSuspended;
            IsBlocked = DemoHelper.CardState.Value.IsBlocked;
            ChangePinRequired = DemoHelper.CardState.Value.ChangePinRequired;
            
            OnPropertyChanged(nameof(Tries));
        }

        protected override void LightDismiss()
        {
            base.LightDismiss();
            IsOpened = false;
            DemoHelper.CardState = new Lazy<CardState>(() => new CardState()
            {
                ChangePinRequired = ChangePinRequired,
                PINTries = Tries
            });
        }

        public ICommand ResetCommand => new Command(() =>
        {
            _tries = 5;
            ChangePinRequired = true;
            IsSuspended = false;
            IsBlocked = false;
        });
    }
}
