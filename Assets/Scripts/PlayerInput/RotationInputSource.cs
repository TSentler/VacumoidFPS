using UI.Joystick;
using UnityEngine;

namespace PlayerInput
{
    public class RotationInputSource
    {
        public RotationInputSource(ITouchable touchable)
        {
            _touchable = touchable;
        }

        private ITouchable _touchable;
        private Vector2 _lastDirection;

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
        
        public Vector2 GetInput()
        {
            if (_touchable.IsTouch == false)
            {
                if (Cursor.lockState == CursorLockMode.Locked)
                {
                    _lastDirection = new Vector2(Input.GetAxis("Mouse X"),
                        Input.GetAxis("Mouse Y"));
                }
            }
            
            return _lastDirection;
        }

        public void Reset()
        {
            _lastDirection = Vector2.zero;
        }
        
        private void OnDowned(Vector2 position)
        {
            Reset();
        }
        
        private void OnOuted()
        {
            Reset();
        }
        
        private void OnMoved(Vector2 direction)
        {
            _lastDirection = direction;
        }
    }
}