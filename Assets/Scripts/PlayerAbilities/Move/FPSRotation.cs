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
        [SerializeField] private Vector2 _turn;

        private ICharacterInputSource InputSource;
        private Rigidbody _rigidbody;
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
            
            var rotation = Quaternion.AngleAxis(-_turn.y, Vector3.right);
            _cameraRoot.localRotation = rotation;
        }
    }
}
