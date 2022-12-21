using PlayerAbilities.Move;
using UnityEngine;
using UnityTools;

namespace PlayerAbilities.Throw
{
    [RequireComponent(typeof(VacuumThrower))]
    public class ThrowPreparer : MonoBehaviour
    {
        private VacuumThrower _vacuumThrower;
        
        [SerializeField] private Movement _movement;

        private void OnValidate()
        {
            if (PrefabChecker.InPrefabFileOrStage(gameObject))
                return;
            
            if (_movement == null)
                Debug.LogWarning("Movement was not found!", this);
        }

        private void Awake()
        {
            _vacuumThrower = GetComponent<VacuumThrower>();
        }

        private void OnEnable()
        {
            _vacuumThrower.Tied += _movement.Stop;
            _vacuumThrower.Throwed += _movement.Go;
        }

        private void OnDisable()
        {
            _vacuumThrower.Tied -= _movement.Stop;
            _vacuumThrower.Throwed -= _movement.Go;
        }
    }
}
