using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EcsMicroTrash.StaticData;
using EcsMicroTrash.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using TMPro;
using Trash;
using UnityEngine;
using Vacuum;
using Debug = UnityEngine.Debug;

namespace EcsMicroTrash 
{
    public sealed class MicroTrashEcsStartup : MonoBehaviour
    {
        //TODO: find _suckerTransform by VacuumRadius
        [SerializeField] private VacuumRadius _vacuumRadius;
        [SerializeField] private MicroGarbageStaticData _microGarbageStaticData;
        
        private EcsWorld _world;
        private IEcsSystems _systems;

        public void Start ()
        {
            var staticTriggerSystem = new StaticTriggerSystem(
                _microGarbageStaticData.Radius);
            var moveMicroGarbageSystem = 
                new MoveMicroGarbageSystem(_microGarbageStaticData);
            var staticMicroGarbages = FindObjectsOfType<StaticGarbage>();
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _systems
                .Add(new UpdateDataSystem())
                .Add(new WorldInitSystem(_vacuumRadius, staticMicroGarbages))
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