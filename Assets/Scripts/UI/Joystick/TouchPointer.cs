using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI.Joystick
{
    public class TouchPointer : MonoBehaviour, IDragHandler,
        IPointerDownHandler, IPointerUpHandler
    {
        private readonly float _deadZone = 0.05f;

        private Vector2 _previousTouch, _currentTouch;
        private int _fingerId = int.MinValue;
    
        public bool IsTouch => _fingerId != int.MinValue;
    
        public event UnityAction FingerOut;
        public event UnityAction<Vector2> FingerDown, FingerMove;

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
            _previousTouch = _currentTouch = eventData.position;
            FingerDown?.Invoke(_previousTouch);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_fingerId != eventData.pointerId)
                return;
            
            _previousTouch = _currentTouch;
            _currentTouch = eventData.position;
            var moveVector = CalculateMoveVector(_currentTouch, 
                _previousTouch);

            FingerMove?.Invoke(moveVector);
        }
    }
}
