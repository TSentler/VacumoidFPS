using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class CountDown : MonoBehaviour
    {
        [Min(0f), SerializeField] private float _time = 0.3f;

        private TextMeshProUGUI _text;
        private Coroutine _coroutine;
        private float _elapsed = float.MaxValue;
        private int _startNumber, _target;
        
        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            if (int.TryParse(_text.text, out var number) == false)
                Debug.LogWarning("Text is not integer!", this);
        }

        private void Update()
        {
            if (_elapsed < _time)
            {
                _elapsed += Time.deltaTime;
                var rate = _elapsed / _time;
                var i = (int)Mathf.Lerp(_startNumber, _target, rate);
                _text.SetText(i.ToString());
            }
        }

        public void Apply(int target)
        {
            _target = target;
            if (int.TryParse(_text.text, out _startNumber))
            {
                _elapsed = 0f;
            }
        }
    }
}
