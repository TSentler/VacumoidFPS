using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YaVk;

namespace Leaderboard
{
    [RequireComponent(typeof(Button))]
    public class LeaderboardShowButton : MonoBehaviour
    {
        [SerializeField] private GameObject _cupIcon, _vacuumIcon;
        [SerializeField] private Animator _animator;
        [SerializeField] private Button _button;

        public event UnityAction BoardShowed;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnLeaderboardShowed);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnLeaderboardShowed);
        }

        private void OnLeaderboardShowed()
        {
            BoardShowed?.Invoke();
        }

        public void ShowCupIcon()
        {
            ShowLeaderboardIcon(true);
        }

        public void ShowVacuumIcon()
        {
            ShowLeaderboardIcon(false);
        }

        private void ShowLeaderboardIcon(bool isCupActive)
        {
            _vacuumIcon.SetActive(isCupActive == false);
            _cupIcon.SetActive(isCupActive);
            _button.enabled = isCupActive;
            _animator.enabled = isCupActive;
        }
    }
}
