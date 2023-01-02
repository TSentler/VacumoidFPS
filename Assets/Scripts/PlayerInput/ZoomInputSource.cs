using UI.Joystick;
using UnityEngine;

namespace PlayerInput
{
    public class ZoomInputSource
    {
        public ZoomInputSource(ZoomTouch touchable)
        {
            _touchable = touchable;
        }

        private ZoomTouch _touchable;
        private float _lastDistance;

        public void Subscribe()
        {
            _touchable.Downed += OnDowned;
            _touchable.Outed += OnOuted;
            _touchable.Moved += OnMoved;
        }

        public void Unsubscribe()
        {
            _touchable.Downed -= OnDowned;
            _touchable.Outed -= OnOuted;
            _touchable.Moved -= OnMoved;
        }
        
        public float GetInput()
        {
            if (_touchable.IsTouch == false)
            {
                _lastDistance = Input.mouseScrollDelta.y;
            }

            return _lastDistance;
        }

        public void Reset()
        {
            _lastDistance = 0f;
        }
        
        private void OnDowned(float distance)
        {
            Reset();
        }
        
        private void OnOuted()
        {
            Reset();
        }
        
        private void OnMoved(float distance)
        {
            _lastDistance = distance;
        }
    }
}