using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerAbilities.Throw
{
    [RequireComponent(typeof(VacuumThrower))]
    public class ThrowTimer : MonoBehaviour
    {
        private VacuumThrower _vacuumThrower;
        private Coroutine _timerCoroutine;
        private float _timePassed = float.MaxValue, 
            _oldDelay;
        private bool _wasFx;
        
        [SerializeField] private float _delayBeforeFx, _delay = 1f, _boostDelay = 0.25f;

        public event UnityAction FxStarted;
        
        private bool IsRun => _timePassed < _delay;
        
        private void Awake()
        {
            _vacuumThrower = GetComponent<VacuumThrower>();
            _oldDelay = _delay;
        }
        
        private void OnEnable()
        {
            _vacuumThrower.Tied += OnTied;
            _vacuumThrower.Throwed += OnThrowed;
        }

        private void OnDisable()
        {
            _vacuumThrower.Tied -= OnTied;
            _vacuumThrower.Throwed -= OnThrowed;
        }
        
        private IEnumerator TimerCoroutine()
        {
            _timePassed = 0f;
            _wasFx = false;
            var fxDelay = _delay - _delayBeforeFx;
            while (IsRun)
            {
                yield return null;
                _timePassed += Time.deltaTime;
                if (fxDelay < _timePassed && _wasFx == false)
                {
                    _wasFx = true;
                    FxStarted?.Invoke();
                }
            }

            _timerCoroutine = null;
            _vacuumThrower.Throw();
        }
        
        private void OnTied()
        {
            _timerCoroutine = StartCoroutine(TimerCoroutine());
        }
        
        private void OnThrowed()
        {
            if (_timerCoroutine != null)
            {
                StopCoroutine(_timerCoroutine);
            }
        }

        public void BoostDelay()
        {
            _delay = _boostDelay;
            
        }

        public void ResetDelay()
        {
            _delay = _oldDelay;
        }
    }
}