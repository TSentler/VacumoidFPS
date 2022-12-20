using PlayerAbilities.Move;
using UnityEngine;

namespace Robber
{
    [RequireComponent(typeof(TopDownMovement),
        typeof(Animator))]
    public class RunToExitState : MonoBehaviour
    {
        private readonly int _flipToExitName = Animator.StringToHash("FlipToExit");
        
        [SerializeField] private RobberAI _robberAI;
        [Min(0.1f), SerializeField] private float _minDistance = 1f;
        
        private RunToExitBehaviour _runToExitBehaviour;
        private TopDownMovement _topDownMovement;
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
            _runToExitBehaviour = _animator.GetBehaviour<RunToExitBehaviour>();
            _topDownMovement = GetComponent<TopDownMovement>();
        }

        private void OnEnable()
        {
            _runToExitBehaviour.Ended += OnRunToExitEnded;
            _runToExitBehaviour.Updated += OnRunToExitUpdated;
        }

        private void OnDisable()
        {
            _runToExitBehaviour.Ended -= OnRunToExitEnded;
            _runToExitBehaviour.Updated -= OnRunToExitUpdated;
        }

        private void OnRunToExitEnded()
        {
            _topDownMovement.Move(Vector2.zero);
        }
        
        private void OnRunToExitUpdated()
        {
            var direction = _robberAI.GetDirectionToExit();
            _topDownMovement.Move(direction.normalized);
            if (direction.magnitude < _minDistance)
            {
                _topDownMovement.Move(Vector2.zero);
                transform.rotation = Quaternion.LookRotation(
                    new Vector3(direction.x, 0f, direction.y));
                 
                _animator.SetTrigger(_flipToExitName);
            }
        }
    }
}
