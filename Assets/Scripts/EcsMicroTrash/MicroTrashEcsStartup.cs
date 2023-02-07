using EcsMicroTrash.StaticData;
using EcsMicroTrash.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Trash;
using UnityEngine;
using Vacuum;

namespace EcsMicroTrash 
{
    public sealed class MicroTrashEcsStartup : MonoBehaviour
    {
        [SerializeField] private MicroGarbageStaticData _microGarbageStaticData;
        
        private EcsWorld _world;
        private IEcsSystems _systems;

        public void Start ()
        {
            var vacuumRadius = FindObjectOfType<VacuumRadius>();
            var staticTriggerSystem = new StaticTriggerSystem(
                _microGarbageStaticData.Radius);
            var moveMicroGarbageSystem = 
                new MoveMicroGarbageSystem(_microGarbageStaticData);
            var staticMicroGarbages = FindObjectsOfType<StaticGarbage>();
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _systems
                .Add(new UpdateDataSystem())
                .Add(new WorldInitSystem(vacuumRadius, staticMicroGarbages))
                .Add(staticTriggerSystem)
                .Add(moveMicroGarbageSystem)
#if UNITY_EDITOR
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Inject()
                .Init ();
        }

        public void Update() 
        {
            _systems.Run();
        }

        public void OnDestroy() 
        {
            if (_systems != null) 
            {
                _systems.Destroy();
                _systems = null;
            }
            
            if (_world != null) 
            {
                _world.Destroy();
                _world = null;
            }
        }
    }
}