using UnityEngine;

namespace Robber
{
    [RequireComponent(typeof(Animator))]
    public class StumbleState : MonoBehaviour
    {
        [SerializeField] private RobberAI _robberAI;
        private readonly int _stumbleName = Animator.StringToHash("Stumble");
        private Animator _animator;

        private StumbleBehaviour _stumbleBehaviour;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _stumbleBehaviour =
                _animator.GetBehaviour<StumbleBehaviour>();
        }

        private void OnEnable()
        {
            _stumbleBehaviour.Started += OnStumbleStarted;
            _stumbleBehaviour.Ended += OnStumbleEnded;
        }

        private void OnDisable()
        {
            _stumbleBehaviour.Started -= OnStumbleStarted;
            _stumbleBehaviour.Ended -= OnStumbleEnded;
        }

        private void OnValidate()
        {
            if (_robberAI == null)
                Debug.LogWarning("Robber was not found!", this);
        }

        private void OnStumbleStarted()
        {
            _robberAI.UseGravity();
            _robberAI.DropTarget();
        }

        private void OnStumbleEnded()
        {
            _animator.ResetTrigger(_stumbleName);
        }
    }
}