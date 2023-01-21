using System;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace DigiD.Common.Helpers
{
    public static class TextResourceHelper
    {
        private const string ResourceId = "DigiD.Common.AppResources";

        internal static readonly Lazy<ResourceManager> Resmgr =
        new Lazy<ResourceManager>(() =>
            new ResourceManager(ResourceId, typeof(TextResourceHelper)
                    .GetTypeInfo().Assembly));

        public static string GetTextResource(string id)
        {
            var translation = Resmgr.Value.GetString(id, CultureInfo.DefaultThreadCurrentCulture);

            if (translation == null)
            {
#if DEBUG
                throw new ArgumentException(
                    $"Key '{id}' was not found in resources '{ResourceId}' for culture '{CultureInfo.DefaultThreadCurrentCulture.Name}'.", nameof(id));
#endif
            }
            return translation;
        }
    }
}
