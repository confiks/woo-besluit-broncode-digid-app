using System.IO;
using System.Reflection;

namespace DigiD.Helpers
{
    public static class ResourceHelper
    {
        public static string GetAsString(string fileName)
        {
            try
            {
                var assembly = typeof(ResourceHelper).GetTypeInfo().Assembly;
                using (var stream = assembly.GetManifestResourceStream(fileName))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
