using UnityEngine;

namespace PlayerAbilities.Move
{
    public class FPSMovement : Movement
    {
        [SerializeField] private SpeedStat _speedStat;
        [SerializeField] private float _rotationSpeed = 9f;
        
        private Vector2 _moveDirection, _rotateDirection;

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
            if(InputSource != null)
                Move(InputSource.MovementInput);
        }

        private void FixedUpdate()
        {
            var deltaSpeed = _speedStat.Value * Time.deltaTime;
            Rigidbody.velocity = new Vector3(
                _moveDirection.x * deltaSpeed,
                0f,
                _moveDirection.y * deltaSpeed);   
            
            HandleRotation(_rotateDirection);
        }
        
        public override void Move(Vector2 direction)
        {
            _rotateDirection = direction;
            if (CanMove == false)
            {
                direction = Vector2.zero;
            }
            _moveDirection = direction;
            base.Move(_moveDirection);
        }
    }
}
