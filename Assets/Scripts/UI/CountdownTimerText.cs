using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class CountdownTimerText : MonoBehaviour
    {
        private readonly int _oneMinute = 60;
        private readonly string _separator = ":";
        
        private TMP_Text _text;
        private int _time, _redLimit = 7;

        private string Template => $"{{0}}{_separator}{{1}}";
        
        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            if (_time > 0)
            {
                SetTime(_time);
            }
        }

        public void SetTime(int time)
        {
            _time = time;
            if (time <= _redLimit)
            {
                _text.color = Color.red;
            }
            int minutes = (int)Mathf.Floor(time / _oneMinute);
            int seconds = time - minutes * _oneMinute;
            _text.SetText(string.Format(Template, 
                minutes.ToString("00"), seconds.ToString("00")));
        }
    }
}
