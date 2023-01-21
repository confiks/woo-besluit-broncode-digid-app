using System.Collections.Generic;
using DigiD.Common.Models;
using Newtonsoft.Json;

namespace DigiD.Helpers
{
    /// <summary>
    /// Voeg hier de Open Source libraries toe die gebruikt worden.
    /// </summary>
    public static class OpenSourceLibraryHelper
    {
        private static List<OpenSourceLibraryModel> _libraries;
        public static List<OpenSourceLibraryModel> Libraries
        {
            get
            {
                if (_libraries == null)
                {
                    var json = ResourceHelper.GetAsString("DigiD.Resources.opensource-packages.json");
                    _libraries = JsonConvert.DeserializeObject<List<OpenSourceLibraryModel>>(json);
                }

                return _libraries;
            }
        }
    }
}
