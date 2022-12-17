using UnityEngine;

namespace PlayerCamera
{
    public class Follow : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;
        [Min(0f), SerializeField] private float _smoothTime = 0.3f;
        [SerializeField] private Vector3 _maxOffset;
        [Min(0.0001f), SerializeField] private float _verticalLimiter = 20f;
        
        private Vector3 _velocity;
        
        public void Apply(Vector3 centerPoint, float verticalDistance)
        {
            var ratio = verticalDistance / _verticalLimiter;
            var offsetByDistance = Vector3.Lerp(
                Vector3.zero, _maxOffset, ratio);
            
            Vector3 targetPosition = centerPoint + _offset + offsetByDistance;
            transform.position = Vector3.SmoothDamp(transform.position,
                targetPosition, ref _velocity, _smoothTime);
        }
    }
}
