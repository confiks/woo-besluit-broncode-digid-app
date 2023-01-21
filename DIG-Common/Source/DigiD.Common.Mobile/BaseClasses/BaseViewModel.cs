using DigiD.Common.BaseClasses;

namespace DigiD.Common.Mobile.BaseClasses
{
    public class BaseViewModel : CommonBaseViewModel
    {
        protected bool IsNextCommandExecuted;

        public BaseViewModel(bool disableLogging = false, bool preventLock = false) : base(disableLogging, preventLock)
        {
            CanExecute = true;
            IsError = false;

#if A11YTEST
            HasBackButton = true;
#endif
        }
    }
}
