using System.Collections.Generic;
using EcsMicroTrash.Components;
using Leopotam.EcsLite;
using System.Linq;
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
                
                var triggerFilter = world.Filter<Trigger>().End();
                foreach (var entity in triggerFilter)
                {
                    ref var trigger = 
                        ref world.GetPool<Trigger>().Get(entity);

                    var minDistance = trigger.Radius + vacuum.Radius;
                    var distance =
                        (vacuum.Transform.position - trigger.Transform.position)
                        .magnitude;
                    if (distance <= minDistance)
                    {
                        trigger.Transform.gameObject.SetActive(false);
                    }
                }

                break;
            }
            
        }

    }
}