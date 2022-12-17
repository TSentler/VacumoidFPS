using PlayerAbilities.Throw;
using Trash;
using UnityEngine;
using UnityTools;

namespace Upgrade
{
    public class ThrowUpgrader : Upgrader
    {
        private readonly string _upgradeName = "ThrowUpgrade";
        
        [SerializeField] private VacuumThrower _thrower;
        [Min(0f), SerializeField] private float _throwSpeed;
        
        private float Speed => _throwSpeed + _throwSpeed * UpFactor;
        
        protected override void OnValidate()
        {
            base.OnValidate();
            if (PrefabChecker.InPrefabFileOrStage(gameObject))
                return;
            
            if (_thrower == null)
                Debug.LogWarning("VacuumThrower was not found!", this);
        }

        protected override void SetUpgrade()
        {
            _thrower.Upgrade(Speed);
        }

        protected override string GetUpgradeName()
        {
            return _upgradeName;
        }
    }
}
