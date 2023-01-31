using System.Collections.Generic;
using System.Linq;
using EcsMicroTrash.Components;
using Leopotam.EcsLite;
using Trash;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Jobs;
using Debug = UnityEngine.Debug;

namespace EcsMicroTrash.Systems
{
    public class StaticTriggerSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private EcsWorld _world;
        private NativeArray<StaticGarbage.Data> _triggers;
        private TransformAccessArray _transformAccessArray;
        private EcsPool<StaticGarbageComponent> _staticGarbagePool;
        private EcsFilter _staticGarbageFilter;
        private EcsPool<Components.Vacuum> _vacuumPool;
        private EcsFilter _playerFilter;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _vacuumPool = _world.GetPool<Components.Vacuum>();
            _playerFilter = _world.Filter<Components.Vacuum>().End();
            
            Setup();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var playerEntity in _playerFilter)
            {
                ref var vacuum = ref _vacuumPool.Get(playerEntity);
                CheckTriggerEnter(ref vacuum);
            }
        }

        public void Destroy(IEcsSystems systems)
        {
            _triggers.Dispose();
            _transformAccessArray.Dispose();
        }
        
        private void CheckTriggerEnter(ref Components.Vacuum vacuum)
        {
            var count = _triggers.Length;
            var sucked = new NativeArray<bool>(count, Allocator.TempJob);
            var triggerEnterJob = new StaticGarbageTriggerJob
            {
                VacuumRadius = vacuum.Radius,
                VacuumPosition = vacuum.Transform.position,
                Triggers = _triggers,
                Sucked = sucked
            };

            var triggerEnterHandle = 
                triggerEnterJob.Schedule(_transformAccessArray);
            triggerEnterHandle.Complete();

            for (int i = 0; i < count; i++)
            {
                if (sucked[i])
                {
                    int entity = _triggers[i].Entity;
                    ref var staticGarbageComponent = ref _staticGarbagePool.Get(entity);
                    staticGarbageComponent.StaticGarbage.Suck(999);
                }
            }

            sucked.Dispose();
        }

        private void Setup()
        {
            _staticGarbagePool = _world.GetPool<StaticGarbageComponent>();
            _staticGarbageFilter = _world.Filter<StaticGarbageComponent>().End();
            var count = _staticGarbageFilter.GetEntitiesCount();
            var transforms = new Transform[count];
            _triggers = new NativeArray<StaticGarbage.Data>(count,
                Allocator.Persistent);
            var i = 0;
            foreach (var entity in _staticGarbageFilter)
            {
                ref var staticGarbageComponent = ref _staticGarbagePool.Get(entity);
                _triggers[i] =
                    new StaticGarbage.Data(staticGarbageComponent.StaticGarbage,
                        entity);
                transforms[i] = staticGarbageComponent.StaticGarbage.transform;
                i++;
            }

            _transformAccessArray = new TransformAccessArray(transforms);
        }
    }
}