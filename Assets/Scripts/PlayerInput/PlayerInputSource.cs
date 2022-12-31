using LevelCompleter;
using Plugins.WebGL;
using UI.Joystick;
using UnityEngine;

namespace PlayerInput
{
    public class PlayerInputSource : MonoBehaviour, ICharacterInputSource
    {
        private JavascriptHook _javascriptHook;
        private CursorLockerPanel _lockerPanel;
        private Completer _completer;
        private MovementInputSource _movementInput;
        private RotationInputSource _rotationInput;
        private bool _isPause, _isUnlocked;

        public Vector2 MovementInput { get; private set; }
        public Vector2 MouseInput { get; private set; }
        public float ScrollInput { get; private set; }
        
        private void Awake()
        {
            // WebGLInput.captureAllKeyboardInput = true; 
            _lockerPanel = FindObjectOfType<CursorLockerPanel>();
            var stick = FindObjectOfType<StickPointer>();
            _javascriptHook = FindObjectOfType<JavascriptHook>();
            _movementInput = new MovementInputSource(stick);
            var touchPointer = FindObjectOfType<TouchPointer>();
            _rotationInput = new RotationInputSource(touchPointer);
            _completer = FindObjectOfType<Completer>();
        }

        private void OnEnable()
        {
            _lockerPanel.PointerDowned += OnPointerDowned;
            _completer.Completed += Pause;
            _javascriptHook.PointerLocked += OnPointerLocked;
            _javascriptHook.PointerUnlocked += OnPointerUnlocked;
            _movementInput.Subscribe();
            _rotationInput.Subscribe();
        }

        private void OnDisable()
        {
            _lockerPanel.PointerDowned -= OnPointerDowned;
            _completer.Completed -= Pause;
            _javascriptHook.PointerLocked -= OnPointerLocked;
            _javascriptHook.PointerUnlocked -= OnPointerUnlocked;
            _movementInput.Unsubscribe();
            _rotationInput.Unsubscribe();
        }

        private void OnPointerDowned()
        {
            if (_isPause)
                return;
            
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            if (Input.GetButtonDown("Cancel") || Cursor.lockState == CursorLockMode.Locked
                && (Input.GetButtonDown("CursorUnlock")
                    || Input.GetButton("CursorUnlock")
                    || Input.GetButtonUp("CursorUnlock")))
            {
                //OnPointerUnlocked();
            }
            
            if (_isPause || _isUnlocked)
            {
                MouseInput = Vector2.zero;
                MovementInput = Vector2.zero;
                ScrollInput = 0f;
            }
            else
            {
                ScrollInput = Input.mouseScrollDelta.y;
                MouseInput = _rotationInput.GetInput();
                _rotationInput.Reset();
                MovementInput = _movementInput.GetInput();
            }
        }
        
        private void OnPointerLocked()
        {
            _isUnlocked = false;
        }

        private void OnPointerUnlocked()
        {
            _isUnlocked = true;
            Cursor.lockState = CursorLockMode.None;
            
        }

        private void Pause()
        {
            _movementInput.Reset();
            _rotationInput.Reset();
            OnPointerUnlocked();
            _isPause = true;
        }
    }
}