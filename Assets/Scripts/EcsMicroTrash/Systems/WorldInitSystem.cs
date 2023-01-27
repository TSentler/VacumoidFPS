using EcsMicroTrash.Components;
using Leopotam.EcsLite;
using Trash;
using UnityEngine;
using Vacuum;

namespace EcsMicroTrash.Systems
{
    public class WorldInitSystem : IEcsInitSystem
    {
        public WorldInitSystem(VacuumRadius vacuumRadius)
        {
            _vacuumRadius = vacuumRadius;
        }
        
        private readonly VacuumRadius _vacuumRadius;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var player = world.NewEntity();
            ref var vacuum = ref world.GetPool<Components.Vacuum>().Add(player);
            vacuum.Radius = _vacuumRadius.GetComponent<SphereCollider>().radius;
            vacuum.Transform = _vacuumRadius.transform;
            
            var microGarbageStatics =
                GameObject.FindObjectsOfType<MicroGarbageStaticTrigger>();
            for (int i = 0; i < microGarbageStatics.Length; i++)
            {
                var staticGarbage = world.NewEntity();
                var triggerPool = world.GetPool<Trigger>();
                ref Trigger trigger = ref triggerPool.Add(staticGarbage);
                trigger.StaticTrigger = microGarbageStatics[i];
            }
        }
    }
}