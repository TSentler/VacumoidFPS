using PlayerAbilities.Move;
using UnityEngine;

namespace Robber
{
    [RequireComponent(typeof(Movement),
        typeof(Animator))]
    public class RunToTargetState : MonoBehaviour
    {
        [SerializeField] private RobberAI _robberAI;
        [Min(0.1f), SerializeField] private float _minDistance = 1f;
        
        private RunToTargetBehaviour _runToTargetBehaviour;
        private Movement _movement;
        private Animator _animator;
        private Vector2 _runDirection;
        
        private void OnValidate()
        {
            if (_robberAI == null)
                Debug.LogWarning("Robber was not found!", this);
        }
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _runToTargetBehaviour = _animator.GetBehaviour<RunToTargetBehaviour>();
            _movement = GetComponent<Movement>();
        }

        private void OnEnable()
        {
            _runToTargetBehaviour.Started += OnRunToTargetStarted;
            _runToTargetBehaviour.Updated += OnRunToTargetUpdated;
            _runToTargetBehaviour.Ended += OnRunToTargetEnded;
        }

        private void OnDisable()
        {
            _runToTargetBehaviour.Started -= OnRunToTargetStarted;
            _runToTargetBehaviour.Updated -= OnRunToTargetUpdated;
            _runToTargetBehaviour.Ended -= OnRunToTargetEnded;
        }

        private void OnRunToTargetStarted()
        {
            _robberAI.UseGravity();
        }
        
        private void OnRunToTargetUpdated()
        {
            var direction = _robberAI.GetDirectionToTarget();
            _movement.Move(direction.normalized);
            if (direction.magnitude < _minDistance)
            {
                _movement.Move(Vector2.zero);
                _robberAI.PickUpTarget();
            }
        }

        private void OnRunToTargetEnded()
        {
            _movement.Move(Vector2.zero);
        }
    }
}
