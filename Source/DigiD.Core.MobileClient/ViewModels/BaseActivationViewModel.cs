using System;
using DigiD.Common.Mobile.BaseClasses;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.CommunityToolkit.ObjectModel;

namespace DigiD.ViewModels
{
    public class BaseActivationViewModel : BaseViewModel
    {
        public bool IsBlurVisible { get; set; }
        public IAsyncCommand NextCommand { get; set; }
        
        public event EventHandler<bool> NavigationBarVisibleEvent
        {
            add => _navigationBarVisibleEventEventManager.AddEventHandler(value);
            remove => _navigationBarVisibleEventEventManager.RemoveEventHandler(value);
        }

        private readonly WeakEventManager<bool> _navigationBarVisibleEventEventManager = new WeakEventManager<bool>();

        
        protected void SetNavbar(bool visible)
        {
            _navigationBarVisibleEventEventManager.RaiseEvent(this, visible, nameof(NavigationBarVisibleEvent));
        }
    }
}
