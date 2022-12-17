using UnityEngine;

namespace UI.Joystick
{
    public class FloatingStick : MonoBehaviour
    {
        [SerializeField] private StickPointer _stickPointer;
        [SerializeField] private CanvasGroup _stickGroup; 
        [SerializeField] private RectTransform _stickArea, _stickKnob;

        private void OnValidate()
        {
            if (_stickPointer == null)
                Debug.LogWarning("StickPointer was not found!", this);
            if (_stickArea == null)
                Debug.LogWarning("Stick area was not found!", this);
            if (_stickKnob == null)
                Debug.LogWarning("Stick knob was not found!", this);
        }

        private void Awake()
        {
            HideStick();
        }

        private void OnEnable()
        {
            _stickPointer.FingerDown += ShowStick;
            _stickPointer.FingerOut += HideStick;
            _stickPointer.FingerMove += MoveStickKnob;
        }

        private void OnDisable()
        {
            _stickPointer.FingerDown -= ShowStick;
            _stickPointer.FingerOut -= HideStick;
            _stickPointer.FingerMove -= MoveStickKnob;
        }
        
        private void ShowStick(Vector2 position)
        {
            _stickArea.position = new Vector3(position.x, position.y,
                _stickArea.position.z);
            _stickGroup.alpha = 1;
        }
        
        private void HideStick()
        {
            _stickGroup.alpha = 0;
            _stickKnob.localPosition = 
                new Vector3(0f, 0f, _stickKnob.localPosition.z);
        }
        
        private void MoveStickKnob(Vector2 position)
        {
            var radius = _stickArea.rect.height / 2;
            position *= radius;
            _stickKnob.localPosition = position;
        }
    }
}
