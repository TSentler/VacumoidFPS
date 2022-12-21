using PlayerAbilities;
using PlayerAbilities.Throw;
using Trash;
using UnityEngine;
using UnityTools;
using Vacuum;

namespace Bonuses.Player
{
    public class LightningUpgrader : MonoBehaviour
    {
        [SerializeField] private TemporaryBonus _temporaryBonus;
        [SerializeField] private GarbageDisposal _garbageDisposal;
        [SerializeField] private GarbageSucker _garbageSucker;
        [SerializeField] private SpeedStat _speedStat;
        [SerializeField] private VacuumRadius _sucker;
        [SerializeField] private ThrowTimer _throwTimer;

        private void OnValidate()
        {
            if (PrefabChecker.InPrefabFileOrStage(gameObject))
                return;
            
            if (_temporaryBonus == null)
                Debug.LogWarning("TemporaryBonus was not found!", this);
            if (_garbageDisposal == null)
                Debug.LogWarning("GarbageDisposal was not found!", this);
            if (_garbageSucker == null)
                Debug.LogWarning("GarbageSucker was not found!", this);
            if (_speedStat == null)
                Debug.LogWarning("SpeedStat was not found!", this);
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
            _speedStat.Boost();
            _sucker.Boost();
            _garbageSucker.Boost();
            _throwTimer.Boost();
        }

        private void OnTimerEnded()
        {
            _speedStat.ResetBoost();
            _sucker.ResetBoost();
            _garbageSucker.ResetBoost();
            _throwTimer.ResetBoost();
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