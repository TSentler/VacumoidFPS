using UnityEngine;

namespace Suckables
{
    public interface ISuckCenter
    {
        public float ExtraSpeedMultiply { get; }
        
        public Vector3 GetPosition();
        public Transform GetTransform();
    }
}