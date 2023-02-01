using Unity.Mathematics;
using UnityEngine;

namespace Trash
{
    public class StaticGarbage : MonoBehaviour
    {
        [SerializeField] private MicroGarbage _garbage;
        [SerializeField] private float _radius = 0.06f;

        private void OnValidate()
        {
            if (_garbage == null)
            {
                Debug.LogError("MicroGarbage not to found", this);
            }
        }

        public void Suck()
        {
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
            public Data(StaticGarbage staticGarbage)
            {
                Garbage = staticGarbage._garbage;
                Radius = staticGarbage._radius;
                Position = staticGarbage.transform.position;
            }
            
            public MicroGarbage Garbage { get; }
            public float Radius { get; }
            public float3 Position { get; }
        }
    }
}