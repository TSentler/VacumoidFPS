using System.Collections;
#if CRAZY_GAMES
using CrazyGames;
#endif
using UnityEngine.Events;
using YaInterstitialAd = Agava.YandexGames.InterstitialAd;
using VkInterstitialAd = Agava.VKGames.Interstitial;
using YaVideoAd = Agava.YandexGames.VideoAd;
using VkVideoAd = Agava.VKGames.VideoAd;

namespace YaVk
{
    public class UnifiedAdsPlatforms
    {
        public UnifiedAdsPlatforms(Initializer init)
        {
            _init = init;
        }
        
        private readonly Initializer _init;
        
        public IEnumerator ShowInterstitialAdsCoroutine(
            UnityAction<bool> onCloseCallback = null,
            UnityAction<string> onErrorCallback = null,
            UnityAction onYaOpenCallback = null,
            UnityAction onYaOfflineCallback = null)
        {
            yield return _init.TryInitializeSdkCoroutine();
            if (Defines.IsUnityWebGl == false
                || Defines.IsUnityEditor)
            {
                onCloseCallback?.Invoke(true);
            }
            else if (Defines.IsYandexGames)
            {
                YaInterstitialAd.Show(
                    () => onYaOpenCallback?.Invoke(),
                    wasShown => onCloseCallback?.Invoke(wasShown),
                    error => onErrorCallback?.Invoke(error),
                    () => onYaOfflineCallback?.Invoke());
            }
            else if (Defines.IsVkGames)
            {
                VkInterstitialAd.Show(
                    () => onCloseCallback?.Invoke(true),
                    () =>
                    {
                        onErrorCallback?.Invoke("VK interstitial ads error");
                        onCloseCallback?.Invoke(false);
                    });
            }
            else
            {
#if CRAZY_GAMES
                CrazyAds.Instance.beginAdBreak(
                    () => onCloseCallback?.Invoke(true),
                    () =>
                    {
                        onErrorCallback?.Invoke("Crazygame interstitial ads error");
                        onCloseCallback?.Invoke(false);
                    });
#endif
            }
        }

        public IEnumerator ShowRewardedAdsCoroutine(
            UnityAction onRewardedCallback = null,
            UnityAction onCloseCallback = null,
            UnityAction<string> onErrorCallback = null,
            UnityAction onYaOpenCallback = null) 
        {
            yield return _init.TryInitializeSdkCoroutine();
            if (Defines.IsUnityWebGl == false
                || Defines.IsUnityEditor)
            {
                onRewardedCallback?.Invoke();
                onCloseCallback?.Invoke();
            }
            if (Defines.IsYandexGames)
            {
                YaVideoAd.Show(() => onYaOpenCallback?.Invoke(),
                    () => onRewardedCallback?.Invoke(),
                    () => onCloseCallback?.Invoke(),
                    error => onErrorCallback?.Invoke(error));
            }
            if (Defines.IsVkGames)
            {
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
            }
            else
            {
#if CRAZY_GAMES
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
#endif
            }
        }
    }
}
