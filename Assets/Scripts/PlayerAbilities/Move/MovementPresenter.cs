using System;
using UnityEngine;

namespace PlayerAbilities.Move
{
    [RequireComponent(typeof(Animator))]
    public class MovementPresenter : MonoBehaviour
    {
        private readonly int _speedHash = Animator.StringToHash("Speed");
    
        [SerializeField] private TopDownMovement _topDownMovement;
        
        private Animator _animator;
        private Vector2 _direction;
    
        private void OnValidate()
        {
            if (_topDownMovement == null)
                Debug.LogWarning("Movement was not found!", this);
        }
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _topDownMovement.Moved += SetDirection;
        }

        private void OnDisable()
        {
            _topDownMovement.Moved -= SetDirection;
        }
        
        private void SetDirection(Vector2 _direction)
        {
            _animator.SetFloat(_speedHash, _direction.magnitude);
        }
    }
}
