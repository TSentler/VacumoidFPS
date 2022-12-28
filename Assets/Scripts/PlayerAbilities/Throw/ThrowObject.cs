using UnityEngine;

namespace PlayerAbilities
{
    [RequireComponent(typeof(Rigidbody))]
    public class ThrowObject : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public Rigidbody Tie()
        {
            _rigidbody.useGravity = false;
            _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
            return _rigidbody;
        }
        
        public void Throw(Vector3 force)
        {
            _rigidbody.AddForce(force, ForceMode.Impulse);
            _rigidbody.useGravity = true;
        }
    }
}
