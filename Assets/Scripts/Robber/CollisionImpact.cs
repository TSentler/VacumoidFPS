using System.Linq;
using UnityEngine;

namespace Robber
{
    [RequireComponent(typeof(Animator))]
    public class CollisionImpact : MonoBehaviour
    {
        private readonly int _stumbleName = Animator.StringToHash("Stumble");
        private readonly int[] _stumbleAnimationsReady =
        {
            Animator.StringToHash("Idle"),
            Animator.StringToHash("Run"),
            Animator.StringToHash("RunWithBox"),
            Animator.StringToHash("GettingUp"),
            Animator.StringToHash("StumbleBackwards"),
            Animator.StringToHash("ToRoll"),
        };
        
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
                if (_stumbleAnimationsReady.Contains(animationHash))
                {
                    _animator.SetTrigger(_stumbleName);
                }
            }
        }
        
    }
}
