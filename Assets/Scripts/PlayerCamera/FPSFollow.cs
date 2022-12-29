using System;
using UnityEngine;

namespace PlayerCamera
{
    public class FPSFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [Min(0f), SerializeField] private float _smoothTime = 0.3f,
            _rotationSmoothTime = 0.3f;

        private Vector3 _targetPosition;
        private Quaternion _targetRotation;

        private void Update()
        {
            _targetRotation = _target.rotation;
        }

        private void FixedUpdate()
        {
            //Follow();
            Rotate();
        }

        private void LateUpdate()
        {
            _targetPosition = _target.position;
            Follow();
        }

        private void Follow()
        {
            Vector3 targetPosition = _targetPosition;
            Vector3 velocity = Vector3.zero;
            targetPosition = Vector3.SmoothDamp(transform.position,
                targetPosition, ref velocity, _smoothTime);
            //GetComponent<Rigidbody>().MovePosition(targetPosition);
            // transform.position = Vector3.MoveTowards(transform.position,
                // _targetPosition, _smoothTime * Time.deltaTime);
            transform.position = _targetPosition;
        }

        private void Rotate()
        {
            var angle = Quaternion.Angle(_targetRotation, transform.rotation);
            if (Mathf.Abs(angle) < 0.1f)
                return;
            
            Quaternion lerp = Quaternion.Lerp(transform.rotation, _targetRotation, _rotationSmoothTime * Time.deltaTime);
            transform.rotation = lerp;
        }
    }
}
