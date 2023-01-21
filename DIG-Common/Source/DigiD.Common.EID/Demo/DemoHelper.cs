using System;

namespace DigiD.Common.EID.Demo
{
    public static class DemoHelper
    {
        public static Lazy<CardState> CardState { get; set; } = new Lazy<CardState>();
    }
}
