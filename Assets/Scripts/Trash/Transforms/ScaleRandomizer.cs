using UnityEngine;
using Random = UnityEngine.Random;

namespace Trash
{
    public class ScaleRandomizer : MonoBehaviour
    {
        [SerializeField] private float _minSize, _maxSize;
        [Header("Axises")] 
        [SerializeField] private bool _x;
        [SerializeField] private bool _y, _z;

        public Vector3 GenerateScale()
        {
            var size = Random.Range(_minSize, _maxSize);
            var scale = new Vector3(
                (_x ? size : 1f) * transform.localScale.x,
                (_y ? size : 1f) * transform.localScale.y,
                (_z ? size : 1f) * transform.localScale.z);
            return scale;
        }
    }
}
