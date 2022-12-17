using Money;
using UnityEngine;
using UnityTools;
using YaVk;

namespace Ads
{
    [RequireComponent(typeof(MoneyCounter))]
    public class MoneyRewardMultiplier : MonoBehaviour
    {
        [SerializeField] private RewardAdsButton _rewardAdsButton;
        
        private MoneyCounter _moneyCounter;

        private void OnValidate()
        {
            if (PrefabChecker.InPrefabFileOrStage(gameObject))
                return;
            
            if (_rewardAdsButton == null)
                Debug.LogWarning("RewardAdsButton was not found!", this);
        }

        private void Awake()
        { 
            _moneyCounter = GetComponent<MoneyCounter>();
        }

        private void OnEnable()
        {
            _rewardAdsButton.Rewarded += OnRewarded;
        }

        private void OnDisable()
        {
            _rewardAdsButton.Rewarded -= OnRewarded;
        }

        private void OnRewarded()
        {
            _moneyCounter.Reward();
        }
    }
}
