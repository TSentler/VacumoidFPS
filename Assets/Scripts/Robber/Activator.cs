using System;
using System.Collections;
using UnityEngine;
using UnityTools;
using Random = UnityEngine.Random;

namespace Robber
{
    public class Activator : MonoBehaviour
    {
        private static int _activateCount;
        
        [SerializeField] private RobberAI _robberAI;
        [SerializeField] private GameObject _signalings;
        [Min(0f), SerializeField] private float _minSeconds = 3f, 
            _maxSeconds = 6f;
        
        private float _seconds;

        private void OnValidate()
        {
            if (PrefabChecker.InPrefabFileOrStage(gameObject))
                return;
            
            if (_robberAI == null)
                Debug.LogWarning("Robber gameObject was not found!", this);
            if (_signalings == null)
                Debug.LogWarning("Signalings gameObject was not found!", this);
        }
        
        private void Awake()
        {
            _signalings.SetActive(false);
            _seconds = Random.Range(_minSeconds, _maxSeconds);
            StartCoroutine(WaitCoroutine());
        }
        
        private IEnumerator WaitCoroutine()
        {
            yield return new WaitForSeconds(_seconds);
            _robberAI.gameObject.SetActive(true);
            ActivateSignaling();
        }

        private void ActivateSignaling()
        {
            _signalings.SetActive(true);
            _activateCount++;
            _robberAI.Deactivated += () =>
            {
                _activateCount--;
                if (_activateCount == 0)
                {
                    _signalings.SetActive(false);
                }
            };
        }
    }
}
