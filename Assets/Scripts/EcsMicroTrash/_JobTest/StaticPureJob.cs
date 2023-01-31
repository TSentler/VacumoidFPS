using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Trash;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Jobs;
using Vacuum;

namespace EcsMicroTrash
{
    /*
    public class StaticPureJob : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private NativeArray<StaticGarbage.Data> _triggers;
        private TransformAccessArray _transformAccessArray;
        private StaticGarbage[] _staticMicroGarbages;
        private VacuumRadius _vacuumRadius;

        private int Count => _staticMicroGarbages.Length;
    
        private void Start()
        {
            _vacuumRadius = FindObjectOfType<VacuumRadius>();
            _staticMicroGarbages = FindObjectsOfType<StaticGarbage>();
            _triggers = new NativeArray<StaticGarbage.Data>(Count,
                Allocator.Persistent);
            var transforms = new Transform[Count];
            for (int i = 0; i < Count; i++)
            {
                _triggers[i] =
                    new StaticGarbage.Data(_staticMicroGarbages[i], 0);
                _staticMicroGarbages[i].IndexInit = i;
                transforms[i] = _staticMicroGarbages[i].transform;
            }

            _transformAccessArray = new TransformAccessArray(transforms);
        }

        private void Update()
        {
            var sw = new Stopwatch();
            sw.Start();
            var sucked = new NativeArray<bool>(Count, Allocator.TempJob);
            var triggerEnterJob = new StaticGarbageTriggerJob
            {
                VacuumRadius = 1.5f,
                VacuumPosition = _vacuumRadius.transform.position,
                Triggers = _triggers,
                Sucked = sucked
            };

            var triggerEnterHandle = 
                triggerEnterJob.Schedule(_transformAccessArray);
            triggerEnterHandle.Complete();

            for (int i = 0; i < Count; i++)
            {
                if (sucked[i])
                {
                    _staticMicroGarbages[i].Suck(i);
                }
            }

            sucked.Dispose();
            sw.Stop();
            _text.SetText(sw.Elapsed.Ticks.ToString("F4"));
        }

        private void OnDisable()
        {
            _triggers.Dispose();
            _transformAccessArray.Dispose();
        }
    }
    */
}
