using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EcsMicroTrash.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Trash;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Jobs;
using Debug = UnityEngine.Debug;

namespace EcsMicroTrash.Systems
{
    public class StaticTriggerSystem : IEcsInitSystem, IEcsRunSystem
    {
        public StaticTriggerSystem(float garbageRadius)
        {
            _garbageRadius = garbageRadius;
        }
        
        readonly EcsPoolInject<MicroGarbageComponent> _microPool = default;
        
        private EcsWorld _world;
        private EcsPool<StaticGarbageComponent> _staticGarbagePool;
        private EcsFilter _staticGarbageFilter;
        private EcsPool<VacuumComponent> _vacuumPool;
        private EcsFilter _playerFilter;
        private float _garbageRadius;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _vacuumPool = _world.GetPool<VacuumComponent>();
            _playerFilter = _world.Filter<VacuumComponent>().End();
            
            _staticGarbagePool = _world.GetPool<StaticGarbageComponent>();
            _staticGarbageFilter = _world.Filter<StaticGarbageComponent>().End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var playerEntity in _playerFilter)
            {
                ref var vacuum = ref _vacuumPool.Get(playerEntity);
                var vacuumPosition = vacuum.Position;
                var sqrMinDistance = Mathf.Pow(_garbageRadius + vacuum.Radius, 2);
                var anySucked = false;
                foreach (var staticGarbageEntity in _staticGarbageFilter)
                {
                    ref var staticGarbageComponent =
                        ref _staticGarbagePool.Get(staticGarbageEntity);
                    if (staticGarbageComponent.IsSucked)
                        continue;
                    
                    var distance = (vacuumPosition - staticGarbageComponent.Data.Position).sqrMagnitude;
                    if (distance <= sqrMinDistance)
                    {
                        staticGarbageComponent.Suck();
                        var microGarbage = _world.NewEntity();
                        ref var microGarbageComponent = ref _microPool.Value.Add(microGarbage);
                        microGarbageComponent = new MicroGarbageComponent(staticGarbageComponent.Data.Garbage);
                        _staticGarbagePool.Del(staticGarbageEntity);
                    }
                }
            }
        }
    }
}