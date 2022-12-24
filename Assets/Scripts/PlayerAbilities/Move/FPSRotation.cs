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

        private ICharacterInputSource InputSource;
        private Vector2 _turn;
        private float _sensitivity = 1f;

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
            transform.localRotation = Quaternion.Euler(0f, _turn.x, 0f);
            _cameraRoot.localRotation = Quaternion.Euler(-_turn.y, 0f, 0f);
            // Debug.Log(transform.localRotation.eulerAngles.y);
            // Debug.Log(_cameraRoot.localRotation.eulerAngles.x);
        }

        private void HandleRotation()
        {
            /*
            var targetRotation = Quaternion.LookRotation(
                new Vector3(direction.x, 0f, direction.y));

            transform.rotation = Quaternion.Slerp(
                transform.rotation, targetRotation,
                _rotationSpeed * Time.deltaTime);
           */
            
            /*
            float targetAngle =
                Mathf.Atan2(_inputDirection.x, _inputDirection.y) * Mathf.Rad2Deg;
            Debug.Log(targetAngle);
            targetAngle += Camera.main.transform.eulerAngles.y;
            Debug.Log(targetAngle + "!");
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(Vector3.up * angle);
            */
        }
    }
}
