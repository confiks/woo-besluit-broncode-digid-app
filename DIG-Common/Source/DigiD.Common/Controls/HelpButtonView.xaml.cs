using System;
using System.Windows.Input;
using DigiD.Common.Enums;
using DigiD.Common.Interfaces;
using DigiD.Common.ViewModels;
using Xamarin.Forms;

namespace DigiD.Common.Controls
{
	public partial class HelpButtonView : Grid
    {
        public static readonly BindableProperty InfoPageTypeProperty = BindableProperty.Create(nameof(InfoPageType), typeof(InfoPageType), typeof(HelpButtonView), InfoPageType.Undefined);

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(HelpButtonView), null, BindingMode.OneTime);

        public InfoPageType InfoPageType
	    {
	        get => (InfoPageType) GetValue(InfoPageTypeProperty);
	        set => SetValue(InfoPageTypeProperty, value);
	    }
        
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public HelpButtonView()
		{
			InitializeComponent ();
        }

        protected override void OnTabIndexPropertyChanged(int oldValue, int newValue)
        {
            base.OnTabIndexPropertyChanged(oldValue, newValue);
            if (__HelpButton__ != null)
                __HelpButton__.TabIndex = newValue;
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            if (InfoPageType != InfoPageType.Undefined)
            {
                var infoViewModel = new InfoViewModel(new Models.HelpModel(InfoPageType));
                Device.BeginInvokeOnMainThread(async () =>  await DependencyService.Get<IPopupService>().OpenPopup(infoViewModel));
            }

            if (Command != null && Command.CanExecute(null))
                Command.Execute(null);
        }
    }
}
