using DigiD.Common.Enums;

namespace DigiD.Common.Models
{
    public class HelpModel
    {
        public HelpModel(InfoPageType type)
        {
            InfoPageType = type;
        }

        public InfoPageType InfoPageType { get; set; }
    }
}
