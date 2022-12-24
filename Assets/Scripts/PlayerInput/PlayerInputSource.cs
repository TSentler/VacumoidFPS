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
        private MousePointer _mousePointer;
        private Vector2 _lastDirection, _lastMouseMove;
        private bool _isPause;

        public Vector2 MovementInput { get; private set; }
        public Vector2 MouseInput { get; private set; }
        
        private void Awake()
        {
            _stick = FindObjectOfType<StickPointer>();
            _mousePointer = FindObjectOfType<MousePointer>();
            _completer = FindObjectOfType<Completer>();
        }

        private void OnEnable()
        {
            _completer.Completed += OnCompleted;
            _stick.FingerDown += StickOn;
            _stick.FingerOut += StickOff;
            _stick.FingerMove += Move;
            _mousePointer.FingerDown += MousePoinerOn;
            _mousePointer.FingerOut += MousePoinerOff;
            _mousePointer.FingerMove += MousePoinerMove;
        }

        private void OnDisable()
        {
            _completer.Completed -= OnCompleted;
            _stick.FingerDown -= StickOn;
            _stick.FingerOut -= StickOff;
            _stick.FingerMove -= Move;
            _mousePointer.FingerDown -= MousePoinerOn;
            _mousePointer.FingerOut -= MousePoinerOff;
            _mousePointer.FingerMove -= MousePoinerMove;
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
            if (_isPause)
            {
                _lastMouseMove = Vector2.zero;
            }
            else if (_mousePointer.IsTouch == false)
            {
                if (Cursor.lockState == CursorLockMode.Locked)
                {
                    _lastMouseMove = new Vector2(Input.GetAxis("Mouse X"),
                        Input.GetAxis("Mouse Y"));
                }
            }
            
            MouseInput = _lastMouseMove;
            _lastMouseMove = Vector2.zero;
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

        private void StickOn(Vector2 position)
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
        
        private void MousePoinerOn(Vector2 position)
        {
            _lastDirection = Vector2.zero;
        }
        
        private void MousePoinerOff()
        {
            _lastMouseMove = Vector2.zero;
        }

        private void MousePoinerMove(Vector2 direction)
        {
            _lastMouseMove = direction;
        }

        private void Pause()
        {
            StickOff();
            MousePoinerOff();
            _isPause = true;
        }
    }
}