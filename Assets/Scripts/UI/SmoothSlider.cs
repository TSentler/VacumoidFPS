using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Slider))]
    public class SmoothSlider : MonoBehaviour
    {
        [SerializeField] private float _duration = 1f;

        private Slider _slider;
        private Coroutine _slideCoroutine;
        private float _previous, _target, _elapsed;

        private bool InProgress => _elapsed < _duration;
        
        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.minValue = 0f;
            _slider.maxValue = 1f;
            _slider.value = _slider.minValue;
            _elapsed = _duration;
        }
    
        private void Update()
        {
            if (InProgress)
            {
                _slider.value = Mathf.Lerp(_previous, _target, 
                    _elapsed / _duration);
                _elapsed += Time.deltaTime;
            }
        }
    
        public void SetValue(float value)
        {
            _previous = _slider.value;
            _target = value;
            if (InProgress == false)
                _elapsed = 0f;
        }
    }
}
