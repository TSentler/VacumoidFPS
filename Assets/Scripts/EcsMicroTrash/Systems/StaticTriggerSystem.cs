using System;
using System.Collections.Generic;
using EcsMicroTrash.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace EcsMicroTrash.Systems
{
    public class StaticTriggerSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            
            var vacuumPool = world.GetPool<Components.Vacuum>();
            var playerFilter = world.Filter<Components.Vacuum>().End();
            foreach (var playerEntity in playerFilter)
            {
                ref var vacuum = ref vacuumPool.Get(playerEntity);
                var triggerPool = world.GetPool<Trigger>();
                var triggerFilter = world.Filter<Trigger>().End();
                foreach (var triggerEntity in triggerFilter)
                {
                    CheckTriggerEnter(ref vacuum, triggerEntity, triggerPool);
                }
            }
        }

        private void CheckTriggerEnter(ref Components.Vacuum vacuum , 
            int triggerEntity, EcsPool<Trigger> triggerPool)
        {
            ref var trigger = ref triggerPool.Get(triggerEntity);
            var minDistance = trigger.Radius + vacuum.Radius;
            var distance = (vacuum.Transform.position -
                 trigger.StaticTrigger.transform.position).magnitude;
            if (distance <= minDistance)
            {
                trigger.StaticTrigger.Suck();
                triggerPool.Del(triggerEntity);
            }
        }
    }
}