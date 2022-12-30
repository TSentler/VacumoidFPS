using System;
using PlayerInput;
using UnityEngine;
using UnityTools;

namespace PlayerAbilities.Move
{
    public class FPSRotation : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _inputSourceBehaviour;
        [SerializeField] private Transform _cameraRoot;
        [SerializeField] private Vector2 _turn;
        [Min(0.001f), SerializeField] private float _speed = 10f;
        [Min(0.01f), SerializeField] private float _sensitivity = 1f;

        public void Set(float val)
        {
            _sensitivity = val;
        }

        private ICharacterInputSource InputSource;

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
        }
        
        private void Update()
        {
            _turn += InputSource.MouseInput * _sensitivity;
            _turn.x %= 360f;
            _turn.y %= 360f;
        }

        private void FixedUpdate()
        {
            var target = Quaternion.AngleAxis(_turn.x, Vector3.up);
            var angle = Vector3.SignedAngle(transform.forward, target * Vector3.forward, Vector3.up);
            angle *= _speed * Time.deltaTime;
            if (Mathf.Abs(angle) > 0.001f)
                 transform.rotation *= Quaternion.AngleAxis(angle, Vector3.up);
            
            var rotation = Quaternion.AngleAxis(-_turn.y, Vector3.right);
            _cameraRoot.localRotation = rotation;
        }
    }
}
