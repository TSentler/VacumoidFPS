using UnityEngine;

namespace PlayerCamera
{
    public class ObstaclesAvoider : MonoBehaviour
    {
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private Transform _root;
        [Min(0f), SerializeField] private float _radius = 0.5f, _smooth = 20f;

        private Vector3 _defaultLocalPosition, _currentLocalPosition;
        private Collider[] _hitBuffer = new Collider[1];
        private float _overlapStepFactor = 3f;

        private void Awake()
        {
            _defaultLocalPosition = transform.localPosition;
        }

        private void Update()
        {
            var targetLocalPosition = GetCameraLocalPosition();

            Move(targetLocalPosition);
        }

        private void Move(Vector3 targetLocalPosition)
        {
            if (IsArrived(targetLocalPosition))
                return;

            var step = _smooth * Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(
                transform.localPosition, targetLocalPosition, step);

            while (Physics.OverlapSphereNonAlloc(transform.position, _radius,
                       _hitBuffer, ~_playerLayer, QueryTriggerInteraction.Ignore) > 0)
            {
                transform.localPosition = Vector3.MoveTowards(
                    transform.localPosition, targetLocalPosition, 
                    step * _overlapStepFactor);

                if (IsArrived(targetLocalPosition))
                    return;
            }
        }

        private void SimpleMove(Vector3 targetLocalPosition)
        {
            if (IsArrived(targetLocalPosition))
                return;
            
            var step = _smooth * Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(
                transform.localPosition, targetLocalPosition, step);
        }

        private bool IsArrived(Vector3 targetLocalPosition)
        {
            return (targetLocalPosition - transform.localPosition).magnitude < 0.001f;
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
                var avoidVector =
                    GetAvoidedVector(_root.position, defaultCameraPosition,
                        _radius);
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
            float radius)
        {
            var startOffset = (start - end).normalized 
                         * radius / 2f;
            var direction = end - (start + startOffset);
            var ray = new Ray(start + startOffset, direction.normalized);
            if (Physics.SphereCast(ray, radius, out var hit,
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
