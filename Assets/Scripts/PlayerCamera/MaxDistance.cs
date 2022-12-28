using System;
using PlayerInput;
using Saves;
using UnityEngine;
using UnityTools;

namespace PlayerCamera
{
    public class MaxDistance : MonoBehaviour
    {
        private readonly CameraDistanceSaver _saver = new ();
        
        [SerializeField] private MonoBehaviour _inputSourceBehaviour;
        [SerializeField] private float _step = 0.1f;
    
        private ICharacterInputSource _inputSource;

        public float Value { get; private set; } = 1f;
        
        public static implicit operator float(MaxDistance maxDistance)
        {
            return maxDistance.Value;
        }
        
        private void OnValidate()
        {
            if (PrefabChecker.InPrefabFileOrStage(gameObject))
                return;
                         
            if (_inputSourceBehaviour 
                && !(_inputSourceBehaviour is ICharacterInputSource))
            {
                Debug.LogError(nameof(_inputSourceBehaviour) + " needs to implement " + nameof(ICharacterInputSource));
                _inputSourceBehaviour = null;
            }
        } 
    
        private void Awake()
        {
            _inputSource = (ICharacterInputSource)_inputSourceBehaviour;
        }

        private void OnEnable()
        {
            Value = _saver.Load();
        }

        private void OnDisable()
        {
            _saver.Save(Value);
        }

        private void Update()
        {
            Value -= _inputSource.ScrollInput * _step;
            Value = Mathf.Clamp01(Value);
        }
    }
}
