using System.Collections;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ShowAnimateAlpha : MonoBehaviour
    {
        [Min(0f), SerializeField] private float _showTime = 0.3f;

        private CanvasGroup _group;
        private Coroutine _coroutine;
        
        private void Awake()
        {
            _group = GetComponent<CanvasGroup>();
        }

        private void OnEnable()
        {
            _group.alpha = 0f;
            _coroutine = StartCoroutine(AnimateAlphaCoroutine());
        }

        private void OnDisable()
        {
            StopCoroutine(_coroutine);
        }

        private IEnumerator AnimateAlphaCoroutine()
        {
            var elapsed = 0f;
            while (elapsed < _showTime)
            {
                elapsed += Time.deltaTime;
                var rate = elapsed / _showTime; 
                _group.alpha = Mathf.Lerp(0f, 1f, rate);
                yield return null;
            }
        }
    }
}
