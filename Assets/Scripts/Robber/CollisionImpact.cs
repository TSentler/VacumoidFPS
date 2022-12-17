using UnityEngine;

namespace Robber
{
    [RequireComponent(typeof(Animator))]
    public class CollisionImpact : MonoBehaviour
    {
        private readonly int _stumbleName = Animator.StringToHash("Stumble"),
            _idleName = Animator.StringToHash("Idle"),
            _runName = Animator.StringToHash("Run"),
            _runWithBoxName = Animator.StringToHash("RunWithBox");
        
        [SerializeField] private float _maxVelocity = 15f;
        
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.rigidbody != null
                && collision.rigidbody.velocity.sqrMagnitude > _maxVelocity)
            {
                var animationHash = _animator.
                    GetCurrentAnimatorStateInfo(0).shortNameHash;
                if (animationHash == _runName || animationHash == _runWithBoxName
                    || animationHash == _idleName)
                {
                    _animator.SetTrigger(_stumbleName);
                }
            }
        }
        
    }
}
