using LevelCompleter;
using UnityEngine;
using UnityTools;

namespace Money.LevelCompleter
{
    public class MoneyPerLevel : MonoBehaviour
    {
        [SerializeField] private Wallet _wallet;
        [SerializeField] private MoneyCounter _moneyCounter;
        [SerializeField] private Completer _completer;

        private void OnValidate()
        {
            if (PrefabChecker.InPrefabFileOrStage(gameObject))
                return;
            
            if (_wallet == null)
                Debug.LogWarning("Store was not found!", this);
            if (_moneyCounter == null)
                Debug.LogWarning("MoneyCounter was not found!", this);
            if (_completer == null)
                Debug.LogWarning("Completer was not found!", this);
        }

        private void OnEnable()
        {
            _completer.Completed += EarnLevelMoney;
            _moneyCounter.Collected += EarnMoney;
        }

        private void OnDisable()
        {
            _completer.Completed -= EarnLevelMoney;
            _moneyCounter.Collected -= EarnMoney;
        }
        
        private void EarnLevelMoney()
        {
            EarnMoney(_moneyCounter.LevelTotal);
        }

        private void EarnMoney(int money)
        {
            if (_completer.IsCompleted == false)
                return;
            
            _wallet.Earn(money);
        }
    }
}
