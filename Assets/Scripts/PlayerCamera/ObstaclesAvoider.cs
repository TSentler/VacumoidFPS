using System;
using UnityEngine;

namespace PlayerCamera
{
    public class ObstaclesAvoider : MonoBehaviour
    {
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private Transform _root;
        [Min(0f), SerializeField] private float _radius = 0.5f, _smooth = 20f;

        private Vector3 _defaultLocalPosition, _currentLocalPosition;

        private void Awake()
        {
            _defaultLocalPosition = transform.localPosition;
        }

        private void Update()
        {
            var targetLocalPosition = GetCameraLocalPosition();
            if ((targetLocalPosition - transform.localPosition).magnitude > 0.001f)
            {
                Debug.Log("Smooth");
                var step = _smooth * Time.deltaTime;
                transform.localPosition = Vector3.MoveTowards(
                    transform.localPosition, targetLocalPosition, step);
            }
        }

        private Vector3 GetCameraLocalPosition()
        {
            Vector3 cameraLocalPosition;
            var defaultCameraPosition =
                _root.TransformPoint(_defaultLocalPosition);
            Collider[] hits = Physics.OverlapCapsule(_root.position,
                defaultCameraPosition, _radius, ~_playerLayer,
                QueryTriggerInteraction.Ignore);
            if (hits.Length > 0)
            {
                var offset = (_root.position - defaultCameraPosition).normalized 
                             * _radius / 2f;
                var avoidVector =
                    GetAvoidedVector(_root.position, defaultCameraPosition, offset);
                cameraLocalPosition =
                    _root.InverseTransformPoint(_root.position + avoidVector);
            }
            else
            {
                cameraLocalPosition = _defaultLocalPosition;
            }

            return cameraLocalPosition;
        }

        private Vector3 GetAvoidedVector(Vector3 start, Vector3 end, 
            Vector3 offset = default)
        {
            var direction = end - start;
            var ray = new Ray(start + offset , direction.normalized);
            if (Physics.SphereCast(ray, _radius, out var hit,
                    direction.magnitude, ~_playerLayer,
                    QueryTriggerInteraction.Ignore))
            {
                var startToHitVector = hit.point - start;
                var startToObstacleVector =
                    Vector3.Project(startToHitVector, direction.normalized);
                return startToObstacleVector;
            }
            
            return Vector3.zero;
        }
    }
}
