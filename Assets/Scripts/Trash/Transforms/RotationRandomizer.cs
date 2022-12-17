using UnityEngine;
using Random = UnityEngine.Random;

namespace Trash
{
    public class RotationRandomizer : MonoBehaviour
    {
        [SerializeField] private float _angleMin, _angleMax = 360f;
        [Header("Axises")] 
        [SerializeField] private bool _x;
        [SerializeField] private bool _y, _z;
    
        public Quaternion GenerateRotation()
        {
            var angle = Random.Range(_angleMin, _angleMax);
            var rotation = Quaternion.Euler(
                _x ? angle : transform.localScale.x,
                _y ? angle : transform.localScale.y,
                _z ? angle : transform.localScale.z);
            return rotation;
        }
    }
}