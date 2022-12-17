using UnityEngine;
using UnityTools;

namespace PlayerAbilities.Throw
{
    [RequireComponent(typeof(VacuumThrower))]
    public class ThrowPresenter : MonoBehaviour
    {
        private readonly int 
            _isThrowPrepareHash = Animator.StringToHash("IsThrowPrepare"),
            _throwPrepareHash = Animator.StringToHash("ThrowPrepare"),
            _throwHash = Animator.StringToHash("Throw");
        
        [SerializeField] private Transform _vacuumStickTransform;
        [SerializeField] private Animator _cleanerAnimator, _vacuumBoxAnimator,
            _vacuumStickAnimator;
    
        private VacuumThrower _vacuumThrower;
        
        private void OnValidate()
        {
            if (PrefabChecker.InPrefabFileOrStage(gameObject))
                return;
            
            if (_vacuumStickTransform == null)
                Debug.LogWarning("Transform was not found!", this);
            if (_cleanerAnimator == null || _vacuumBoxAnimator == null 
                                         || _vacuumStickAnimator == null)
                Debug.LogWarning("Animator was not found!", this);
        }
        
        private void Awake()
        {
            _vacuumThrower = GetComponent<VacuumThrower>();
        }
        
        private void OnEnable()
        {
            _vacuumThrower.Tied += TiedHandler;
            _vacuumThrower.Throwed += ThrowedHandler;
        }

        private void OnDisable()
        {
            _vacuumThrower.Tied -= TiedHandler;
            _vacuumThrower.Throwed -= ThrowedHandler;
        }
        
        private void TiedHandler()
        {
            _cleanerAnimator.ResetTrigger(_throwHash);
            _cleanerAnimator.SetTrigger(_throwPrepareHash);
            _vacuumBoxAnimator.SetBool(_isThrowPrepareHash, true);
            _vacuumStickAnimator.SetBool(_isThrowPrepareHash, true);
        }

        private void ThrowedHandler()
        {
            _cleanerAnimator.ResetTrigger(_throwPrepareHash);
            _cleanerAnimator.SetTrigger(_throwHash);
            _vacuumBoxAnimator.SetBool(_isThrowPrepareHash, false);
            _vacuumStickAnimator.SetBool(_isThrowPrepareHash, false);
        }
    }
}
