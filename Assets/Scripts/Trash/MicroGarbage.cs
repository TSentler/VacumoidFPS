using UnityEngine;

namespace Trash
{
    public class MicroGarbage : Garbage
    {
        private readonly float _minRotationRatio = 0.01f,
            _maxRotationRatio = 1, _minVelocity = 0.01f,
            _maxVelocity = 0.3f;
            
        [SerializeField] private float _damp = 5f;
        
        private Coroutine _suckCoroutine;
        private Vector3 _direction;
        private float _startDistance, _velocity, _axeleration = 0.5f;
        
        private bool IsSuck => Target != null || _velocity > _minVelocity;

        private void Start()
        {
            if (IsSuck == false)
            {
                gameObject.SetActive(false);
            } 
        }

        private void FixedUpdate()
        {
            if (IsSuck)
            {
                if (Target != null)
                {
                    _velocity += _axeleration * Time.deltaTime;
                }
                else
                {
                    _velocity -= _damp * Time.deltaTime;
                }
                _velocity = Mathf.Clamp(_velocity, 0f, _maxVelocity);

                MoveToTarget();
            }
        }

        private void MoveToTarget()
        {
            if (Target != null)
            {
                _direction = CalculateDirection(
                    Target.transform.position, _direction);
            }
            transform.position += _direction * _velocity;
        }

        private Vector3 CalculateDirection(Vector3 targetPosition, 
            Vector3 currentDirection)
        {
            var direction = targetPosition - transform.position;
            var distance = direction.magnitude;
            direction.Normalize();
            var distanceRatio = 1f - distance / _startDistance;
            var rotationRatio = Mathf.Lerp(_minRotationRatio,
                _maxRotationRatio, distanceRatio) * 2f;
            return Vector3.Lerp(currentDirection, direction, rotationRatio);
        }

        protected override void SuckHandler()
        {
            if (_velocity > _minVelocity)
                return;
            
            _direction = Target.transform.position - transform.position;
            _startDistance = _direction.magnitude;
            _direction.Normalize();
            _velocity = 0f;
        }
    }
}