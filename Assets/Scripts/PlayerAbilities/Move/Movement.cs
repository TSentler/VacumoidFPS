using UnityEngine;
using UnityEngine.Events;

namespace PlayerAbilities.Move
{
    [RequireComponent(typeof(Rigidbody))]
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float _runSpeed = 150f,
            _speedMultiply = 1.5f,
            _rotationSpeed = 15f;

        private Rigidbody _rb;
        private Vector2 _moveDirection, _rotateDirection;
        private float _currentSpeed;
        private bool _canMove = true;

        public event UnityAction<Vector2> Moved;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _currentSpeed = _runSpeed;
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

        private void FixedUpdate()
        {
            var deltaSpeed = _currentSpeed * Time.deltaTime;
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

        public void BoostSpeed()
        {
            _currentSpeed = _runSpeed * _speedMultiply;
        }

        public void ResetSpeed()
        {
            _currentSpeed = _runSpeed;
        }

        public void Upgrade(float speed)
        {
            _currentSpeed = _runSpeed = speed;
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
