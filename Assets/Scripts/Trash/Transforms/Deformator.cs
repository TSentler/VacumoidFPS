using System.Collections;
using UnityEngine;

namespace Trash
{
    public class Deformator : MonoBehaviour
    {
        [SerializeField] private Transform _scaleCenter;
        [Min(0.001f), SerializeField] private float _time = 0.2f, 
            _multiplier = 2f;
        
        private void OnValidate()
        {
            if (_scaleCenter == null)
            {
                _scaleCenter = transform;
                Debug.LogWarning("ScaleCenter was not found!", this);
            }
        }
        
        private IEnumerator DeformationCoroutine()
        {
            GetDeformScales(out var startScale, out var endScale);
            var time = 0f;
            while (time < _time)
            {
                var elapsed = time / _time;
                ResizeScaleCenter(startScale, endScale, elapsed);
                yield return null;
                time += Time.deltaTime;
            }
        }

        private void GetDeformScales(out Vector3 startScale, 
            out Vector3 endScale)
        {
            SetChildRoot(transform, _scaleCenter);
            startScale = _scaleCenter.localScale;
            SetChildRoot(_scaleCenter, transform);
            endScale = startScale;
            endScale.z *= _multiplier;
        }

        private void ResizeScaleCenter(Vector3 startScale, Vector3 endScale, 
            float elapsed)
        {
            SetChildRoot(transform, _scaleCenter);
            _scaleCenter.localScale = 
                Vector3.Lerp(startScale, endScale, elapsed);
            SetChildRoot(_scaleCenter, transform);
        }

        private void SetChildRoot(Transform root, Transform child)
        {
            child.parent = root.parent;
            root.parent = child;
        }

        public Coroutine Apply()
        {
            return StartCoroutine(DeformationCoroutine());
        }
    }
}
