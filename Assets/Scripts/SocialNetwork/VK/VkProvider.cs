using System;
using System.Collections;
using Agava.VKGames;
using SocialNetwork.Interfaces;

namespace SocialNetwork.VK
{
    public class VkProvider : IInitialize, IMobileChecker
    {
        public IEnumerator Initialize(Action onSuccessCallback)
        {
            yield return VKGamesSdk.Initialize(
                onSuccessCallback: onSuccessCallback);
        }

        public bool MobileCheck()
        {
            return Defines.IsVkMobileGames;
        }
    }
}