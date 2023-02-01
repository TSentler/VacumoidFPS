using EcsMicroTrash.StaticData;
using Trash;
using Unity.Mathematics;
using UnityEngine;

namespace EcsMicroTrash.Components
{
    public struct MicroGarbageComponent
    {
        public MicroGarbageComponent(MicroGarbage microGarbage, MicroGarbageStaticData staticData)
        {
            _microGarbage = microGarbage;
            _transform = microGarbage.transform;
            _staticData = staticData;
            
            Position = _transform.position;
            Target = -1;
            _direction = default;
            _startDistance = 0f;
            _velocity = 0f;
            _axeleration = 0.5f;
        }
        
        private readonly MicroGarbageStaticData _staticData;
        private readonly MicroGarbage _microGarbage;
        private readonly Transform _transform;

        private float3 _direction;
        private float _startDistance, _velocity, _axeleration;
        
        public int Target { get; private set; }
        public float3 Position { get; private set; }

        public bool IsSucked => _microGarbage.IsSucked;
        public bool IsMove => _velocity > _staticData.MinVelocity;
        
        public void Move()
        {
            Position += _direction * _velocity;
            _transform.position = Position;
        }

        public static void SetTarget(ref MicroGarbageComponent garbage, 
            int playerEntity, float3 targetPosition)
        {
            garbage.Target = playerEntity;
            if (garbage._velocity > garbage._staticData.MinVelocity)
                return;
            
            garbage._direction = targetPosition - garbage.Position;
            garbage._startDistance = math.length(garbage._direction);
            garbage._direction = math.normalize(garbage._direction);
            garbage._velocity = 0f;
        }
        
        public static void CalculateDirection(ref MicroGarbageComponent garbage,
            float3 targetPosition)
        {
            var direction = targetPosition - garbage.Position;
            var distance = math.length(direction);
            direction = math.normalize(direction);
            var distanceRatio = 1f - distance / garbage._startDistance;
            var rotationRatio = Mathf.Lerp(garbage._staticData.MinRotationRatio,
                garbage._staticData.MaxRotationRatio, distanceRatio) * 2f;
            garbage._direction = math.lerp(garbage._direction, direction, rotationRatio);
        }

        public static void CalculateVelocity(ref MicroGarbageComponent garbage,
            float deltaTime)
        {
            if (garbage.Target > -1)
            {
                garbage._velocity += garbage._axeleration * deltaTime;
            }
            else
            {
                garbage._velocity -= garbage._staticData.Damp * deltaTime;
            }

            garbage._velocity = Mathf.Clamp(garbage._velocity, 0f, 
                garbage._staticData.MaxVelocity);
        }
    }
}