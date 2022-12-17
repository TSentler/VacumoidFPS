using System;
using System.Collections;
using UnityEngine;

namespace Trash.Transforms
{
    [RequireComponent(typeof(Garbage))]
    public class LookAtRotator : MonoBehaviour
    {
        private Coroutine _lookAtCoroutine;
        private Garbage _garbage;

        private void Awake()
        {
            _garbage = GetComponent<Garbage>();
        }

        private void OnEnable()
        {
            _garbage.SuckStarted += OnSuckStarted;
        }

        private void OnDisable()
        {
            _garbage.SuckStarted -= OnSuckStarted;
        }
        
        private IEnumerator LookAtCoroutine()
        {
            while (_garbage.Target != null)
            {
                LookAtHandler(_garbage.Target.transform);
                yield return null;
            }

            _lookAtCoroutine = null;
        }
        
        protected virtual void LookAtHandler(Transform target)
        {
            transform.LookAt(target);
        }

        private void OnSuckStarted()
        {
            if (_lookAtCoroutine == null)
            {
                _lookAtCoroutine = StartCoroutine(LookAtCoroutine());
            }
        }
    }
}
