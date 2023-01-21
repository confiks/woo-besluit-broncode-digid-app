using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DigiD.Common;
using DigiD.Common.BaseClasses;
using DigiD.Common.Helpers;
using DigiD.Helpers;
using DigiD.UI.Pages;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DigiD.Models
{
    public class MenuItem : BindableObject
    {
        public static BindableProperty IconSourceProperty = BindableProperty.Create(nameof(IconSource), typeof(ImageSource), typeof(MenuItem));

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                AccessibilityText = value;
            }
        }

        public Thickness IconMargin { get; set; } = new Thickness(10);

        public ImageSource IconSource
        {
            get => (ImageSource)GetValue(IconSourceProperty);
            set => SetValue(IconSourceProperty, value);
        }

        private string _icon;
        public string Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                this.SetOnAppTheme<ImageSource>(IconSourceProperty, Icon, ThemeHelper.Convert(Icon, true));
            }
        }

        public bool IsChevronVisible => TargetViewModel != null || !IsExternalLink;
        public Type TargetViewModel { get; set; }
        public object[] Arguments { get; set; }
        public Func<Task> Action { get; set; }

        // KTR: voorkomt van het onterecht niet tonen van chevron en foutief
        // voorlezen tekst in A11Y-mode.
        public bool IsExternalLink { get; set; } = false;

        private string _accessibilityText;
        public string AccessibilityText
        {
            get
            {
                if (string.IsNullOrEmpty(_accessibilityText))
                {
                    _accessibilityText = Title;
                }
                return _accessibilityText + (IsChevronVisible ? string.Empty : $", {AppResources.AccessibilityMenuItemLinkText}");
            }
            set => _accessibilityText = value;
        }

        public AsyncCommand ItemSelectedCommand => new AsyncCommand(async () =>
        {
            if (Application.Current.MainPage is CustomNavigationPage { RootPage: LandingPage lp })
                await lp.CloseMenu();

            if (TargetViewModel != null)
                await NavigationHelper.PushTargetViewModel(TargetViewModel, Arguments);
            else
                Action?.Invoke();
        });
    }

    public class MenuBlock
    {
        public string Header { get; set; }
        public List<MenuItem> MenuItems { get; set; }
        public MenuBlock(string header, List<MenuItem> menuItems)
        {
            Header = header;
            MenuItems = menuItems;
        }
    }
}
