using EcsMicroTrash.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsMicroTrash.Systems
{
    public class UpdateDataSystem : IEcsRunSystem
    {
        readonly EcsWorldInject _world = default;
        readonly EcsPoolInject<VacuumComponent> _vacuumPool = default;
        readonly EcsFilterInject<Inc<VacuumComponent>> _vacuumFilter = default; 
        readonly EcsPoolInject<MicroGarbageComponent> _microPool = default;
        readonly EcsFilterInject<Inc<MicroGarbageComponent>> _microFilter = default;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var vacuumEntity in _vacuumFilter.Value)
            {
                ref var vacuum = ref _vacuumPool.Value.Get(vacuumEntity);
                vacuum.UpdateData();
            }

            foreach (var garbageEntity in _microFilter.Value)
            {
                ref var garbage = ref _microPool.Value.Get(garbageEntity);
                if (garbage.IsSucked)
                {
                    _microPool.Value.Del(garbageEntity);
                }
            }
        }
    }
}