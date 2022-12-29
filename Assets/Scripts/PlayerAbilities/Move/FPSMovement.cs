using System;
using UnityEngine;

namespace PlayerAbilities.Move
{
    public class FPSMovement : Movement
    {
        [SerializeField] private SpeedStat _speedStat;

        private Vector2 _inputDirection;

        private void Update()
        {
            if (InputSource != null)
                SetDirection(InputSource.MovementInput);
        }

        private void FixedUpdate()
        {
            var moveDirection = 
                new Vector3(_inputDirection.x, 0f, _inputDirection.y);
            moveDirection = Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up)
                * moveDirection;
            Rigidbody.velocity = moveDirection * _speedStat.Value * Time.deltaTime;
            //transform.position = Vector3.Lerp(transform.position,
              //  transform.position + moveDirection, _speedStat.Value * Time.deltaTime);
            //var path = moveDirection * _speedStat.Value;
            //Rigidbody.MovePosition(transform.position + path * Time.deltaTime);
            //Rigidbody.AddForce(path/Time.deltaTime/Time.deltaTime, ForceMode.Force);
            //
            return;
            Debug.Log(Rigidbody.velocity);
            if (moveDirection.sqrMagnitude == 0f)
            {
                Rigidbody.velocity = Vector3.zero;
            }

            float currentSpeed = Rigidbody.velocity.magnitude;
            float maxSpeed = _speedStat.Value * Time.deltaTime * 2f;
            float actualForce = _speedStat.Value * (1 - currentSpeed / maxSpeed);
            //Debug.Log(currentSpeed);
            //Debug.Log(maxSpeed);
            Rigidbody.AddForce(moveDirection * actualForce);
        }
        
        public override void SetDirection(Vector2 direction)
        {
            if (CanMove == false)
            {
                direction = Vector2.zero;
            }
            _inputDirection = direction;
            base.SetDirection(_inputDirection);
        }
    }
}
