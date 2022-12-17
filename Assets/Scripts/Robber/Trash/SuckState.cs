using Trash;
using UnityEngine;

namespace Robber
{
    [RequireComponent(typeof(Animator),
        typeof(Garbage))]
    public class SuckState : MonoBehaviour
    {
        private readonly int _suckName = Animator.StringToHash("Suck");
        
        [SerializeField] private RobberAI _robberAI;
        
        private Animator _animator;
        private SuckBehaviour _suckBehaviour;
        private Garbage _garbage;

        private void OnValidate()
        {
            if (_robberAI == null)
                Debug.LogWarning("Robber was not found!", this);
        }
        
        private void Awake()
        {
            _animator = GetComponent<Animator>(); 
            _suckBehaviour = _animator.GetBehaviour<SuckBehaviour>();
            _garbage = GetComponent<Garbage>();
        }

        private void OnEnable()
        {
            _garbage.SuckStarted += OnSuckStarted;
            _suckBehaviour.Started += OnAnimationSuckStarted;
        }

        private void OnDisable()
        {
            _garbage.SuckStarted -= OnSuckStarted;
            _suckBehaviour.Started -= OnAnimationSuckStarted;
        }

        private void OnSuckStarted()
        {
            _robberAI.DropTarget();
            _animator.SetTrigger(_suckName);
        }

        private void OnAnimationSuckStarted()
        {
            gameObject.SetActive(true);
            _robberAI.UseKinematic();
        }
    }
}
