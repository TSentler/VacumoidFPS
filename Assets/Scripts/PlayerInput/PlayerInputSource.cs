using LevelCompleter;
using UI.Joystick;
using UnityEngine;

namespace PlayerInput
{
    public interface ICharacterInputSource
    {
        Vector2 MovementInput { get; }
        Vector2 MouseInput { get; }
    }
    
    public class PlayerInputSource : MonoBehaviour, ICharacterInputSource
    {
        private CursorLockerPanel _lockerPanel;
        private Completer _completer;
        private StickPointer _stick;
        private TouchPointer _touchPointer;
        private Vector2 _lastDirection, _lastMouseMove;
        private bool _isPause;

        public Vector2 MovementInput { get; private set; }
        public Vector2 MouseInput { get; private set; }
        
        private void Awake()
        {
            _lockerPanel = FindObjectOfType<CursorLockerPanel>();
            _stick = FindObjectOfType<StickPointer>();
            _touchPointer = FindObjectOfType<TouchPointer>();
            _completer = FindObjectOfType<Completer>();
        }

        private void OnEnable()
        {
            _lockerPanel.PointerDowned += OnPointerDowned;
            _completer.Completed += OnCompleted;
            _stick.FingerDown += StickOn;
            _stick.FingerOut += StickOff;
            _stick.FingerMove += Move;
            _touchPointer.FingerDown += TouchPoinerOn;
            _touchPointer.FingerOut += TouchPoinerOff;
            _touchPointer.FingerMove += TouchPoinerMove;
        }

        private void OnDisable()
        {
            _lockerPanel.PointerDowned -= OnPointerDowned;
            _completer.Completed -= OnCompleted;
            _stick.FingerDown -= StickOn;
            _stick.FingerOut -= StickOff;
            _stick.FingerMove -= Move;
            _touchPointer.FingerDown -= TouchPoinerOn;
            _touchPointer.FingerOut -= TouchPoinerOff;
            _touchPointer.FingerMove -= TouchPoinerMove;
        }

        private void OnPointerDowned()
        {
            if (_isPause)
                return;
            
            CursorLock();
        }

        private void OnCompleted()
        {
            Pause();
        }

        private void Update()
        {
            if (Input.GetButtonDown("CursorUnlock"))
            {
                CursorUnlock();
            }
            
            SetMouseInput();
            SetMovementInput();
        }

        private void CursorLock()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void CursorUnlock()
        {
            Cursor.lockState = CursorLockMode.None;
        }

        private void SetMouseInput()
        {
            if (_isPause)
            {
                _lastMouseMove = Vector2.zero;
            }
            else if (_touchPointer.IsTouch == false)
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
        
        private void TouchPoinerOn(Vector2 position)
        {
            _lastMouseMove = Vector2.zero;
        }
        
        private void TouchPoinerOff()
        {
            _lastMouseMove = Vector2.zero;
        }

        private void TouchPoinerMove(Vector2 direction)
        {
            _lastMouseMove = direction;
        }

        private void Pause()
        {
            StickOff();
            TouchPoinerOff();
            CursorUnlock();
            _isPause = true;
        }
    }
}