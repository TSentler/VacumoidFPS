using Upgrade.Move;
using UnityEngine;

namespace PlayerAbilities
{
    public class SpeedStat : MonoBehaviour
    {
        [SerializeField] private float _boostMultiply = 1.5f, 
            _startSpeed= 125f;

        private SpeedUpgrader _speedUpgrader;
        private float _currentSpeed;
        
        public float Value { get; private set; }

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
            Value = _currentSpeed = _speedUpgrader.CalculateRunSpeed(_startSpeed);
        }

        public void Boost()
        {
            Value = _currentSpeed * _boostMultiply;
        }

        public void ResetBoost()
        {
            Value = _currentSpeed;
        }
    }
}
