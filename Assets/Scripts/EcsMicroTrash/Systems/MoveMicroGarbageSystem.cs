using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EcsMicroTrash.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Debug = UnityEngine.Debug;

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
            foreach (var playerEntity in _vacuumFilter.Value)
            {
                ref var vacuumComponent =
                    ref _vacuumPool.Value.Get(playerEntity);
                var vacuumPosition = vacuumComponent.Position;
                var sqrMinDistance =
                    Mathf.Pow(_garbageRadius + vacuumComponent.Radius, 2);
                foreach (var garbageEntity in _microFilter.Value)
                {
                    ref var garbage =
                        ref _microPool.Value.Get(garbageEntity);
                    if (garbage.IsSucked)
                        continue;
                    if (garbage.Target == -1 || garbage.Target == playerEntity)
                    {
                        var distance = (vacuumPosition - garbage.Position).sqrMagnitude;
                        if (garbage.Target == -1 && distance <= sqrMinDistance)
                        {
                            garbage.SetTarget(playerEntity, vacuumPosition);
                        }
                        else if (garbage.Target == playerEntity && distance > sqrMinDistance)
                        {
                            garbage.SetTarget(-1, garbage.Position);
                        }
                    }
                }
            }

            foreach (var garbageEntity in _microFilter.Value)
            {
                ref var garbage = ref _microPool.Value.Get(garbageEntity);
                garbage.CalculateVelocity(deltaTime);

                if (garbage.IsMove == false)
                    continue;
                
                if (garbage.Target > -1)
                {
                    ref var vacuumComponent =
                        ref _vacuumPool.Value.Get(garbage.Target);
                    garbage.CalculateDirection(vacuumComponent.Position);
                }
                garbage.Move();
            }
        }
    }
}