using System;
using UnityEngine;
using UnityEngine.UI;

namespace SocialNetwork
{
    [RequireComponent(typeof(Button))]
    public class HideRewardAdsButton : MonoBehaviour
    {
        private UnifySocialNetworks _socialNetwork;
        
        private void Awake()
        {
            _socialNetwork = FindObjectOfType<UnifySocialNetworks>();
            gameObject.SetActive(_socialNetwork?.Ads.HasAccess() ?? false);
        }
    }
}
