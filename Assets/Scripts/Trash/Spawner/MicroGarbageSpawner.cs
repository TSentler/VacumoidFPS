using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Trash
{
    public class MicroGarbageSpawner : MonoBehaviour
    {
        [SerializeField] private List<BoxCollider> _boxColliders;
        [SerializeField] private GameObject _garbage;
        [SerializeField, Range(0f, 1.5f)] private float _distance, _offset;

        private void OnValidate()
        {
            if (_garbage.TryGetComponent(
                    out MicroGarbageStaticTrigger garbageTriger) == false)
            {
                Debug.LogError("MicroGarbageStaticTrigger prefab not to found", this);
            }
            if (_boxColliders.Count == 0)
            {
                Debug.LogWarning("BoxCollider list is empty!", this);
            }
        }

        private List<MicroGarbageStaticTrigger> SpawnAsGrid(Vector3 startPosition, 
            float rows, float columns, Transform parent)
        {
            List<MicroGarbageStaticTrigger> trash = new();
            var rowPosition = startPosition;
            for (int i = 0; i < rows; i++)
            {
                rowPosition -= Vector3.forward * _distance;
                for (int j = 0; j < columns; j++)
                {
                    rowPosition += Vector3.right * _distance;
                    trash.Add(Spawn(rowPosition, parent));
                }
                rowPosition = new Vector3(startPosition.x, 
                    rowPosition.y, rowPosition.z);
            }

            return trash;
        }

        private MicroGarbageStaticTrigger Spawn(Vector3 position, Transform parent)
        {
            var garbage = Instantiate(_garbage).transform;
            garbage.parent = parent;
            garbage.localPosition =
                position
                + Vector3.right * Random.Range(-_offset, _offset)
                + Vector3.forward * Random.Range(-_offset, _offset);
            return garbage.GetComponent<MicroGarbageStaticTrigger>();
        }

        public List<MicroGarbageStaticTrigger> SpawnInsideAllColliders()
        {
            List<MicroGarbageStaticTrigger> trash = new();
            foreach (var box in _boxColliders)
            {
                var startPosition = new Vector3(
                    -box.size.x / 2,
                    box.center.y,
                    box.size.z / 2);

                var columns = box.size.x / _distance - 1f;
                var rows = box.size.z / _distance - 1f;
                trash.AddRange(SpawnAsGrid(startPosition, rows, columns,
                    box.transform));
            }

            return trash;
        }
    }
}
