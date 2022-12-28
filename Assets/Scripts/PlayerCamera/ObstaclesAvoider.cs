using UnityEngine;
using UnityEngine.Events;

namespace PlayerCamera
{
    [RequireComponent(typeof(MaxDistance))]
    public class ObstaclesAvoider : MonoBehaviour
    {
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private Transform _root;
        [Min(0f), SerializeField] private float _radius = 0.5f, _speed = 20f;

        private Vector3 _defaultMaxLocalPosition;
        private Collider[] _hitBuffer = new Collider[1];
        private float _overlapSpeedFactor = 3f;
        private MaxDistance _maxDistanceFactor;

        public event UnityAction Moved;
                
        private Vector3 MaxLocalPosition => _defaultMaxLocalPosition * _maxDistanceFactor;
        
        private void Awake()
        {
            _maxDistanceFactor = GetComponent<MaxDistance>();
            _defaultMaxLocalPosition = transform.localPosition;
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

            var step = _speed * Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(
                transform.localPosition, targetLocalPosition, step);

            while (Physics.OverlapSphereNonAlloc(transform.position, _radius,
                       _hitBuffer, ~_playerLayer,
                       QueryTriggerInteraction.Ignore) > 0
                   && IsArrived(targetLocalPosition) == false)
            {
                transform.localPosition = Vector3.MoveTowards(
                    transform.localPosition, targetLocalPosition, 
                    step * _overlapSpeedFactor);
            }
            
            Moved?.Invoke();
        }

        private void SimpleMove(Vector3 targetLocalPosition)
        {
            if (IsArrived(targetLocalPosition))
                return;
            
            var step = _speed * Time.deltaTime;
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
            var maxCameraPosition =
                _root.TransformPoint(MaxLocalPosition);
            Collider[] hits = Physics.OverlapCapsule(_root.position,
                maxCameraPosition, _radius, ~_playerLayer,
                QueryTriggerInteraction.Ignore);
            if (hits.Length > 0)
            {
                var avoidVector =
                    GetAvoidedVector(_root.position, maxCameraPosition,
                        _radius);
                cameraLocalPosition =
                    _root.InverseTransformPoint(_root.position + avoidVector);
            }
            else
            {
                cameraLocalPosition = MaxLocalPosition;
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
