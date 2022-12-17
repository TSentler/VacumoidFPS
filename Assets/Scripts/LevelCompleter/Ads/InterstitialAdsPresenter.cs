using UnityEngine;
using YaVk;

namespace LevelCompleter.Ads
{
    [RequireComponent(typeof(CompletePresenter))]
    public class InterstitialAdsPresenter : MonoBehaviour
    {
        private SocialNetwork _socialNetwork;
        private CompletePresenter _completePresenter;

        private void Awake()
        {
            _socialNetwork = FindObjectOfType<SocialNetwork>();
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
            _socialNetwork.ShowInterstitialAds(); 
        }
    }
}
