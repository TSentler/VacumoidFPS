using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YaVk
{
    [RequireComponent(typeof(Button))]
    public class RewardAdsButton : MonoBehaviour
    {
        private SocialNetwork _socialNetwork;
        private Button _button;
        
        public event UnityAction Rewarded;

        private void Awake()
        {
            _socialNetwork = FindObjectOfType<SocialNetwork>();
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
            _socialNetwork.ShowRewardedAds(Rewarded);
        }
    }
}
