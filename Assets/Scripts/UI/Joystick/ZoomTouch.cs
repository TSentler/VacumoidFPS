using UnityEngine;
using UnityEngine.Events;

namespace UI.Joystick
{
    [RequireComponent(typeof(TwoPointersHandler))]
    public class ZoomTouch : MonoBehaviour
    {
        private readonly float _minMove = 0.001f, _minDistance = 0.1f;

        
        private TwoPointersHandler _pointerHandler;
        private float _previousDistance, _currentDistance;
        private float _sensitivity = 4f;

        public event UnityAction Outed;
        public event UnityAction<float> Downed, Moved;

        public bool IsTouch => _pointerHandler.IsTouch;
        
        private void Awake()
        {
            _pointerHandler = GetComponent<TwoPointersHandler>();
        }

        private void OnEnable()
        {
            _pointerHandler.PointerOuted += OnPointerOuted;
            _pointerHandler.PointersDowned += OnPointersDowned;
            _pointerHandler.PointerMoved += OnPointerMoved;
        }

        private void OnDisable()
        {
            _pointerHandler.PointerOuted -= OnPointerOuted;
            _pointerHandler.PointersDowned -= OnPointersDowned;
            _pointerHandler.PointerMoved -= OnPointerMoved;
        }

        private void OnPointerOuted()
        {
            Outed?.Invoke();
        }

        private void OnPointersDowned(float distance)
        {
            _previousDistance = _currentDistance = distance;
            Downed?.Invoke(_currentDistance);
        }

        private void OnPointerMoved(float distance)
        {
            if (distance < _minDistance)
            {
                distance = 0f;
            }
            
            _previousDistance = _currentDistance;
            _currentDistance = distance;
            var moveDistance = CalculateMoveDistance(_currentDistance, 
                _previousDistance);
            Moved?.Invoke(moveDistance);
        }

        private float CalculateMoveDistance(float currentDistance, 
            float previousDistance)
        {
            var diff = (currentDistance - previousDistance) * 
                _sensitivity / Screen.dpi;
            if (Mathf.Abs(diff) < _minMove)
                diff = 0f;
            
            return diff;
        }

    }
}
