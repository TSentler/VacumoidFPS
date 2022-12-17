using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Bonuses
{
    public class TemporaryBonus : MonoBehaviour
    {
        [SerializeField] private float _duration = 1f;

        private float _timePassed = float.MaxValue;

        public event UnityAction TimerStarted, TimerEnded;
        public event UnityAction<float> TimerChanged;

        private bool IsRun => _timePassed < _duration;
        
        private IEnumerator BonusCoroutine()
        {
            TimerStarted?.Invoke();
            _timePassed = 0f;
            while (IsRun)
            {
                yield return null;
                _timePassed += Time.deltaTime;
                var ratio = _timePassed / _duration;
                TimerChanged?.Invoke(ratio);
            }
            TimerEnded?.Invoke();
        }
        
        public void Apply()
        {
            if (IsRun)
            {
                _timePassed = 0f;
            }
            else
            {
                StartCoroutine(BonusCoroutine());
            }
        } 
    }
}
