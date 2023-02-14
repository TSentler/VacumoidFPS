using System.Collections;
using Agava.YandexGames;
using Plugins.MobileIdentify;
using SocialNetwork.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace SocialNetwork.Mobile
{
    public class MobileDevice
    {
        public MobileDevice(Initializer init, IMobileChecker provider)
        {
            _init = init;
            _platformProvider = provider;
        }
        
        private readonly Initializer _init;
        private readonly IMobileChecker _platformProvider;

        public IEnumerator Check(
            UnityAction<bool> callback)
        {
            yield return _init.TryInitializeSdkCoroutine();

            bool isMobile = _platformProvider.MobileCheck() 
                            || MobileIdentificator.IsMobile() 
                            || Application.isMobilePlatform 
                            || Application.platform == RuntimePlatform.Android
                            || SystemInfo.deviceModel.StartsWith("iPad");
            
            callback.Invoke(isMobile);
        }
    }
}