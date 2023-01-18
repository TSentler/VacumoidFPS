using Trash;
using UnityEngine;

namespace Robber
{
    [RequireComponent(typeof(Animator))]
    public class GettingUpState : MonoBehaviour
    {
        [SerializeField] private VacuumGrabberActivator _vacuumActivator;
        
        private Animator _animator;

        private GettingUpBehaviour _gettingUpBehaviour;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _gettingUpBehaviour =
                _animator.GetBehaviour<GettingUpBehaviour>();
        }

        private void OnEnable()
        {
            _gettingUpBehaviour.Ended += OnGettingUpEnded;
        }

        private void OnDisable()
        {
            _gettingUpBehaviour.Ended -= OnGettingUpEnded;
        }

        private void OnGettingUpEnded()
        {
            _vacuumActivator.Deactivate();
        }
    }
}