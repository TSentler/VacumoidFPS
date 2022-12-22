using System;
using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private MonoBehaviour[] _boostsBehaviour = new MonoBehaviour[0];
        
        private IBoostable[] _boosts;
        
        private void OnValidate()
        {
            if (PrefabChecker.InPrefabFileOrStage(gameObject))
                return;
            
            if (_temporaryBonus == null)
                Debug.LogWarning("TemporaryBonus was not found!", this);
            if (_garbageDisposal == null)
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