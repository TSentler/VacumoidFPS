using System;
using System.Collections;
using Agava.VKGames;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace YaVk
{
    [DisallowMultipleComponent,
     RequireComponent(typeof(Initializer))]
    public class SocialNetwork : MonoBehaviour
    {
        private Initializer _init;
        private UnifiedAdsPlatforms _unifiedAdsPlatforms;
        private MobileDevice _mobileDevice;
        private UnifiedLeaderboardPlatforms _unifiedLeaderboardPlatforms;

        public event UnityAction AdsStarted, AdsEnded;
        
        public bool IsAds { get; private set; }

        public UnifiedLeaderboardPlatforms Leaderboard =>
            _unifiedLeaderboardPlatforms;
        
        private void Awake()
        {
            _init = GetComponent<Initializer>();
            _mobileDevice = new MobileDevice(_init);
            _unifiedAdsPlatforms = new UnifiedAdsPlatforms(_init);
            _unifiedLeaderboardPlatforms = new UnifiedLeaderboardPlatforms();
        }

        private void Start()
        {
            StartCoroutine(_init.TryInitializeSdkCoroutine());
        }

        public IEnumerator CheckMobileDeviceCoroutine(
            UnityAction<bool> callback) => _mobileDevice.Check(callback);
        
        public bool IsAdsAccess()
        {
#if ITCHIO_GAMES
            return false;
#endif
            return true;
        }
        
        public void ShowInterstitialAds(
            UnityAction<bool> onCloseCallback = null,
            UnityAction<string> onErrorCallback = null,
            UnityAction onYaOpenCallback = null,
            UnityAction onYaOfflineCallback = null)
        {
            if(IsAdsAccess() == false)
                return;
            
            IsAds = true;
            AdsStarted?.Invoke();
            onCloseCallback += wasShown =>
            {
                IsAds = false;
                AdsEnded?.Invoke();
            };
            StartCoroutine(
                _unifiedAdsPlatforms.ShowInterstitialAdsCoroutine(onCloseCallback, 
                    onErrorCallback, onYaOpenCallback, onYaOfflineCallback));
        }

        public void ShowRewardedAds(UnityAction onRewardedCallback = null,
            UnityAction onCloseCallback = null,
            UnityAction<string> onErrorCallback = null,
            UnityAction onYaOpenCallback = null)
        {
            if(IsAdsAccess() == false)
                return;
            
            IsAds = true;
            AdsStarted?.Invoke();
            onCloseCallback += () =>
            {
                IsAds = false;
                AdsEnded?.Invoke();
            };
            StartCoroutine(
                _unifiedAdsPlatforms.ShowRewardedAdsCoroutine(onRewardedCallback, 
                    onCloseCallback, onErrorCallback, onYaOpenCallback));
        }
        
    }
}
