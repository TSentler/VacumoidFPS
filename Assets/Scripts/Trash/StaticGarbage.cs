using Unity.Mathematics;
using UnityEngine;

namespace Trash
{
    public class StaticGarbage : MonoBehaviour
    {
        [SerializeField] private MicroGarbage _garbage;

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
                Position = staticGarbage.transform.position;
            }
            
            public MicroGarbage Garbage { get; }
            public float3 Position { get; }
        }
    }
}