using System.Collections;
using UnityEngine;

namespace Trash
{
    public class SizeReducer : MonoBehaviour
    {
        [SerializeField] private float _multiplier = 0.1f, 
            _speed = 10f;

        private IEnumerator ReduceSizeCoroutine()
        {
            var startScale = transform.localScale;
            var endScale = startScale * _multiplier;
            var delta = 0f;
            while (delta < 1f)
            {
                delta += _speed * Time.deltaTime;
                transform.localScale =
                    Vector3.Lerp(startScale, endScale, delta);
                yield return null;
            }
        }
        
        public void Apply()
        {
            StartCoroutine(ReduceSizeCoroutine());
        }
    }
}
