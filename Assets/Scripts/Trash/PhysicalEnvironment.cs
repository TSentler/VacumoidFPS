using Suckables;
using UnityEngine;

namespace Trash
{
    [RequireComponent(typeof(Rigidbody))]
    public class PhysicalEnvironment : MonoBehaviour, ISuckableToCenter
    {
        [SerializeField] private float _speed = 135f;

        private ISuckCenter _target;
        private Rigidbody _rigidbody;
        private bool _isTied;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
    
        private void FixedUpdate()
        {
            if (_target == null)
                return;

            var deltaSpeed = _speed * _target.ExtraSpeedMultiply 
                                    * Time.deltaTime;
            var direction = 
                (_target.GetPosition() - transform.position).normalized;
            var force = direction * deltaSpeed;
            _rigidbody.AddForce(force, ForceMode.Force);
        }

        public void Suck(ISuckCenter target)
        {
            _target = target;
        }
    }
}
