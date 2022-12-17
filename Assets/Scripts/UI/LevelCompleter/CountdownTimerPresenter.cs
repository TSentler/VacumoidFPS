using LevelCompleter;
using UnityEngine;
using UnityTools;

namespace UI.LevelCompleter
{
    [RequireComponent(typeof(CountdownTimerCompleter))]
    public class CountdownTimerPresenter : MonoBehaviour
    {
        [SerializeField] private CountdownTimerText _timerText;
        
        private CountdownTimerCompleter _timer;

        private void OnValidate()
        {
            if (PrefabChecker.InPrefabFileOrStage(gameObject))
                return;
            
            if (_timerText == null)
                Debug.LogWarning("CountdownTimerText was not found!", this);
        }

        private void Awake()
        {
            _timer = GetComponent<CountdownTimerCompleter>();
        }

        private void OnEnable()
        {
            _timer.TimerChanged += OnTimerTimerChanged;
        }

        private void OnDisable()
        {
            _timer.TimerChanged -= OnTimerTimerChanged;
        }

        private void OnTimerTimerChanged(int time)
        {
            _timerText.SetTime(time);
        }
    }
}
