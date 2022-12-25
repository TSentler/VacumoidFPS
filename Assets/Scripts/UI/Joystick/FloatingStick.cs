using System;
using UnityEngine;

namespace UI.Joystick
{
    public class FloatingStick : MonoBehaviour
    {
        [SerializeField] private StickPointer _stickPointer;
        [SerializeField] private CanvasGroup _stickGroup; 
        [SerializeField] private RectTransform _stickArea, _stickKnob;

        private float _defaultYRatio, _defaultLeft, _defaultRight;
        private bool _isFingerOut;
        
        private void OnValidate()
        {
            if (_stickPointer == null)
                Debug.LogWarning("StickPointer was not found!", this);
            if (_stickArea == null)
                Debug.LogWarning("Stick area was not found!", this);
            if (_stickKnob == null)
                Debug.LogWarning("Stick knob was not found!", this);
        }

        private void Start()
        {
            _defaultLeft = _stickArea.offsetMin.x;
            _defaultRight = _stickArea.offsetMax.x;
            _defaultYRatio = _stickArea.position.y / Screen.height;
            OnFingerOuted();
        }

        private void OnEnable()
        {
            _stickPointer.Downed += OnFingerDowned;
            _stickPointer.Outed += OnFingerOuted;
            _stickPointer.Moved += MoveStickKnob;
        }

        private void OnDisable()
        {
            _stickPointer.Downed -= OnFingerDowned;
            _stickPointer.Outed -= OnFingerOuted;
            _stickPointer.Moved -= MoveStickKnob;
        }

        private void LateUpdate()
        {
            if (_isFingerOut)
            {
                var position = _stickArea.position;
                var defaultYPosition = _defaultYRatio * Screen.height;
                position = new Vector3(position.x, defaultYPosition, position.z);
                _stickArea.position = position;
                _isFingerOut = false;
            }
        }

        private void OnFingerDowned(Vector2 position)
        {
            _stickArea.position = new Vector3(position.x, position.y,
                _stickArea.position.z);
            _stickGroup.alpha = 0.5f;
            _isFingerOut = false;
        }
        
        private void OnFingerOuted()
        {
            _stickGroup.alpha = 1f;
            _stickArea.offsetMin = new Vector2(_defaultLeft, _stickArea.anchorMin.y);
            _stickArea.offsetMax = new Vector2(_defaultRight, _stickArea.anchorMax.y);
            _stickKnob.localPosition = 
                new Vector3(0f, 0f, _stickKnob.localPosition.z);
            _isFingerOut = true;
        }
        
        private void MoveStickKnob(Vector2 position)
        {
            var radius = _stickArea.rect.height / 2;
            position *= radius;
            _stickKnob.localPosition = position;
        }
    }
}
