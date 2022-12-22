using Bonuses;
using Suckables;
using UnityEngine;

namespace Vacuum
{
    [RequireComponent(typeof(SphereCollider))]
    public class GarbageSucker : MonoBehaviour, ISuckCenter, IBoostable
    {
        [Min(0f), SerializeField] private float _environmentExtraSuckSpeed = 1f,
            _boostMultiply = 5f;
        
        private float _oldSpeed;
        
        public float ExtraSpeedMultiply => _environmentExtraSuckSpeed;
        
        private void Awake()
        {
            _oldSpeed = _environmentExtraSuckSpeed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ISuckableToCenter physicalEnvironment))
            {
                physicalEnvironment.Suck(this);
            }
            
            if (other.TryGetComponent(out ISuckable simpleGarbage))
            {
                simpleGarbage.Suck();
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out ISuckable simpleGarbage))
            {
                simpleGarbage.Suck();
            }
            if (other.TryGetComponent(out ISuckableToCenter physicalEnvironment))
            {
                physicalEnvironment.Suck(null);
            }
        }
        
        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public void Boost()
        {
            _environmentExtraSuckSpeed *= _boostMultiply;
        }

        public void ResetBoost()
        {
            _environmentExtraSuckSpeed = _oldSpeed;
        }
    }
}
