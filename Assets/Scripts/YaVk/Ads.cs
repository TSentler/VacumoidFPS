using System.Collections;
#if CRAZY_GAMES
using CrazyGames;
#endif
using UnityEngine;
using UnityEngine.Events;
using YaInterstitialAd = Agava.YandexGames.InterstitialAd;
using VkInterstitialAd = Agava.VKGames.Interstitial;
using YaVideoAd = Agava.YandexGames.VideoAd;
using VkVideoAd = Agava.VKGames.VideoAd;

namespace YaVk
{
    public class Ads : MonoBehaviour
    {
        [SerializeField] private Initializer _init;
        
        public IEnumerator ShowInterstitialAdsCoroutine(
            UnityAction<bool> onCloseCallback = null,
            UnityAction<string> onErrorCallback = null,
            UnityAction onYaOpenCallback = null,
            UnityAction onYaOfflineCallback = null)
        {
            yield return _init.TryInitializeSdkCoroutine();
#if !UNITY_WEBGL || UNITY_EDITOR
            onCloseCallback?.Invoke(true);
#elif CRAZY_GAMES
            CrazyAds.Instance.beginAdBreak(
                () => onCloseCallback?.Invoke(true),
                () =>
                {
                    onErrorCallback?.Invoke("Crazygame interstitial ads error");
                    onCloseCallback?.Invoke(false);
                });
#elif YANDEX_GAMES
            YaInterstitialAd.Show(
                () => onYaOpenCallback?.Invoke(),
                wasShown => onCloseCallback?.Invoke(wasShown),
                error => onErrorCallback?.Invoke(error),
                () => onYaOfflineCallback?.Invoke());
#elif VK_GAMES
            VkInterstitialAd.Show(
                () => onCloseCallback?.Invoke(true),
                () =>
                {
                    onErrorCallback?.Invoke("VK interstitial ads error");
                    onCloseCallback?.Invoke(false);
                });
#endif
        }

        public IEnumerator ShowRewardedAdsCoroutine(
            UnityAction onRewardedCallback = null,
            UnityAction onCloseCallback = null,
            UnityAction<string> onErrorCallback = null,
            UnityAction onYaOpenCallback = null) 
        {
            yield return StartCoroutine(_init.TryInitializeSdkCoroutine());
#if !UNITY_WEBGL || UNITY_EDITOR
            onRewardedCallback?.Invoke();
            onCloseCallback?.Invoke();
#elif CRAZY_GAMES
            CrazyAds.Instance.beginAdBreakRewarded(() =>
                {
                    onRewardedCallback?.Invoke();
                    onCloseCallback?.Invoke();
                },
                () =>
                {
                    onErrorCallback?.Invoke("Crazygame rewarded ads error");
                    onCloseCallback?.Invoke();
                });
#elif YANDEX_GAMES
            YaVideoAd.Show(() => onYaOpenCallback?.Invoke(),
                () => onRewardedCallback?.Invoke(),
                () => onCloseCallback?.Invoke(),
                error => onErrorCallback?.Invoke(error));
#elif VK_GAMES
            VkVideoAd.Show(() =>
                {
                    onRewardedCallback?.Invoke();
                    onCloseCallback?.Invoke();
                },
                () =>
                {
                    onErrorCallback?.Invoke("VK rewarded ads error");
                    onCloseCallback?.Invoke();
                });
#endif
        }
    }
}
