using UnityEngine;

namespace PlayerCamera
{
    [RequireComponent(typeof(Camera))]
    public class Zoom : MonoBehaviour
    {
        [Min(0.0001f),SerializeField] private float _minZoom = 70f,
            _maxZoom = 40f,
            _horizontalLimiter = 7f,
            _verticalLimiter = 20f;
        [Min(0f), SerializeField] private float _maxExtraZoomByAspect = 40f;

        private Camera _camera;
        private float _extraZoom;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }

        private float ZoomByAspect(float aspectRatio)
        {
            var extraZoom = 0f;
            if (aspectRatio < 1f)
            {
                var reverseRatio = 1f - aspectRatio;
                extraZoom = Mathf.Clamp(reverseRatio, 0f, 0.5f) 
                            * _maxExtraZoomByAspect;
            }

            return extraZoom;
        }

        public void Apply(Vector2 greatestDistance)
        {
            var ratioX = greatestDistance.x / _horizontalLimiter;
            var ratioY = greatestDistance.y / _verticalLimiter;
            var ratio = Mathf.Max(ratioX, ratioY);
            
            var targetZoom = Mathf.Lerp(_maxZoom, _minZoom, ratio)
                             + ZoomByAspect(_camera.aspect);
            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView,
                targetZoom, Time.deltaTime);
        }
    }
}
