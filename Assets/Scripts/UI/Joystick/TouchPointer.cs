using UnityEngine;
using UnityEngine.Events;

namespace UI.Joystick
{
    [RequireComponent(typeof(PointerHandler))]
    public class TouchPointer : MonoBehaviour, ITouchable
    {
        private readonly float _deadZone = 0.05f;

        private PointerHandler _pointerHandler;
        private Vector2 _previousTouch, _currentTouch;

        public event UnityAction Outed;
        public event UnityAction<Vector2> Downed, Moved;

        public bool IsTouch => _pointerHandler.IsTouch;
        
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
            _previousTouch = _currentTouch = position;
            Downed?.Invoke(_currentTouch);
        }

        private void OnPointerMoved(Vector2 position)
        {
            _previousTouch = _currentTouch;
            _currentTouch = position;
            var moveVector = CalculateMoveVector(_currentTouch, 
                _previousTouch);
            Moved?.Invoke(moveVector);
        }

        private Vector2 CalculateMoveVector(Vector2 currentPosition, 
            Vector2 previousPosition)
        {
            var move = currentPosition - previousPosition;
            move = new Vector2(move.x / Screen.width, move.y / Screen.height);
            move *= Screen.dpi;
            if (move.magnitude < _deadZone)
                move = Vector2.zero;

            return move;
        }

    }
}
