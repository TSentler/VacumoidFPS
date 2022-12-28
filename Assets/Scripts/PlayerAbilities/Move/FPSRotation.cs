using System;
using PlayerInput;
using UnityEngine;
using UnityTools;

namespace PlayerAbilities.Move
{
    [RequireComponent(typeof(Rigidbody))]
    public class FPSRotation : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _inputSourceBehaviour;
        [SerializeField] private Transform _cameraRoot;

        private ICharacterInputSource InputSource;
        private Rigidbody _rigidbody;
        private Vector2 _turn;
        [SerializeField] private float _sensitivity = 100f;

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
        
        private void Initialize(ICharacterInputSource inputSource)
        {
            InputSource = inputSource;
        }
        
        protected virtual void Awake()
        {
            if (InputSource == null)
            {
                Initialize((ICharacterInputSource)_inputSourceBehaviour);
            }

            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _turn += InputSource.MouseInput * _sensitivity;
            _turn.x %= 360f;
            _turn.y %= 360f;
        }

        private void FixedUpdate()
        {
            _rigidbody.MoveRotation(Quaternion.Euler(0f, _turn.x, 0f));
            
            var cameraRotation = _cameraRoot.localRotation.eulerAngles;
            cameraRotation.x = -_turn.y;
            _cameraRoot.localRotation = Quaternion.Euler(cameraRotation);
        }
    }
}
