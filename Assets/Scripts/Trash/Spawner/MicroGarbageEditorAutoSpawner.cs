using System.Collections.Generic;
using UnityEngine;

namespace Trash
{
    [RequireComponent(typeof(ScaleRandomizer), 
        typeof(RotationRandomizer))]
    public class MicroGarbageEditorAutoSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _microGarbageRoot;
        [SerializeField] private MicroGarbageSpawner _spawner;
        [Min(1), SerializeField] private int _countMicroGarbage = 1;

        private ScaleRandomizer _scaleRandomizer;
        private RotationRandomizer _rotationRandomizer;

        private void OnValidate()
        {
            if (_microGarbageRoot == null)
                Debug.LogWarning("MicroGarbageRoot was not found!", this);
            if (_spawner == null)
                Debug.LogWarning("MicroGarbageSpawner was not found!", this);
        }

        public void SpawnButton()
        {
            _scaleRandomizer = GetComponent<ScaleRandomizer>();
            _rotationRandomizer = GetComponent<RotationRandomizer>();

            List<MicroGarbageStaticTrigger> trash = _spawner.SpawnInsideAllColliders();
            
            var oneGarbageCount = (float)_countMicroGarbage / trash.Count;
            foreach (var garbage in trash)
            {
                garbage.transform.localScale = 
                    _scaleRandomizer.GenerateScale();
                garbage.transform.localRotation = 
                    _rotationRandomizer.GenerateRotation();
                garbage.SetCount(oneGarbageCount);
            }
        }        
        
        public void ClearButton()
        {
            var trash = 
                _microGarbageRoot.GetComponentsInChildren<MicroGarbageStaticTrigger>();
            foreach (var garbage in trash)
            {
                DestroyImmediate(garbage.gameObject);
            }
        }
    }
}
