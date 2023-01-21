using System.IO;
using DigiD.Common.Mobile.BaseClasses;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DigiD.ViewModels
{
    public class WidPhotoViewModel : BaseViewModel
    {
#if A11YTEST
        public WidPhotoViewModel() : this(null)
        {

        }
#endif
        public ImageSource Photo { get; set; }
        public WidPhotoViewModel(byte[] photo)
        {
            Photo = ImageSource.FromStream(() => new MemoryStream(photo));
            NavCloseCommand = new AsyncCommand(async () => await NavigationService.PopToRoot());
        }
    }
}
