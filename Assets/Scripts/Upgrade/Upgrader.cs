using Money;
using Saves;
using UnityEngine;
using UnityEngine.Events;
using UnityTools;

namespace Upgrade
{
    public abstract class Upgrader : MonoBehaviour
    {
        [Min(-1), SerializeField] private int _upLevelOverride = -1;
        [Min(0), SerializeField] private int _coast;
        [Min(0), SerializeField] private float _upFactor = 0.1f;
        [SerializeField] private Wallet _wallet;

        private GameSaver _saver;
        private int _upLevel;

        public event UnityAction Upgraded;
        
        public int UpLevel => _upLevel;
        public int Coast => _upLevel * (_upLevel + 1) / 2 * 100 + _coast;
        protected float UpFactor => _upLevel * _upFactor;

        protected abstract string GetUpgradeName();

        protected virtual void OnValidate()
        {
            if (PrefabChecker.InPrefabFileOrStage(gameObject))
                return;
            
            if (_wallet == null)
                Debug.LogWarning("Wallet was not found!", this);
        }

        protected virtual void Awake()
        {
            _saver = new GameSaver();
            _upLevel = _saver.Load(GetUpgradeName());
#if UNITY_EDITOR
            if (_upLevelOverride != -1)
            {
                _upLevel = _upLevelOverride;
            } 
#endif
            Upgraded?.Invoke();
        }

        public void Upgrade()
        {
            _wallet.Buy(Coast, () =>
            {
                _upLevel++;
                _saver.Save(GetUpgradeName(), _upLevel);
                Upgraded?.Invoke();
            });
        }
    }
}
