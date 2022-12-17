using System;
using Agava.VKGames;
using UnityEngine;
using UnityEngine.UI;

namespace YaVk
{
    [RequireComponent(typeof(Button))]
    public class InviteGroupVkButton : MonoBehaviour
    {
        private Button _button;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnInvited);
        }

        private void OnInvited()
        {
            Community.InviteToIJuniorGroup(OnRewardedCallback);
        }

        private void OnRewardedCallback()
        {
        }
    }
}
