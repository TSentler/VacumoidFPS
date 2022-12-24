using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI.Joystick
{
    public class MousePointer : MonoBehaviour, IDragHandler,
        IPointerDownHandler, IPointerUpHandler
    {
        private readonly float _deadZone = 0.05f;

        [SerializeField] private float _referenceDiagonal = 24f;
        
        private Vector2 _previousTouch, _currentTouch;
        private int _fingerId = int.MinValue;
        private bool _isTouch;
    
        public bool IsTouch => _isTouch;
    
        public event UnityAction FingerOut;
        public event UnityAction<Vector2> FingerDown, FingerMove;

        private Vector2 CalculateMoveVector(Vector2 _currentPosition, 
            Vector2 previousPosition)
        {
            var move = _currentPosition - previousPosition;
            move = new Vector2(move.x / Screen.width, move.y / Screen.height);
            move *= Screen.dpi;
            if (move.magnitude < _deadZone)
                move = Vector2.zero;

            return move;
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
            _previousTouch = _currentTouch = eventData.position;
            FingerDown?.Invoke(_previousTouch);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_isTouch == false || _fingerId != eventData.pointerId)
                return;
            
            _previousTouch = _currentTouch;
            _currentTouch = eventData.position;
            var moveVector = CalculateMoveVector(_currentTouch, 
                _previousTouch);

            FingerMove?.Invoke(moveVector);
        }
    }
}
