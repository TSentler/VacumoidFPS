using System;
using System.Collections.Generic;
using EcsMicroTrash.Components;
using Leopotam.EcsLite;
using Unity.Collections;
using Unity.Jobs;
using System.Linq;
using UnityEngine;

namespace EcsMicroTrash.Systems
{
    public struct TriggerEnterJob : IJobParallelFor
    {
        [ReadOnly] public float VacuumRadius;
        [ReadOnly] public Vector3 VacuumPosition;
        [ReadOnly] public NativeArray<Vector3> TriggersPosition;
        [ReadOnly] public NativeArray<float> TriggersRadius;
        
        public NativeArray<bool> TriggersIsSucked;
        
        public void Execute(int index)
        {
            var triggerIsSucked = TriggersIsSucked[index];
            if (triggerIsSucked)
                return;
            
            var triggerPosition = TriggersPosition[index];
            var triggerRadius = TriggersRadius[index];
            
            var minDistance = triggerRadius + VacuumRadius;
            var distance = (VacuumPosition - triggerPosition).magnitude;
            if (distance <= minDistance)
            {
                TriggersIsSucked[index] = true;
            }
        }
    }

    public class StaticTriggerSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private NativeArray<Vector3> _triggersPosition;
        private NativeArray<bool> _triggersIsSucked;
        private NativeArray<float> _triggersRadius;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var triggerPool = world.GetPool<Trigger>();
            var triggerFilter = world.Filter<Trigger>().End();
            var count = triggerFilter.GetEntitiesCount();
            _triggersIsSucked = new NativeArray<bool>(count, Allocator.TempJob);
            _triggersPosition = new NativeArray<Vector3>(count, Allocator.TempJob);
            _triggersRadius = new NativeArray<float>(count, Allocator.TempJob);
            var i = 0;
            foreach (var triggerEntity in triggerFilter)
            {
                var trigger= triggerPool.Get(triggerEntity);
                _triggersIsSucked[i] = trigger.IsSucked;
                _triggersPosition[i] = trigger.StaticTrigger.transform.position;
                _triggersRadius[i] = trigger.Radius;
                i++;
            }
        }
        
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var vacuumPool = world.GetPool<Components.Vacuum>();
            var playerFilter = world.Filter<Components.Vacuum>().End();
            foreach (var playerEntity in playerFilter)
            {
                ref var vacuum = ref vacuumPool.Get(playerEntity);
                var triggerFilter = world.Filter<Trigger>().End();
                var count = triggerFilter.GetEntitiesCount();

                var triggerEnterJob = new TriggerEnterJob
                {
                    VacuumRadius = vacuum.Radius,
                    VacuumPosition = vacuum.Transform.position,
                    TriggersPosition = _triggersPosition,
                    TriggersRadius = _triggersRadius,
                    TriggersIsSucked = _triggersIsSucked
                };

                var triggerEnterHandle = 
                    triggerEnterJob.Schedule(count, 0);
                triggerEnterHandle.Complete();
                
                break;
            }
        }

        public void Destroy(IEcsSystems systems)
        {
            _triggersPosition.Dispose();
            _triggersRadius.Dispose();
            _triggersIsSucked.Dispose();
        }
    }
}