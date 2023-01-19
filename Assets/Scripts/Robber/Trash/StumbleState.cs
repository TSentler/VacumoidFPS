using Trash;
using UnityEngine;

namespace Robber
{
    [RequireComponent(typeof(Animator))]
    public class StumbleState : MonoBehaviour
    {
        [SerializeField] private RobberAI _robberAI;
        [SerializeField] private VacuumGrabberActivator _vacuumActivator;
        
        private readonly int _stumbleName = Animator.StringToHash("Stumble");
        private Animator _animator;
        private GettingUpBehaviour _gettingUpBehaviour;
        private StumbleBehaviour _stumbleBehaviour;

        private void OnValidate()
        {
            if (_robberAI == null)
                Debug.LogWarning("Robber was not found!", this);
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _stumbleBehaviour =
                _animator.GetBehaviour<StumbleBehaviour>();
            _gettingUpBehaviour =
                _animator.GetBehaviour<GettingUpBehaviour>();
        }

        private void OnEnable()
        {
            _stumbleBehaviour.Started += OnStumbleStarted;
            _stumbleBehaviour.Ended += OnStumbleEnded;
            _gettingUpBehaviour.Ended += OnGettingUpEnded;
        }

        private void OnDisable()
        {
            _stumbleBehaviour.Started -= OnStumbleStarted;
            _stumbleBehaviour.Ended -= OnStumbleEnded;
            _gettingUpBehaviour.Ended -= OnGettingUpEnded;
        }

        private void OnStumbleStarted()
        {
            _vacuumActivator.Activate();
            _robberAI.UseGravity();
            _robberAI.DropTarget();
        }

        private void OnStumbleEnded()
        {
            _animator.ResetTrigger(_stumbleName);
        }
        
        private void OnGettingUpEnded()
        {
            _vacuumActivator.Deactivate();
        }
        
    }
}