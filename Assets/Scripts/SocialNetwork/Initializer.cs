using System.Collections;
using SocialNetwork.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace SocialNetwork
{
    public class Initializer
    {
        public Initializer(IInitialize provider)
        {
            _platformProvider = provider;
        }

        private readonly IInitialize _platformProvider;
        
        private bool _isInitialized,
            _isInitializeRun;

        public IEnumerator TryInitializeSdkCoroutine()
        {
            if (_isInitialized)
                yield break;
            
            if (_isInitializeRun)
            {
                while (_isInitialized == false && _isInitializeRun)
                {
                    yield return new WaitForSecondsRealtime(0.2f);
                }

                if (_isInitialized)
                    yield break;
            }
            
            _isInitializeRun = true;
            yield return _platformProvider.Initialize(InitializeComplete);
        }
        
        private void InitializeComplete()
        {
            _isInitialized = true;
            _isInitializeRun = false;
        }
    }
}
