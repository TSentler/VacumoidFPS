using EcsMicroTrash.Systems;
using Leopotam.EcsLite;
using UnityEngine;
using Vacuum;

namespace EcsMicroTrash 
{
    public sealed class MicroTrashEcsStartup : MonoBehaviour
    {
        //TODO: find _suckerTransform by VacuumRadius
        [SerializeField] private VacuumRadius _vacuumRadius;
        
        private EcsWorld _world;
        private IEcsSystems _systems;

        public void Start () 
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _systems
                .Add(new WorldInitSystem(_vacuumRadius))
                .Add(new StaticTriggerSystem())
#if UNITY_EDITOR
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Init ();
        }

        public void Update() 
        {
            _systems?.Run();
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