using EcsMicroTrash.StaticData;
using Trash;
using Unity.Mathematics;
using UnityEngine;

namespace EcsMicroTrash.Components
{
    public struct MicroGarbageComponent
    {
        public MicroGarbageComponent(MicroGarbage microGarbage)
        {
            _microGarbage = microGarbage;
            _transform = microGarbage.transform;
            
            Position = _transform.position;
            Target = -1;
            Direction = default;
            StartDistance = 0f;
            Velocity = 0f;
            Axeleration = 0.5f;
        }
        
        private readonly MicroGarbage _microGarbage;
        private readonly Transform _transform;

        public float3 Direction;
        public float StartDistance, Velocity, Axeleration;
        public int Target;

        public float3 Position { get; private set; }

        public bool IsSucked => _microGarbage.IsSucked;
        
        public void Move()
        {
            Position += Direction * Velocity;
            _transform.position = Position;
        }

    }
}