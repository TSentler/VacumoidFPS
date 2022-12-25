using LevelCompleter;
using UI.Joystick;
using UnityEngine;

namespace PlayerInput
{
    public class PlayerInputSource : MonoBehaviour, ICharacterInputSource
    {
        private CursorLockerPanel _lockerPanel;
        private Completer _completer;
        private MovementInputSource _movementInput;
        private RotationInputSource _rotationInput;
        private bool _isPause;

        public Vector2 MovementInput { get; private set; }
        public Vector2 MouseInput { get; private set; }
        
        private void Awake()
        {
            _lockerPanel = FindObjectOfType<CursorLockerPanel>();
            var stick = FindObjectOfType<StickPointer>();
            _movementInput = new MovementInputSource(stick);
            var touchPointer = FindObjectOfType<TouchPointer>();
            _rotationInput = new RotationInputSource(touchPointer);
            _completer = FindObjectOfType<Completer>();
        }

        private void OnEnable()
        {
            _lockerPanel.PointerDowned += OnPointerDowned;
            _completer.Completed += Pause;
            _movementInput.Subscribe();
            _rotationInput.Subscribe();
        }

        private void OnDisable()
        {
            _lockerPanel.PointerDowned -= OnPointerDowned;
            _completer.Completed -= Pause;
            _movementInput.Unsubscribe();
            _rotationInput.Unsubscribe();
        }

        private void OnPointerDowned()
        {
            if (_isPause)
                return;
            
            CursorLock();
        }

        private void Update()
        {
            if (Input.GetButtonDown("CursorUnlock"))
            {
                CursorUnlock();
            }

            if (_isPause)
            {
                MouseInput = Vector2.zero;
                MovementInput = Vector2.zero;
            }
            else
            {
                MouseInput = _rotationInput.GetInput();
                _rotationInput.Reset();
                MovementInput = _movementInput.GetInput();
            }
        }

        private void CursorLock()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void CursorUnlock()
        {
            Cursor.lockState = CursorLockMode.None;
        }

        private void Pause()
        {
            _movementInput.Reset();
            _rotationInput.Reset();
            CursorUnlock();
            _isPause = true;
        }
    }
}