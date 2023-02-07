using System.Collections;
using Agava.YandexGames;
using UnityEngine.Events;

namespace YaVk
{
    public class MobileDevice
    {
        public MobileDevice(Initializer init)
        {
            _init = init;
        }
        
        private readonly Initializer _init;
        
        public IEnumerator Check(
            UnityAction<bool> callback)
        {
            yield return _init.TryInitializeSdkCoroutine();

            bool isMobile = false;
#if YANDEX_GAMES
            isMobile = Device.Type != DeviceType.Desktop;
#elif VK_GAMES_MOBILE
            isMobile = true;
#else
            isMobile = Application.isMobilePlatform;
#endif
            callback.Invoke(isMobile);
        }
    }
}