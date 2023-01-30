using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EcsMicroTrash.Systems;
using Leopotam.EcsLite;
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
        [SerializeField] private TMP_Text _text; 
        
        private EcsWorld _world;
        private IEcsSystems _systems;

        public void Start ()
        {
            var staticMicroGarbages = FindObjectsOfType<StaticGarbage>().ToList();
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _systems
                .Add(new WorldInitSystem(_vacuumRadius, staticMicroGarbages))
                .Add(new StaticTriggerSystem())
#if UNITY_EDITOR
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Init ();
        }

        public void Update() 
        {
            var sw = new Stopwatch();
            sw.Start();
            _systems?.Run();
            sw.Stop();
            _text.SetText(sw.Elapsed.Ticks.ToString("F4"));
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