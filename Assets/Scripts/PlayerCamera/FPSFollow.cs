using System;
using UnityEngine;

namespace PlayerCamera
{
    public class FPSFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [Min(0f), SerializeField] private float _smoothTime = 0.3f,
            _rotationSmoothTime = 0.3f;
        
        private Vector3 _targetPosition, _targetRotation;

        private void Update()
        {
            //Rotate();
        }

        private void FixedUpdate()
        {
            //Follow();
        }

        private void LateUpdate()
        {
            _targetPosition = _target.position;
            _targetRotation = _target.eulerAngles;
            Follow();
            Rotate();
        }

        private void Follow()
        {
            Vector3 targetPosition = _targetPosition;
            Vector3 velocity = Vector3.zero;
            targetPosition = Vector3.SmoothDamp(transform.position,
                targetPosition, ref velocity, _smoothTime);
            //GetComponent<Rigidbody>().MovePosition(targetPosition);
            //transform.position = targetPosition;
            transform.position = _targetPosition;
        }

        private void Rotate()
        {
            var targetEulerRotation = _targetRotation;
            float velocityMove = 0f;
            targetEulerRotation.y = Mathf.SmoothDampAngle(
                transform.eulerAngles.y, targetEulerRotation.y,
                ref velocityMove, _rotationSmoothTime);

            Quaternion targetRotation = Quaternion.Euler(targetEulerRotation);
            //GetComponent<Rigidbody>().MoveRotation(targetRotation);
            //transform.rotation = targetRotation;
            transform.rotation = Quaternion.Euler(_targetRotation);
        }
    }
}
