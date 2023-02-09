using System;
using System.Collections;

namespace YaVk.Interfaces
{
    public interface IInitialize
    {
        public IEnumerator Initialize(Action onSuccessCallback);
    }
}