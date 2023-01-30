using Bonuses;
using UnityEngine;
using Upgrade;

namespace Vacuum 
{
    [RequireComponent(typeof(SphereCollider))]
    public class VacuumRadius : MonoBehaviour, IBoostable
    {
        [SerializeField] private float _boostMultiplier = 1.5f;
        [Min(0f), SerializeField] private float _startRadius = 1.5f;
        
        private SphereCollider _sphereCollider;
        private VacuumRadiusUpgrader _vacuumRadiusUpgrader;
        private float _radius;

        public float Radius => _radius;

        private void Awake()
        {
            _sphereCollider = GetComponent<SphereCollider>();
            _radius = _sphereCollider.radius;
            _vacuumRadiusUpgrader = FindObjectOfType<VacuumRadiusUpgrader>();
        }

        private void OnEnable()
        {
            OnUpgraded();
            _vacuumRadiusUpgrader.Upgraded += OnUpgraded;
        }

        private void OnDisable()
        {
            _vacuumRadiusUpgrader.Upgraded -= OnUpgraded;
        }

        private void OnUpgraded()
        {
            _sphereCollider.radius = _radius = _vacuumRadiusUpgrader.CalculateRadius(_startRadius);
        }
        
        public void Boost()
        {
            _sphereCollider.radius = _radius * _boostMultiplier;
        }

        public void ResetBoost()
        {
            _sphereCollider.radius = _radius;
        }
    }
}
