using LevelCompleter;
using UI.Joystick;
using UnityEngine;
using UnityTools;

namespace PlayerInput
{
    public interface ICharacterInputSource
    {
        Vector2 MovementInput { get; }
        Vector2 MouseInput { get; }
    }
    
    public class PlayerInputSource : MonoBehaviour, ICharacterInputSource
    {
        private Completer _completer;
        private StickPointer _stick;
        private Vector2 _lastDirection;
        private bool _isPause;

        public Vector2 MovementInput { get; private set; }
        public Vector2 MouseInput { get; private set; }
        
        private void Awake()
        {
            _stick = FindObjectOfType<StickPointer>();
            _completer = FindObjectOfType<Completer>();
        }

        private void OnEnable()
        {
            _completer.Completed += OnCompleted;
            _stick.FingerDown += StickOn;
            _stick.FingerOut += StickOff;
            _stick.FingerMove += Move;
        }

        private void OnDisable()
        {
            _completer.Completed -= OnCompleted;
            _stick.FingerDown -= StickOn;
            _stick.FingerOut -= StickOff;
            _stick.FingerMove -= Move;
        }

        private void OnCompleted()
        {
            Pause();
        }

        private void Update()
        {
            SetMouseInput();
            SetMovementInput();
        }

        private void SetMouseInput()
        {
            MouseInput = Vector2.zero;
            if (_isPause)
            {
                MouseInput = Vector2.zero;
            }
            else if (Cursor.lockState == CursorLockMode.Locked)
            {
                MouseInput = new Vector2(Input.GetAxis("Mouse X"),
                    Input.GetAxis("Mouse Y"));
            }
        }

        private void SetMovementInput()
        {
            if (_isPause)
            {
                _lastDirection = Vector2.zero;
            }
            else if (_stick.IsTouch == false)
            {
                _lastDirection = new Vector2(
                    Input.GetAxisRaw("Horizontal"),
                    Input.GetAxisRaw("Vertical"));
                if (_lastDirection.magnitude > 1f)
                {
                    _lastDirection.Normalize();
                }
            }
            
            MovementInput = _lastDirection;
        }

        private void StickOn(Vector2 direction)
        {
            _lastDirection = Vector2.zero;
        }
        
        private void StickOff()
        {
            _lastDirection = Vector2.zero;
        }
        
        private void Move(Vector2 direction)
        {
            _lastDirection = direction;
        }

        private void Pause()
        {
            StickOff();
            _isPause = true;
        }
    }
}