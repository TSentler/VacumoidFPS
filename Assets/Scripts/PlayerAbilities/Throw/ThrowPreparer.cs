using PlayerAbilities.Move;
using UnityEngine;

namespace PlayerAbilities.Throw
{
    [RequireComponent(typeof(VacuumThrower))]
    public class ThrowPreparer : MonoBehaviour
    {
        private VacuumThrower _vacuumThrower;
        
        [SerializeField] private TopDownMovement _topDownMovement;

        private void OnValidate()
        {
            if (_topDownMovement == null)
                Debug.LogWarning("Movement was not found!", this);
        }

        private void Awake()
        {
            _vacuumThrower = GetComponent<VacuumThrower>();
        }

        private void OnEnable()
        {
            _vacuumThrower.Tied += _topDownMovement.Stop;
            _vacuumThrower.Throwed += _topDownMovement.Go;
        }

        private void OnDisable()
        {
            _vacuumThrower.Tied -= _topDownMovement.Stop;
            _vacuumThrower.Throwed -= _topDownMovement.Go;

        }
    }
}
