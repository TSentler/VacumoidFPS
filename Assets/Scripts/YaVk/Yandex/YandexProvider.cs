using System;
using System.Collections;
using Agava.YandexGames;
using YaVk.Interfaces;

namespace YaVk.Yandex
{
    public class YandexProvider : IInitialize
    {
        public IEnumerator Initialize(Action onSuccessCallback)
        {
            yield return YandexGamesSdk.Initialize(
                onSuccessCallback: onSuccessCallback);
        }
    }
}