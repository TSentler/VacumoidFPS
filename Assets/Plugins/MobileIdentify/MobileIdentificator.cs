using System.Runtime.InteropServices;

namespace Plugins.MobileIdentify
{
    public static class MobileIdentificator
    {
        [DllImport("__Internal")]
        public static extern bool IsMobile();
    }
}