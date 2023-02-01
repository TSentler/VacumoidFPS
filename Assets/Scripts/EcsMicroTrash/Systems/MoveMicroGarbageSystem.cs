using EcsMicroTrash.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.Mathematics;
using UnityEngine;

namespace EcsMicroTrash.Systems
{
    public class MoveMicroGarbageSystem : IEcsRunSystem
    {
        public MoveMicroGarbageSystem(float garbageRadius)
        {
            _garbageRadius = garbageRadius;
        }
        
        readonly EcsWorldInject _world = default;
        readonly EcsPoolInject<VacuumComponent> _vacuumPool = default;
        readonly EcsFilterInject<Inc<VacuumComponent>> _vacuumFilter = default; 
        readonly EcsPoolInject<MicroGarbageComponent> _microPool = default;
        readonly EcsFilterInject<Inc<MicroGarbageComponent>> _microFilter = default;
        private readonly float _garbageRadius;
        
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
                        math.pow(_garbageRadius + vacuumComponent.Radius, 2);
                    var sqrDistance = math.lengthsq(vacuumPosition - garbage.Position);
                    if (garbage.Target == -1 && sqrDistance <= sqrMinDistance)
                    {
                        MicroGarbageComponent.SetTarget(ref garbage, playerEntity, vacuumPosition);
                    }
                    else if (garbage.Target == playerEntity && sqrDistance > sqrMinDistance)
                    {
                        MicroGarbageComponent.SetTarget(ref garbage, -1, garbage.Position);
                    }
                }
                
                MicroGarbageComponent.CalculateVelocity(ref garbage, deltaTime);

                if (garbage.IsMove == false)
                    continue;
                
                if (garbage.Target > -1)
                {
                    ref var vacuum = ref _vacuumPool.Value.Get(garbage.Target);
                    MicroGarbageComponent.CalculateDirection(ref garbage, vacuum.Position);
                }
                garbage.Move();
            }
        }
    }
}