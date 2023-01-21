using DigiD.Common.Constants;
using DigiD.Common.Helpers;
using DigiD.Common.Interfaces;
using DigiD.Common.Services;
#if DEBUG
using System.Diagnostics;
#endif
using System.Windows.Input;
using DigiD.Common.Enums;
using DigiD.Common.SessionModels;
using Xamarin.Forms;

namespace DigiD.Common.BaseClasses
{
    public class BaseViewModel : CommonBaseViewModel
    {
        public BaseViewModel(bool disableLogging = false) : base(disableLogging)
        {
            CanExecute = true;
            IsError = false;
            IsLandingPageActive = false;
#if A11YTEST
            HasBackButton = true;
#endif
        }
    }
}
