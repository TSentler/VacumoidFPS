using System;
using UnityEngine;
using UnityEngine.UI;

namespace YaVk
{
    [RequireComponent(typeof(Button))]
    public class HideRewardAdsButton : MonoBehaviour
    {
        private SocialNetwork _socialNetwork;
        
        private void Awake()
        {
            _socialNetwork = FindObjectOfType<SocialNetwork>();
            gameObject.SetActive(_socialNetwork?.IsAdsAccess() ?? false);
        }
    }
}
