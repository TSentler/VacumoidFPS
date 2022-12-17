using UnityEngine;

namespace Robber
{
    public class HudActivator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _hud;
        
        private FlipToExitBehaviour _flipToExitBehaviour;
        private RunToTargetBehaviour _runToTargetBehaviour;
        private SuckBehaviour _suckBehaviour;

        private void OnValidate()
        {
            if (_animator == null)
                Debug.LogWarning("Animator was not found!", this);
            if (_hud == null)
                Debug.LogWarning("HUD was not found!", this);
        }

        private void Awake()
        {
            _flipToExitBehaviour = 
                _animator.GetBehaviour<FlipToExitBehaviour>();
            _runToTargetBehaviour = 
                _animator.GetBehaviour<RunToTargetBehaviour>();
            _suckBehaviour = 
                _animator.GetBehaviour<SuckBehaviour>();
        }

        private void OnEnable()
        {
            _runToTargetBehaviour.Started += Activate;
            _flipToExitBehaviour.Started += Deactivate;
            _suckBehaviour.Started += Deactivate;
        }

        private void OnDisable()
        {
            _runToTargetBehaviour.Started -= Activate;
            _flipToExitBehaviour.Started -= Deactivate;
            _suckBehaviour.Started -= Deactivate;
        }

        private void Activate()
        {
            _hud.SetActive(true);
        }

        private void Deactivate()
        {
            _hud.SetActive(false);
        }
    }
}