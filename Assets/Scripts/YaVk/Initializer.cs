using System.Collections;
using Agava.VKGames;
using Agava.YandexGames;
using UnityEngine;

namespace YaVk
{
    public class Initializer : MonoBehaviour
    {
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
            if (Defines.IsUnityWebGl == false
                || Defines.IsUnityEditor)
            {
                InitializeComplete();
            }
            else if (Defines.IsYandexGames)
            {
                yield return YandexGamesSdk.Initialize(
                    onSuccessCallback: InitializeComplete);
            }
            else if (Defines.IsVkGames)
            {
                yield return VKGamesSdk.Initialize(
                    onSuccessCallback: InitializeComplete);
            }
            else
            {
                InitializeComplete();
            }
        }
        
        private void InitializeComplete()
        {
            _isInitialized = true;
            _isInitializeRun = false;
        }
    }
}
