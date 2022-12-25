using UnityEngine;
using UnityEngine.Events;

namespace UI.Joystick
{
    [RequireComponent(typeof(PointerHandler))]
    public class StickPointer : MonoBehaviour, ITouchable
    {
        private readonly float _deadZone = 0.05f;

        [SerializeField] private RectTransform _stickRect;

        private PointerHandler _pointerHandler;
        private Vector2 _startTouch, _currentTouch, _stickVector;

        public event UnityAction Outed;
        public event UnityAction<Vector2> Downed, Moved;

        public bool IsTouch => _pointerHandler.IsTouch;
        
        private void OnValidate()
        {
            if (_stickRect == null)
                Debug.LogWarning("RectTransform was not found!", this);
        }

        private void Awake()
        {
            _pointerHandler = GetComponent<PointerHandler>();
        }

        private void OnEnable()
        {
            _pointerHandler.PointerOuted += OnPointerOuted;
            _pointerHandler.PointerDowned += OnPointerDowned;
            _pointerHandler.PointerMoved += OnPointerMoved;
        }

        private void OnDisable()
        {
            _pointerHandler.PointerOuted -= OnPointerOuted;
            _pointerHandler.PointerDowned -= OnPointerDowned;
            _pointerHandler.PointerMoved -= OnPointerMoved;
        }

        private void OnPointerOuted()
        {
            Outed?.Invoke();
        }

        private void OnPointerDowned(Vector2 position)
        {
            _startTouch = position;
            Downed?.Invoke(_startTouch);
        }

        private void OnPointerMoved(Vector2 position)
        {
            _stickVector = CalculateStickVector(_startTouch,
                position);

            Moved?.Invoke(_stickVector);
        }

        private Vector2 CalculateStickVector(Vector2 position,
            Vector2 pressPosition)
        {
            var stickVector = pressPosition - position;
            stickVector /= _stickRect.lossyScale;
            var radius = _stickRect.rect.height / 2;
            stickVector /= radius;
            if (stickVector.magnitude < _deadZone)
                stickVector = Vector2.zero;
            else if (stickVector.magnitude > 1f)
                stickVector.Normalize();

            return stickVector;
        }

    }
}
