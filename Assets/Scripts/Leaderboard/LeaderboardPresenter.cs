using LevelCompleter;
using UnityEngine;
using UnityTools;
using Vacuum;
using SocialNetwork;

namespace Leaderboard
{
    public class LeaderboardPresenter : MonoBehaviour
    {
        [SerializeField] private LeaderboardShowButton _button;
        [SerializeField] private LeaderboardView _view;
        [SerializeField] private Completer _completer;
        
        private UnifySocialNetworks _socialNetwork;
        private VacuumBag _vacuumBag;
        private bool _hasLeaderboard;

        private void OnValidate()
        {
            if (PrefabChecker.InPrefabFileOrStage(gameObject))
                return;
            
            if (_button == null)
                Debug.LogWarning("Leaderboard Button was not found!", this);
            if (_view == null)
                Debug.LogWarning("Leaderboard View was not found!", this);
            if (_completer == null)
                Debug.LogWarning("Completer was not found!", this);
        }
        
        private void Awake()
        {
            _socialNetwork = FindObjectOfType<UnifySocialNetworks>();
            _hasLeaderboard =
                _socialNetwork?.Leaderboard.IsAutoLeaderboard() ?? false;
            _vacuumBag = FindObjectOfType<VacuumBag>();
        }
        
        private void OnEnable()
        {
            _completer.Completed += LevelCompleted;
            _button.BoardShowed += BoardShowed;
        }

        private void OnDisable()
        {
            _completer.Completed -= LevelCompleted;
            _button.BoardShowed -= BoardShowed;
        }

        private void Start()
        {
            _view.gameObject.SetActive(false);
            if (_hasLeaderboard)
            {
                _button.ShowCupIcon();
            }
            else
            {
                _button.ShowVacuumIcon();
            }
        }

        private void BoardShowed()
        {
            if (_socialNetwork.Leaderboard.IsAutoLeaderboard())
            {
                Apply();
            }
            else
            {
                _view.gameObject.SetActive(true);
            }
        }

        private void LevelCompleted()
        {
            if (_hasLeaderboard)
                return; 
            
            Apply();
        }
        
        private void Apply()
        {
            _socialNetwork.Leaderboard.GetLeaderboard(
                _vacuumBag.AllTrashPointsRounded,
                leaderList =>
                {
                    if (_hasLeaderboard || _view == null)
                        return;
                    
                    _view.ConstructLeaderboard(leaderList);
                });
        }
    }
}
