using UnityEngine;
using UnityEngine.Events;
using UnityTools;
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
        private Transform _hook;
        private TransformConstraint _constraint;
        private float _currentSpeed;

        public event UnityAction Tied, Throwed;
            
        private void Awake()
        {
            _throwUpgrader = FindObjectOfType<ThrowUpgrader>();
            _jointConfig = GetComponent<FixedJoint>();
            if (_hook == null)
            {
                GameObject go = new GameObject("Hook");
                _hook = go.transform;
                _hook.parent = transform;
            }
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
                _hook.position = _throwObject.transform.position;
                _hook.rotation = _throwObject.transform.rotation;
                CreateJoint();

                Tied?.Invoke();
            }
        }

        private void LateUpdate()
        {
            if (_throwObject == null)
                return;
            
            _throwObject.transform.position = _hook.position;
            _throwObject.transform.rotation = _hook.rotation;
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
            if (_throwObject == null)
                return;

            DestroyJoint();

            var forward = transform.forward;
            forward = new Vector3(forward.x, 0f, forward.z);
            var force = _currentSpeed * forward;
            _throwObject?.Throw(force);
            _throwObject = null;
            Throwed?.Invoke();
        }

        private void CreateJoint()
        {
            var rigidbody = _throwObject.Tie();
            _joint = gameObject.AddComponent<FixedJoint>();
            _joint.connectedBody = rigidbody;
            //_joint.spring = _jointConfig.spring;
            _joint.breakForce = _jointConfig.breakForce;
        }

        private void DestroyJoint()
        {
            if (_joint != null)
            {
                _joint.connectedBody = null;
                Destroy(_joint);
            }
        }
    }
}
