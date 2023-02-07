using EcsMicroTrash.Components;
using Leopotam.EcsLite;
using Trash;
using Vacuum;

namespace EcsMicroTrash.Systems
{
    public class WorldInitSystem : IEcsInitSystem
    {
        public WorldInitSystem(VacuumRadius vacuumRadius, 
            StaticGarbage[] staticMicroGarbages)
        {
            _vacuumRadius = vacuumRadius;
            _staticMicroGarbages = staticMicroGarbages;
        }
        
        private readonly VacuumRadius _vacuumRadius;
        private readonly StaticGarbage[] _staticMicroGarbages;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var player = world.NewEntity();
            ref var vacuum = ref world.GetPool<VacuumComponent>().Add(player);
            vacuum = new VacuumComponent(_vacuumRadius);
            
            var staticGarbagePool = world.GetPool<StaticGarbageComponent>();
            var microGarbagePool = world.GetPool<MicroGarbageComponent>();
            for (int i = 0; i < _staticMicroGarbages.Length; i++)
            {
                var staticGarbage = world.NewEntity();
                ref var staticGarbageComponent = ref staticGarbagePool.Add(staticGarbage);
                staticGarbageComponent = new StaticGarbageComponent(_staticMicroGarbages[i]);
            }
        }
    }
}