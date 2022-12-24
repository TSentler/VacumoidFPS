using System;
using System.Linq;
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
    
        public bool IsTouch => _fingerId != int.MinValue;
    
        public event UnityAction FingerOut;
        public event UnityAction<Vector2> FingerDown, FingerMove;

        private void OnValidate()
        {
            if (_stickRect == null)
                Debug.LogWarning("RectTransform was not found!", this);
        }

        private void LateUpdate()
        {
            CheckFinger();
        }

        private void CheckFinger()
        {
            var isMouse = _fingerId != int.MinValue && _fingerId < 0;
            var hasTouch =
                Input.touches.Any(touch => touch.fingerId == _fingerId);
            if (isMouse == false && hasTouch == false && IsTouch)
            {
                OnFingerOuted();
            }
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
            if (_fingerId != eventData.pointerId)
                return;

            OnFingerOuted();
        }

        private void OnFingerOuted()
        {
            _fingerId = int.MinValue;
            FingerOut?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (IsTouch)
                return;
            
            _fingerId = eventData.pointerId;
            _startTouch = eventData.position;
            FingerDown?.Invoke(_startTouch);
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            if (_fingerId != eventData.pointerId)
                return;

            Vector2 targetTouch = eventData.position;
            _stickVector = CalculateStickVector(_startTouch,
                targetTouch);

            FingerMove?.Invoke(_stickVector);
        }
    }
}
