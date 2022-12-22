using System;
using System.Linq;
using UnityEngine;
using UnityTools;

namespace Bonuses.Player
{
    public class LightningUpgrader : MonoBehaviour
    {
        [SerializeField] private TemporaryBonus _temporaryBonus;
        [SerializeField] private BonusDisposal _bonusDisposal;
        [SerializeField] private MonoBehaviour[] _boostsBehaviour = Array.Empty<MonoBehaviour>();
        
        private IBoostable[] _boosts;
        
        private void OnValidate()
        {
            if (PrefabChecker.InPrefabFileOrStage(gameObject))
                return;
            
            if (_temporaryBonus == null)
                Debug.LogWarning("TemporaryBonus was not found!", this);
            if (_bonusDisposal == null)
                Debug.LogWarning("GarbageDisposal was not found!", this);
            if (_boostsBehaviour.Length > 0)
            {
                for (int i = 0; i < _boostsBehaviour.Length; i++)
                {
                    if (_boostsBehaviour[i] 
                        && !(_boostsBehaviour[i] is IBoostable) )
                    {
                        Debug.LogError(nameof(_boostsBehaviour) + " needs to implement " + nameof(IBoostable));
                        _boostsBehaviour[i] = null;
                    }
                }

                if (_boostsBehaviour.Any(item => item == null))
                {
                    Debug.LogError(nameof(_boostsBehaviour) + " contains null element");
                }
            }
        }

        private void Awake()
        {
            _boosts = _boostsBehaviour.OfType<IBoostable>().ToArray();
        }

        private void OnEnable()
        {
            _temporaryBonus.TimerStarted += OnTimerStarted;
            _temporaryBonus.TimerEnded += OnTimerEnded;
            _bonusDisposal.Boosted += OnBoosted;
        }
    
        private void OnDisable()
        {
            _temporaryBonus.TimerStarted -= OnTimerStarted;
            _temporaryBonus.TimerEnded -= OnTimerEnded;
            _bonusDisposal.Boosted -= OnBoosted;
        }

        private void OnTimerStarted()
        {
            foreach (var boostable in _boosts)
            {
                boostable.Boost();
            }
        }

        private void OnTimerEnded()
        {
            foreach (var boostable in _boosts)
            {
                boostable.ResetBoost();
            }
        }
        
        private void OnBoosted(Lightning lightningBonus)
        {
            _temporaryBonus.Apply();
        }
    }
}