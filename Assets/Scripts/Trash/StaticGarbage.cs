using System;
using Suckables;
using UnityEngine;

namespace Trash
{
    public class StaticGarbage : MonoBehaviour
    {
        [SerializeField] private MicroGarbage _garbage;
        [SerializeField] private float _radius = 0.06f;
        [SerializeField] private int _index;
        public int IndexInit;

        private void OnValidate()
        {
            if (_garbage == null)
            {
                Debug.LogError("MicroGarbage not to found", this);
            }
        }

        public void Suck(int index)
        {
            _index = index;
            if (gameObject.activeSelf == false)
                return;
            
            
            _garbage.transform.parent = transform.parent;
            _garbage.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        public void SetCount(float oneGarbageCount)
        {
            _garbage.SetCount(oneGarbageCount);
        }

        public struct Data
        {
            public Data(StaticGarbage staticGarbage, int entity)
            {
                Radius = staticGarbage._radius;
                IsSucked = staticGarbage.enabled == false;
                Entity = entity;
            }
            
            public int Entity { get; }
            public float Radius { get; }
            public bool IsSucked { get; private set; }

            public void Suck() => IsSucked = true;
        }
    }
}