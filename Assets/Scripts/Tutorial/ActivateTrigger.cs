using UnityEngine;
using UnityEngine.Events;

namespace Tutorial
{
    public class ActivateTrigger : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnTrigger;
        
        private bool _isActivated;
        
        private void OnTriggerEnter(Collider other)
        {
            if (_isActivated)
                return;
            
            if (other.TryGetComponent(out PlayerTrigger player))
            {
                _isActivated = true;
                OnTrigger?.Invoke();
            }
        }
    }
}
