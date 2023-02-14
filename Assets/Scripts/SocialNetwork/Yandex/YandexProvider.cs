using System;
using System.Collections;
using Agava.YandexGames;
using SocialNetwork.Interfaces;

namespace SocialNetwork.Yandex
{
    public class YandexProvider : IInitialize, IMobileChecker
    {
        public IEnumerator Initialize(Action onSuccessCallback)
        {
            yield return YandexGamesSdk.Initialize(
                onSuccessCallback: onSuccessCallback);
        }

        public bool MobileCheck()
        {
            return Device.Type != DeviceType.Desktop;
        }
    }
}