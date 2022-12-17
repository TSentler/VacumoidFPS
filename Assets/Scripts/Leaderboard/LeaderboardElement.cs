using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Leaderboard
{
    public class LeaderboardElement : MonoBehaviour
    {
        [SerializeField] private TMP_Text _playerNumber;
        [SerializeField] private TMP_Text _playerNick;
        [SerializeField] private TMP_Text _playerResult;
        [SerializeField] private Color _playerColor;
        [SerializeField] private Image _backImage;
        [SerializeField] private Sprite _playerSprite;

        public void Initialize (int number, string nick, int playerResult, bool isPlayer)
        {
            _playerNumber.text = number.ToString();
            _playerNick.text = nick;
            _playerResult.text = playerResult.ToString();

            if(isPlayer)
            {
                _backImage.sprite = _playerSprite;
                _playerNick.color = _playerColor;
                _playerResult.color = _playerColor;
            }
        }
    }
}
