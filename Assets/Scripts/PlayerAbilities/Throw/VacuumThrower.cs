using UnityEngine;
using UnityEngine.Events;
using Upgrade;

namespace PlayerAbilities.Throw
{
    [RequireComponent(typeof(FixedJoint))]
    public class VacuumThrower : MonoBehaviour
    {
        [Min(0), SerializeField] private float _startSpeed = 10f;

        private FixedJoint _joint, _jointConfig;
        private ThrowObject _throwObject;
        private ThrowUpgrader _throwUpgrader;
        private float _currentSpeed;
        private bool _isThrow;

        public event UnityAction Tied, Throwed;
            
        private void Awake()
        {
            _throwUpgrader = FindObjectOfType<ThrowUpgrader>();
            _jointConfig = GetComponent<FixedJoint>();
        }

        private void OnEnable()
        {
            OnUpgraded();
            _throwUpgrader.Upgraded += OnUpgraded;
        }

        private void OnDisable()
        {
            _throwUpgrader.Upgraded -= OnUpgraded;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_throwObject != null)
                return;
            
            if (other.TryGetComponent(out ThrowObject throwObject))
            {
                _throwObject = throwObject;
                _isThrow = true;
                var rb = _throwObject.Tie(); 
                _joint = gameObject.AddComponent<FixedJoint>();
                _joint.connectedBody = rb;
                //_joint.spring = _jointConfig.spring;
                _joint.breakForce = _jointConfig.breakForce;
                Tied?.Invoke();
            }
        }
        
        private void OnUpgraded()
        {
            _currentSpeed = _throwUpgrader.CalculateThrowSpeed(_startSpeed);
        }
        
        private void OnJointBreak(float breakForce)
        {
            Throw();
        }

        public void Throw()
        {
            if (_isThrow == false)
                return;

            if (_joint != null)
            {
                _joint.connectedBody = null;
                Destroy(_joint);
            }

            var forward = transform.forward;
            forward = new Vector3(forward.x, 0f, forward.z);
            var force = _currentSpeed * forward;
            _throwObject?.Throw(force);
            _throwObject = null;
            Throwed?.Invoke();
        }
    }
}
