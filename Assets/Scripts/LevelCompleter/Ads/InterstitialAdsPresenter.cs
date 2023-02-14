using UnityEngine;
using SocialNetwork;

namespace LevelCompleter.Ads
{
    [RequireComponent(typeof(CompletePresenter))]
    public class InterstitialAdsPresenter : MonoBehaviour
    {
        private UnifySocialNetworks _socialNetwork;
        private CompletePresenter _completePresenter;

        private void Awake()
        {
            _socialNetwork = FindObjectOfType<UnifySocialNetworks>();
            _completePresenter = GetComponent<CompletePresenter>();
        }

        private void OnEnable()
        {
            _completePresenter.InterstitialAdsStarted += InterstitialAdsStarted;
        }

        private void OnDisable()
        {
            _completePresenter.InterstitialAdsStarted -= InterstitialAdsStarted;
        }

        private void InterstitialAdsStarted()
        {
            _socialNetwork.Ads.ShowInterstitialAds(); 
        }
    }
}
