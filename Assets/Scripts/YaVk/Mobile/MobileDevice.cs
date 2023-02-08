using System.Collections;
using Agava.YandexGames;
using Plugins.MobileIdentify;
using UnityEngine;
using UnityEngine.Events;
using DeviceType = Agava.YandexGames.DeviceType;

namespace YaVk.Mobile
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

            if (Defines.IsUnityWebGl && Defines.IsUnityEditor == false
                && MobileIdentificator.IsMobile())
            {
                isMobile = true;
            }
            else if (Defines.IsYandexGames)
            {
                isMobile = Device.Type != DeviceType.Desktop;
            }
            else if (Defines.IsVkMobileGames)
            {
                isMobile = true;
            }
            else
            {
                isMobile = Application.isMobilePlatform 
                           || Application.platform == RuntimePlatform.Android
                           || SystemInfo.deviceModel.StartsWith("iPad");
            }
            callback.Invoke(isMobile);
        }
    }
}