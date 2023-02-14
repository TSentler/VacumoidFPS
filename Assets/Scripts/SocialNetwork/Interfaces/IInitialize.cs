using System;
using System.Collections;

namespace SocialNetwork.Interfaces
{
    public interface IInitialize
    {
        public IEnumerator Initialize(Action onSuccessCallback);
    }

    public interface IMobileChecker
    {
        public bool MobileCheck();
    }
}