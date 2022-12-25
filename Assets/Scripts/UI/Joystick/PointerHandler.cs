using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI.Joystick
{
    public class PointerHandler : MonoBehaviour, IDragHandler,
        IPointerDownHandler, IPointerUpHandler
    {
        private int _fingerId = int.MinValue;
    
        public event UnityAction PointerOuted;
        public event UnityAction<Vector2> PointerDowned, PointerMoved;

        public bool IsTouch => _fingerId != int.MinValue;
    
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

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_fingerId != eventData.pointerId)
                return;

            OnFingerOuted();
        }

        private void OnFingerOuted()
        {
            _fingerId = int.MinValue;
            PointerOuted?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (IsTouch)
                return;
                
            _fingerId = eventData.pointerId;
            PointerDowned?.Invoke(eventData.position);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_fingerId != eventData.pointerId)
                return;
            
            PointerMoved?.Invoke(eventData.position);
        }
    }
}
