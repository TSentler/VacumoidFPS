using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SocialNetwork
{
    [RequireComponent(typeof(Button))]
    public class RewardAdsButton : MonoBehaviour
    {
        private UnifySocialNetworks _socialNetwork;
        private Button _button;
        
        public event UnityAction Rewarded;

        private void Awake()
        {
            _socialNetwork = FindObjectOfType<UnifySocialNetworks>();
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(ShowRewardedAds);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ShowRewardedAds);
        }

        private void ShowRewardedAds()
        {
            _socialNetwork.Ads.ShowRewardedAds(Rewarded);
        }
    }
}
