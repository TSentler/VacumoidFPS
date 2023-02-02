using EcsMicroTrash.Components;
using EcsMicroTrash.StaticData;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.Mathematics;
using UnityEngine;

namespace EcsMicroTrash.Systems
{
    public class MoveMicroGarbageSystem : IEcsRunSystem
    {
        public MoveMicroGarbageSystem(MicroGarbageStaticData staticData)
        {
            _staticData = staticData;
        }
        
        private readonly MicroGarbageStaticData _staticData;
        private readonly EcsWorldInject _world = default;
        private readonly EcsPoolInject<VacuumComponent> _vacuumPool = default;
        private readonly EcsFilterInject<Inc<VacuumComponent>> _vacuumFilter = default; 
        private readonly EcsPoolInject<MicroGarbageComponent> _microPool = default;
        private readonly EcsFilterInject<Inc<MicroGarbageComponent>> _microFilter = default;
        
        public void Run(IEcsSystems systems)
        {
            var deltaTime = Time.deltaTime;
            int playerEntity = 0;
            VacuumComponent vacuumComponent = _vacuumPool.Value.Get(playerEntity);

            foreach (var garbageEntity in _microFilter.Value)
            {
                ref var garbage = ref _microPool.Value.Get(garbageEntity);
                
                if (garbage.IsSucked)
                {
                    _microPool.Value.Del(garbageEntity);
                    continue;
                }
                
                if (garbage.Target == -1 || garbage.Target == playerEntity)
                {
                    var vacuumPosition = vacuumComponent.Position;
                    var sqrMinDistance =
                        math.pow(_staticData.Radius + vacuumComponent.Radius, 2);
                    var sqrDistance = math.lengthsq(vacuumPosition - garbage.Position);
                    if (garbage.Target == -1 && sqrDistance <= sqrMinDistance)
                    {
                        SetTarget(ref garbage, playerEntity, vacuumPosition);
                    }
                    else if (garbage.Target == playerEntity && sqrDistance > sqrMinDistance)
                    {
                        SetTarget(ref garbage, -1, garbage.Position);
                    }
                }
                
                CalculateVelocity(ref garbage, deltaTime);

                if (IsMove(garbage) == false)
                    continue;
                
                if (garbage.Target > -1)
                {
                    ref var vacuum = ref _vacuumPool.Value.Get(garbage.Target);
                    CalculateDirection(ref garbage, vacuum.Position);
                }
                garbage.Move();
            }
        }
        
        private bool IsMove(MicroGarbageComponent garbage) => 
            garbage.Velocity > _staticData.MinVelocity;
        
        private void SetTarget(ref MicroGarbageComponent garbage, 
            int playerEntity, float3 targetPosition)
        {
            garbage.Target = playerEntity;
            if (garbage.Velocity > _staticData.MinVelocity)
                return;
            
            garbage.Direction = targetPosition - garbage.Position;
            garbage.StartDistance = math.length(garbage.Direction);
            garbage.Direction = math.normalize(garbage.Direction);
            garbage.Velocity = 0f;
        }
        
        private void CalculateDirection(ref MicroGarbageComponent garbage,
            float3 targetPosition)
        {
            var direction = targetPosition - garbage.Position;
            var distance = math.length(direction);
            direction = math.normalize(direction);
            var distanceRatio = 1f - distance / garbage.StartDistance;
            var rotationRatio = Mathf.Lerp(_staticData.MinRotationRatio,
                _staticData.MaxRotationRatio, distanceRatio) * 2f;
            garbage.Direction = math.lerp(garbage.Direction, direction, rotationRatio);
        }

        private void CalculateVelocity(ref MicroGarbageComponent garbage,
            float deltaTime)
        {
            if (garbage.Target > -1)
            {
                garbage.Velocity += garbage.Axeleration * deltaTime;
            }
            else
            {
                garbage.Velocity -= _staticData.Damp * deltaTime;
            }

            garbage.Velocity = Mathf.Clamp(garbage.Velocity, 0f, 
                _staticData.MaxVelocity);
        }
    }
}