using System;
using System.Collections;
using YaVk.Interfaces;

namespace YaVk
{
    public class PlugProvider : IInitialize
    {
        public IEnumerator Initialize(Action onSuccessCallback)
        {
            yield return null;
            onSuccessCallback?.Invoke();
        }
    }
}