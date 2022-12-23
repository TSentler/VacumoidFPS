using System;
using UnityEngine;

namespace UI.Joystick
{
    public class FloatingStick : MonoBehaviour
    {
        [SerializeField] private StickPointer _stickPointer;
        [SerializeField] private CanvasGroup _stickGroup; 
        [SerializeField] private RectTransform _stickArea, _stickKnob;

        private float _defaultYPosition, _defaultLeft, _defaultRight;
        
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
            _defaultYPosition = _stickArea.position.y;
            OnFingerOuted();
        }

        private void OnEnable()
        {
            _stickPointer.FingerDown += OnFingerDowned;
            _stickPointer.FingerOut += OnFingerOuted;
            _stickPointer.FingerMove += MoveStickKnob;
        }

        private void OnDisable()
        {
            _stickPointer.FingerDown -= OnFingerDowned;
            _stickPointer.FingerOut -= OnFingerOuted;
            _stickPointer.FingerMove -= MoveStickKnob;
        }

        private bool _isFingerOut;
        private void LateUpdate()
        {
            if (_isFingerOut)
            {
                var position = _stickArea.position;
                position = new Vector3(position.x, _defaultYPosition, position.z);
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
