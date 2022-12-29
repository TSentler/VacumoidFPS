using Upgrade.Move;
using UnityEngine;
using Bonuses;

namespace PlayerAbilities
{
    public class SpeedStat : MonoBehaviour, IBoostable
    {
        [SerializeField] private float _boostMultiply = 1.5f, 
            _startSpeed= 125f;

        private SpeedUpgrader _speedUpgrader;
        private float _currentSpeed, _currentBoost = 1f;

        public float Value => _currentSpeed * _currentBoost;

        private void Awake()
        {
            _speedUpgrader = FindObjectOfType<SpeedUpgrader>();
        }

        private void OnEnable()
        {
            OnUpgraded();
            _speedUpgrader.Upgraded += OnUpgraded;
        }

        private void OnDisable()
        {
            _speedUpgrader.Upgraded -= OnUpgraded;
        }

        private void OnUpgraded()
        {
            _currentSpeed = _speedUpgrader.CalculateRunSpeed(_startSpeed);
        }

        public void Boost()
        {
            _currentBoost = _boostMultiply;
        }

        public void ResetBoost()
        {
            _currentBoost = 1f;
        }
    }
}
