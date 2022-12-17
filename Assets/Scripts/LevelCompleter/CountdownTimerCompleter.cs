using UnityEngine;
using UnityEngine.Events;

namespace LevelCompleter
{
    [RequireComponent(typeof(Completer))]
    public class CountdownTimerCompleter : MonoBehaviour
    {
        private Completer _completer;
        private float _time;
        private int _oldTime = -1;
        private bool _isRun;
        private float _seconds = 33f;

        public event UnityAction<int> TimerChanged;
        
        private void Awake()
        {
            _completer = GetComponent<Completer>();
        }

        private void Start()
        {
            Run();
        }

        private void Update()
        {
            if (_isRun == false)
                return;

            _time -= Time.deltaTime;
            var wholeTime = Mathf.RoundToInt(_time);
            if (wholeTime != _oldTime)
            {
                _oldTime = wholeTime;
                TimerChanged?.Invoke(_oldTime);
            }
            
            if (_time > 0f)
                return;

            _isRun = false;
            _completer.Complete();
        }

        private void Run()
        {
            _time = _seconds;
            _isRun = true;
        }
    }
}
