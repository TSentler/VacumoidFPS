using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI.Joystick
{
    public class StickPointer : MonoBehaviour, IDragHandler,
        IPointerDownHandler, IPointerUpHandler
    {
        private readonly float _deadZone = 0.05f;

        [SerializeField] private RectTransform _stickRect;

        private Vector2 _startTouch, _currentTouch, _stickVector;
        private int _fingerId = int.MinValue;
        private bool _isTouch;
    
        public bool IsTouch => _isTouch;
    
        public event UnityAction FingerOut;
        public event UnityAction<Vector2> FingerDown, FingerMove;

        private void OnValidate()
        {
            if (_stickRect == null)
                Debug.LogWarning("RectTransform was not found!", this);
        }
    
        private Vector2 CalculateStickVector(Vector2 position, Vector2 pressPosition)
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

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_isTouch == false || _fingerId != eventData.pointerId)
                return;

            _isTouch = false;
            FingerOut?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_isTouch)
                return;
            
            _isTouch = true;
            _fingerId = eventData.pointerId;
            _startTouch = eventData.position;
            FingerDown?.Invoke(_startTouch);
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            if (_isTouch == false || _fingerId != eventData.pointerId)
                return;

            Vector2 targetTouch = eventData.position;
            _stickVector = CalculateStickVector(_startTouch,
                targetTouch);

            FingerMove?.Invoke(_stickVector);
        }
    }
}
