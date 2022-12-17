using UnityEngine;

namespace Robber
{
    [RequireComponent(typeof(Animator),
        typeof(Rigidbody))]
    public class SuccessfulTheftState : MonoBehaviour
    {
        private SuccessfulTheftBehaviour _successfulTheftBehaviour;
        private Animator _animator;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>(); 
            _successfulTheftBehaviour = 
                _animator.GetBehaviour<SuccessfulTheftBehaviour>();
        }

        private void OnEnable()
        {
            _successfulTheftBehaviour.Started += OnSuccessfulTheftStarted;
        }

        private void OnDisable()
        {
            _successfulTheftBehaviour.Started -= OnSuccessfulTheftStarted;
        }

        private void OnSuccessfulTheftStarted()
        {
            gameObject.SetActive(false);
        }
    }
}
