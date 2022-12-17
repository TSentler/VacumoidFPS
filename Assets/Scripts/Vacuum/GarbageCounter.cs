using System.Collections.Generic;
using System.Linq;
using Trash;
using UnityEngine;
using UnityEngine.Events;

namespace Vacuum
{
    public class GarbageCounter : MonoBehaviour
    {
        private GarbageRoot[] _garbageRoots;
        private float _targetTrashPoints = 0f;
        
        public int TargetTrashPoints => Mathf.RoundToInt(_targetTrashPoints);
        
        public event UnityAction TargetTrashPointsChanged;
        
        private void Awake()
        {
            _garbageRoots = FindObjectsOfType<GarbageRoot>();
            var targetTrashPoints = 0f;
            foreach (var root in _garbageRoots)
            {
                var childTrash = 
                    root.GetComponentsInChildren<Garbage>();
                if (childTrash.Length != 0)
                {
                    var trash = childTrash.ToList();
                    foreach (var garbage in trash)
                    {
                        targetTrashPoints += garbage.TrashPoints;
                    }
                }
            }
            _targetTrashPoints += targetTrashPoints;
            TargetTrashPointsChanged?.Invoke();
        }
    }
}
