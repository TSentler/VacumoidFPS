using Trash;
using UnityEngine;

namespace EcsMicroTrash.Components
{
    public struct MicroGarbageComponent
    {
        public MicroGarbageComponent(MicroGarbage microGarbage)
        {
            _microGarbage = microGarbage;
            _transform = microGarbage.transform;
            _minRotationRatio = 0.01f;
            _maxRotationRatio = 1f;
            _minVelocity = 0.01f;
            _maxVelocity = 0.3f;
            _damp = 12;
            
            Position = _transform.position;
            Target = -1;
            _direction = default;
            _startDistance = 0f;
            _velocity = 0f;
            _axeleration = 0.5f;
        }
        
        private readonly MicroGarbage _microGarbage;
        private readonly Transform _transform;

        private readonly float _minRotationRatio,
            _maxRotationRatio,
            _minVelocity,
            _maxVelocity,
            _damp;
        
        private Vector3 _direction;
        private float _startDistance, _velocity, _axeleration;
        
        public int Target { get; private set; }
        public Vector3 Position { get; private set; }

        public bool IsSucked => _microGarbage.IsSucked;
        public bool IsMove => _velocity > _minVelocity;
        
        public void Move()
        {
            Position += _direction * _velocity;
            _transform.position = Position;
        }

        public void SetTarget(int playerEntity, Vector3 targetPosition)
        {
            Target = playerEntity;
            if (_velocity > _minVelocity)
                return;
            
            _direction = targetPosition - Position;
            _startDistance = _direction.magnitude;
            _direction.Normalize();
            _velocity = 0f;
        }
        
        public void CalculateDirection(Vector3 targetPosition)
        {
            var direction = targetPosition - Position;
            var distance = direction.magnitude;
            direction.Normalize();
            var distanceRatio = 1f - distance / _startDistance;
            var rotationRatio = Mathf.Lerp(_minRotationRatio,
                _maxRotationRatio, distanceRatio) * 2f;
            _direction = Vector3.Lerp(_direction, direction, rotationRatio);
        }

        public void CalculateVelocity(float deltaTime)
        {
            if (Target > -1)
            {
                _velocity += _axeleration * deltaTime;
            }
            else
            {
                _velocity -= _damp * deltaTime;
            }

            _velocity = Mathf.Clamp(_velocity, 0f, _maxVelocity);
        }
    }
}