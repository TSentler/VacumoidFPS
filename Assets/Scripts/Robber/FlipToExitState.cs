using UnityEngine;

namespace Robber
{
    [RequireComponent(typeof(Animator),
        typeof(RobberAI))]
    public class FlipToExitState : MonoBehaviour
    {
        private FlipToExitBehaviour _flipToExitBehaviour;
        private Animator _animator;
        private RobberAI _robberAI;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>(); 
            _flipToExitBehaviour = 
                _animator.GetBehaviour<FlipToExitBehaviour>();
            _robberAI = GetComponent<RobberAI>(); 
        }

        private void OnEnable()
        {
            _flipToExitBehaviour.Started += OnFlipToExitStarted;
        }

        private void OnDisable()
        {
            _flipToExitBehaviour.Started -= OnFlipToExitStarted;
        }

        private void OnFlipToExitStarted()
        {
            _robberAI.UseKinematic();
        }
    }
}
