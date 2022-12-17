using PlayerAbilities.Move;
using PlayerAbilities.Throw;
using Trash;
using Trash.Boosters;
using UnityEngine;
using Vacuum;

namespace Bonuses.Player
{
    public class LightningUpgrader : MonoBehaviour
    {
        [SerializeField] private TemporaryBonus _temporaryBonus;
        [SerializeField] private GarbageDisposal _garbageDisposal;
        [SerializeField] private GarbageSucker _garbageSucker;
        [SerializeField] private Movement _movement;
        [SerializeField] private SuckerBooster _sucker;
        [SerializeField] private ThrowTimer _throwTimer;

        private void OnValidate()
        {
            if (_temporaryBonus == null)
                Debug.LogWarning("TemporaryBonus was not found!", this);
            if (_garbageDisposal == null)
                Debug.LogWarning("GarbageDisposal was not found!", this);
            if (_garbageSucker == null)
                Debug.LogWarning("GarbageSucker was not found!", this);
            if (_movement == null)
                Debug.LogWarning("Movement was not found!", this);
            if (_sucker == null)
                Debug.LogWarning("GarbageSucker was not found!", this);
        }

        private void OnEnable()
        {
            _temporaryBonus.TimerStarted += OnTimerStarted;
            _temporaryBonus.TimerEnded += OnTimerEnded;
            _garbageDisposal.Collected += OnCollected;
        }
    
        private void OnDisable()
        {
            _temporaryBonus.TimerStarted -= OnTimerStarted;
            _temporaryBonus.TimerEnded -= OnTimerEnded;
            _garbageDisposal.Collected -= OnCollected;
        }

        private void OnTimerStarted()
        {
            _movement.BoostSpeed();
            _sucker.IncreaseSize();
            _garbageSucker.BoostSpeed();
            _throwTimer.BoostDelay();
        }

        private void OnTimerEnded()
        {
            _movement.ResetSpeed();
            _sucker.ResetSize();
            _garbageSucker.ResetSpeed();
            _throwTimer.ResetDelay();
        }
        
        private void OnCollected(Garbage garbage)
        {
            if (garbage.TryGetComponent<Lightning>(
                    out var lightningBonus))
            {
                _temporaryBonus.Apply();
            }
        }
    }
}