using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI.Joystick
{
    public class TwoPointersHandler : MonoBehaviour, IDragHandler,
        IPointerDownHandler, IPointerUpHandler
    {
        private int[] _fingersId = new int[2]{
            int.MinValue, int.MinValue
        };
        
        private Vector3[] _lastPositions = new Vector3[2]{
            Vector3.zero, Vector3.zero
        };
    
        public event UnityAction PointerOuted;
        public event UnityAction<float> PointersDowned, PointerMoved;

        public bool IsTouch => _fingersId.All(item => item != int.MinValue);
    
        private void LateUpdate()
        {
            CheckFingers();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            for (int i = 0; i < _fingersId.Length; i++)
            {
                if (_fingersId[i] == eventData.pointerId)
                {
                    OnFingerOuted(i);
                    break;
                }
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            var eventId = eventData.pointerId;
            if (IsTouch)
                return;

            for (int i = 0; i < _fingersId.Length; i++)
            {
                if (_fingersId[i] == eventId)
                {
                    _lastPositions[i] = eventData.position;
                    return;
                }
            }
            
            for (int i = 0; i < _fingersId.Length; i++)
            {
                if (_fingersId[i] == int.MinValue)
                {
                    _fingersId[i] = eventId;
                    _lastPositions[i] = eventData.position;
                    break;
                }
            }

            if (IsTouch)
            {
                PointersDowned?.Invoke(Distance());
            }
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            for (int i = 0; i < _fingersId.Length; i++)
            {
                if (_fingersId[i] == eventData.pointerId)
                {
                    _lastPositions[i] = eventData.position;
                    PointerMoved?.Invoke(Distance());
                    break;
                }
            }
        }

        private void CheckFingers()
        {
            for (int i = 0; i < _fingersId.Length; i++)
            {
                var fingerId = _fingersId[i];
                if (fingerId == int.MinValue)
                    continue;
                
                var isMouse = fingerId < 0;
                var hasTouch =
                    Input.touches.Any(touch => touch.fingerId == fingerId);
                if (isMouse == false && hasTouch == false)
                {
                    OnFingerOuted(i);
                }
            }
        }

        private void OnFingerOuted(int i)
        {
            if (IsTouch)
            {
                PointerOuted?.Invoke();
            }
            
            _fingersId[i] = int.MinValue;
        }

        private float Distance()
        {
            return Vector3.Distance(_lastPositions[0], _lastPositions[1]);
        }
    }
}
