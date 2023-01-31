using System.Collections.Generic;
using EcsMicroTrash.Components;
using Leopotam.EcsLite;
using Trash;
using UnityEngine;
using Vacuum;

namespace EcsMicroTrash.Systems
{
    public class WorldInitSystem : IEcsInitSystem
    {
        public WorldInitSystem(VacuumRadius vacuumRadius, 
            List<StaticGarbage> staticMicroGarbages)
        {
            _vacuumRadius = vacuumRadius;
            _staticMicroGarbages = staticMicroGarbages;
        }
        
        private readonly VacuumRadius _vacuumRadius;
        private readonly List<StaticGarbage> _staticMicroGarbages;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var player = world.NewEntity();
            ref var vacuum = ref world.GetPool<Components.VacuumComponent>().Add(player);
            vacuum = new VacuumComponent(_vacuumRadius);
            
            var staticGarbagePool = world.GetPool<StaticGarbageComponent>();
            var microGarbagePool = world.GetPool<MicroGarbageComponent>();
            for (int i = 0; i < _staticMicroGarbages.Count; i++)
            {
                var staticGarbage = world.NewEntity();
                ref var staticGarbageComponent = ref staticGarbagePool.Add(staticGarbage);
                staticGarbageComponent = new StaticGarbageComponent(_staticMicroGarbages[i]);
            }
        }
    }
}