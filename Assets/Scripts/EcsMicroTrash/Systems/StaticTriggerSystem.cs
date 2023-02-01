using System.Collections.Generic;
using EcsMicroTrash.Components;
using EcsMicroTrash.StaticData;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.Mathematics;

namespace EcsMicroTrash.Systems
{
    public class StaticTriggerSystem : IEcsInitSystem, IEcsRunSystem
    {
        public StaticTriggerSystem(float garbageRadius, MicroGarbageStaticData staticData)
        {
            _garbageRadius = garbageRadius;
            _staticData = staticData;
        }
        
        readonly EcsPoolInject<MicroGarbageComponent> _microPool = default;
        
        private EcsWorld _world;
        private EcsPool<StaticGarbageComponent> _staticGarbagePool;
        private EcsFilter _staticGarbageFilter;
        private EcsPool<VacuumComponent> _vacuumPool;
        private EcsFilter _playerFilter;
        private float _garbageRadius;
        private MicroGarbageStaticData _staticData;

        private StaticGarbageComponent[] _staticGarbageComponents;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _vacuumPool = _world.GetPool<VacuumComponent>();
            _playerFilter = _world.Filter<VacuumComponent>().End();
            
            _staticGarbagePool = _world.GetPool<StaticGarbageComponent>();
            _staticGarbageFilter = _world.Filter<StaticGarbageComponent>().End();
            var playerEntity = 0;
            ref var vacuum = ref _vacuumPool.Get(playerEntity);
            var vacuumPosition = vacuum.Position;
            var sqrMinDistance = math.pow(_garbageRadius + vacuum.Radius, 2);
            var staticComponents = new List<StaticGarbageComponent>();
            foreach (var staticGarbageEntity in _staticGarbageFilter)
            {
                ref var staticGarbageComponent =
                    ref _staticGarbagePool.Get(staticGarbageEntity);
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
            // foreach (var staticGarbageEntity in _staticGarbageFilter)
            for (int i = 0; i < _staticGarbageComponents.Length; i++)
            {
                // ref var staticGarbageComponent = ref _staticGarbagePool.Get(staticGarbageEntity);
                ref var staticGarbageComponent = ref _staticGarbageComponents[i];
                if (staticGarbageComponent.IsSucked)
                    continue;
                
                var sqrDistance = math.lengthsq(vacuumPosition - staticGarbageComponent.Position);
                if (sqrDistance <= sqrMinDistance)
                {
                    staticGarbageComponent.Suck();
                    var microGarbage = _world.NewEntity();
                    ref var microGarbageComponent = ref _microPool.Value.Add(microGarbage);
                    microGarbageComponent = new MicroGarbageComponent(
                        staticGarbageComponent.Garbage,
                        _staticData);
                    staticGarbageComponent.Garbage.Show();
                    // _staticGarbagePool.Del(staticGarbageEntity);
                }
            }
        }
    }
}