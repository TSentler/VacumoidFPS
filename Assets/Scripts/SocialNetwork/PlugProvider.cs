using System;
using System.Collections;
using SocialNetwork.Interfaces;

namespace SocialNetwork
{
    public class PlugProvider : IInitialize, IMobileChecker
    {
        public IEnumerator Initialize(Action onSuccessCallback)
        {
            yield return null;
            onSuccessCallback?.Invoke();
        }

        public bool MobileCheck()
        {
            return false;
        }
    }
}