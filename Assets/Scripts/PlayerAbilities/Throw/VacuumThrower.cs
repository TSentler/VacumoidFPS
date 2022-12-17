using UnityEngine;
using UnityEngine.Events;

namespace PlayerAbilities.Throw
{
    [RequireComponent(typeof(FixedJoint))]
    public class VacuumThrower : MonoBehaviour
    {
        [Min(0), SerializeField] private float _speed = 10f;

        private FixedJoint _joint, _jointConfig;
        private ThrowObject _throwObject;
        private bool _isThrow;

        public event UnityAction Tied, Throwed;
            
        private void Awake()
        {
            _jointConfig = GetComponent<FixedJoint>();
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
            var force = _speed * forward;
            _throwObject?.Throw(force);
            _throwObject = null;
            Throwed?.Invoke();
        }

        public void Upgrade(float speed)
        {
            _speed = speed;
        }
    }
}
