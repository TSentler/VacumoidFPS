using System;
using UnityEngine;

namespace PlayerAbilities.Move
{
    [RequireComponent(typeof(Animator))]
    public class MovementPresenter : MonoBehaviour
    {
        private readonly int _speedHash = Animator.StringToHash("Speed");
    
        [SerializeField] private Movement _movement;
        
        private Animator _animator;
        private Vector2 _direction;
    
        private void OnValidate()
        {
            if (_movement == null)
                Debug.LogWarning("Movement was not found!", this);
        }
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _movement.Moved += SetDirection;
        }

        private void OnDisable()
        {
            _movement.Moved -= SetDirection;
        }
        
        private void SetDirection(Vector2 _direction)
        {
            _animator.SetFloat(_speedHash, _direction.magnitude);
        }
    }
}
