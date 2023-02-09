using System;
using System.Collections;
using Agava.VKGames;
using YaVk.Interfaces;

namespace YaVk.VK
{
    public class VkProvider : IInitialize
    {
        public IEnumerator Initialize(Action onSuccessCallback)
        {
            yield return VKGamesSdk.Initialize(
                onSuccessCallback: onSuccessCallback);
        }
    }
}