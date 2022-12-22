using UnityEngine;

namespace PlayerAbilities.Move
{
    public class FPSMovement : Movement
    {
        [SerializeField] private SpeedStat _speedStat;
        [SerializeField] private float _turnSmoothTime = 0.1f,
            _rotationSpeed = 9f;
        
        private Vector2 _inputDirection, _rotateDirection;
        private float _turnSmoothVelocity;

        private void HandleRotation()
        {
            /*
            var targetRotation = Quaternion.LookRotation(
                new Vector3(direction.x, 0f, direction.y));

            transform.rotation = Quaternion.Slerp(
                transform.rotation, targetRotation,
                _rotationSpeed * Time.deltaTime);
           */
            float targetAngle =
                Mathf.Atan2(_inputDirection.x, _inputDirection.y) * Mathf.Rad2Deg;
            Debug.Log(targetAngle);
            targetAngle += Camera.main.transform.eulerAngles.y;
            Debug.Log(targetAngle + "!");
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(Vector3.up * angle);
        }

        private void Update()
        {
            if(InputSource != null)
                SetDirection(InputSource.MovementInput);
        }

        private void FixedUpdate()
        {
            var relativeRotation = transform.rotation;
            var moveDirection = 
                new Vector3(_inputDirection.x, 0f, _inputDirection.y);
            moveDirection = 
                Quaternion.Euler(Vector3.up * relativeRotation.eulerAngles.y) 
                * moveDirection;
            
            var deltaSpeed = _speedStat.Value * Time.deltaTime;
            Rigidbody.velocity = moveDirection * deltaSpeed;
        }
        
        public override void SetDirection(Vector2 direction)
        {
            _rotateDirection = direction;
            if (CanMove == false)
            {
                direction = Vector2.zero;
            }
            _inputDirection = direction;
            base.SetDirection(_inputDirection);
        }
    }
}
