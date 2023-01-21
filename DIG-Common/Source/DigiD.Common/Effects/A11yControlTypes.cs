using System;
namespace DigiD.Common.Effects
{
    [Flags]
    public enum A11YControlTypes
    {
        None = 0,
        Button = 1,
        Toggle = 2,
        Link = 4,
        Header = 8,
        LiveUpdate = 16,
        MenuItem = 32,
        LinkLabel = 64
    }
}
