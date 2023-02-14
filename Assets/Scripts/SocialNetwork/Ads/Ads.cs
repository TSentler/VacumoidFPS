using System;
using UnityEngine;
using UnityEngine.Events;

namespace SocialNetwork
{
    public class Ads : MonoBehaviour
    {
        private UnifiedAdsPlatforms _unifiedAdsPlatforms;

        public event UnityAction Started, AdsEnded;

        public bool IsRun { get; private set; }
        
        public void Initialize(Initializer init)
        {
            _unifiedAdsPlatforms = new UnifiedAdsPlatforms(init);
        }

        public bool HasAccess()
        {
            if (Defines.IsItchIoGames)
            {
                return false;
            }
            
            return true;
        }
        
        public void ShowInterstitialAds(
            UnityAction<bool> onCloseCallback = null,
            UnityAction<string> onErrorCallback = null,
            UnityAction onYaOpenCallback = null,
            UnityAction onYaOfflineCallback = null)
        {
            if(HasAccess() == false)
                return;
            
            IsRun = true;
            Started?.Invoke();
            onCloseCallback += wasShown =>
            {
                IsRun = false;
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
            if(HasAccess() == false)
                return;
            
            IsRun = true;
            Started?.Invoke();
            onCloseCallback += () =>
            {
                IsRun = false;
                AdsEnded?.Invoke();
            };
            StartCoroutine(
                _unifiedAdsPlatforms.ShowRewardedAdsCoroutine(onRewardedCallback, 
                    onCloseCallback, onErrorCallback, onYaOpenCallback));
        }
        
    }
}