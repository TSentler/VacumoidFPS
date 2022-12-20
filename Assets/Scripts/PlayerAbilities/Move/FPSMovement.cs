using PlayerInput;
using UnityEngine;
using UnityEngine.Events;
using UnityTools;

namespace PlayerAbilities.Move
{
    [RequireComponent(typeof(Rigidbody))]
    public class FPSMovement : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _inputSourceBehaviour;
        [SerializeField] private SpeedStat _speedStat;
        [SerializeField] private float _rotationSpeed = 15f;
        
        private ICharacterInputSource _inputSource;
        private Rigidbody _rb;
        private Vector2 _moveDirection, _rotateDirection;
        private bool _canMove = true;

        public event UnityAction<Vector2> Moved;

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
            _inputSource = inputSource;
        }
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            if (_inputSource == null)
            {
                Initialize((ICharacterInputSource)_inputSourceBehaviour);
            }
        }

        private void HandleRotation(Vector2 direction)
        {
            if (direction.magnitude < 0.05f)
                return;

            var targetRotation = Quaternion.LookRotation(
                new Vector3(direction.x, 0f, direction.y));

            transform.rotation = Quaternion.Slerp(
                transform.rotation, targetRotation,
                _rotationSpeed * Time.deltaTime);
        }

        private void Update()
        {
            if(_inputSource != null)
                Move(_inputSource.MovementInput);
        }

        private void FixedUpdate()
        {
            var deltaSpeed = _speedStat.Value * Time.deltaTime;
            _rb.velocity = new Vector3(
                _moveDirection.x * deltaSpeed,
                0f,
                _moveDirection.y * deltaSpeed);   
            
            HandleRotation(_rotateDirection);
        }
        
        public void Move(Vector2 direction)
        {
            _rotateDirection = direction;
            if (_canMove == false)
            {
                direction = Vector2.zero;
            }
            _moveDirection = direction;
            Moved?.Invoke(_moveDirection);
        }

        public void Stop()
        {
            _canMove = false;
            _rb.isKinematic = true;
        }

        public void Go()
        {
            _canMove = true;
            _rb.isKinematic = false;
        }
    }
}
