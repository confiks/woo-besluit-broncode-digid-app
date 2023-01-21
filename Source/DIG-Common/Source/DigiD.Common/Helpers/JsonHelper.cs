using Newtonsoft.Json;

namespace DigiD.Common.Helpers
{
    public static class JsonHelper
    {
        public static string AsJson(this object data)
        {
            return JsonConvert.SerializeObject(data);
        }
    }
}
