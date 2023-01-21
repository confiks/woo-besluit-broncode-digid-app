using System.Threading.Tasks;
using Xamarin.Essentials;

namespace DigiD.Common.Helpers
{
    public static class PermissionHelper
    {
        public static async Task<bool> CheckCameraPermissions()
        {
            var permission = await Permissions.CheckStatusAsync<Permissions.Camera>();

            if (permission == PermissionStatus.Denied || permission == PermissionStatus.Unknown || permission == PermissionStatus.Disabled)
                return false;

            return true;
        }
    }
}
