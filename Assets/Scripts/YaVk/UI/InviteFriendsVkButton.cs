using System;
using Agava.VKGames;
using UnityEngine;
using UnityEngine.UI;

namespace YaVk
{
    [RequireComponent(typeof(Button))]
    public class InviteFriendsVkButton : MonoBehaviour
    {
        private Button _button;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnInvited);
        }

        private void OnInvited()
        {
            SocialInteraction.InviteFriends(OnRewardedCallback);
        }

        private void OnRewardedCallback()
        {
        }
    }
}
