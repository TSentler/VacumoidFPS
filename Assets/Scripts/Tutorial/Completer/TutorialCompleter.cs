using System.Collections;
using LevelCompleter;
using Trash;
using UnityEngine;

namespace Tutorial
{
    public class TutorialCompleter : MonoBehaviour
    {
        [SerializeField] private Garbage _robber;
        [SerializeField] private Completer _completer;

        private Coroutine _coroutine;
        
        private void OnValidate()
        {
            if (_robber == null)
                Debug.LogWarning("Robber was not found!", this);
            if (_completer == null)
                Debug.LogWarning("Completer was not found!", this);
        }
        
        private void OnEnable()
        {
            _robber.SuckStarted += SuckStartedHandler;
        }

        private void OnDisable()
        {
            _robber.SuckStarted -= SuckStartedHandler;
        }

        private IEnumerator CompleteCoroutine()
        {
            yield return new WaitForSeconds(1f);
            _completer.Complete();
        }
        
        private void SuckStartedHandler()
        {
            if (_coroutine != null)
                return;
            
            _coroutine = StartCoroutine(CompleteCoroutine());
        }
    }
}
