using System.Collections.Generic;
using EcsMicroTrash.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.Mathematics;

namespace EcsMicroTrash.Systems
{
    public class StaticTriggerSystem : IEcsInitSystem, IEcsRunSystem
    {
        public StaticTriggerSystem(float garbageRadius)
        {
            _garbageRadius = garbageRadius;
        }
        
        private readonly EcsPoolInject<MicroGarbageComponent> _microPool = default;
        private readonly float _garbageRadius;
        
        private EcsWorld _world;
        private EcsPool<VacuumComponent> _vacuumPool;

        private StaticGarbageComponent[] _staticGarbageComponents;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _vacuumPool = _world.GetPool<VacuumComponent>();
            
            var staticGarbagePool = _world.GetPool<StaticGarbageComponent>();
            var staticGarbageFilter = _world.Filter<StaticGarbageComponent>().End();
            var staticComponents = new List<StaticGarbageComponent>();
            foreach (var staticGarbageEntity in staticGarbageFilter)
            {
                ref var staticGarbageComponent =
                    ref staticGarbagePool.Get(staticGarbageEntity);
                staticComponents.Add(staticGarbageComponent);
            }

            _staticGarbageComponents = staticComponents.ToArray();
        }
        
        public void Run(IEcsSystems systems)
        {
            var playerEntity = 0;
            ref var vacuum = ref _vacuumPool.Get(playerEntity);
            var vacuumPosition = vacuum.Position;
            var sqrMinDistance = math.pow(_garbageRadius + vacuum.Radius, 2);
            for (int i = 0; i < _staticGarbageComponents.Length; i++)
            {
                ref var staticGarbageComponent = ref _staticGarbageComponents[i];
                if (staticGarbageComponent.IsSucked)
                    continue;
                
                var sqrDistance = math.lengthsq(vacuumPosition - staticGarbageComponent.Position);
                if (sqrDistance <= sqrMinDistance)
                {
                    staticGarbageComponent.Suck();
                    InitializeMicroGarbageEntity(staticGarbageComponent);
                }
            }
        }

        private void InitializeMicroGarbageEntity(
            StaticGarbageComponent staticGarbageComponent)
        {
            var microGarbage = _world.NewEntity();
            ref var microGarbageComponent = ref _microPool.Value.Add(microGarbage);
            microGarbageComponent = new MicroGarbageComponent(
                staticGarbageComponent.Garbage);
            staticGarbageComponent.Garbage.Show();
        }
    }
}