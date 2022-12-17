using UnityEngine;
using UnityTools;
using Vacuum;

namespace UI.Vacuum
{
    public class TrashPointsCollectPresenter : MonoBehaviour
    {
        [SerializeField] private TrashText _allInGamePanel,
            _allInCompletitionPanel;

        private VacuumBag _vacuumBag;

        private void OnValidate()
        {
            if (PrefabChecker.InPrefabFileOrStage(gameObject))
                return;
            
            if (_allInCompletitionPanel == null || _allInGamePanel == null)
                Debug.LogWarning("CountDown was not found!", this);
        }

        private void Awake()
        {
            _vacuumBag = FindObjectOfType<VacuumBag>();
        }

        private void OnEnable()
        {
            _vacuumBag.AllTrashPointsChanged += OnAllTrashPointsChanged;
            _vacuumBag.TrashPointsChanged += OnTrashPointsChanged;
        }

        private void OnDisable()
        {
            _vacuumBag.AllTrashPointsChanged -= OnAllTrashPointsChanged;
            _vacuumBag.TrashPointsChanged -= OnTrashPointsChanged;
        }

        private void OnTrashPointsChanged(float collected)
        {
            SetText(_vacuumBag.TrashPoints +
                    _vacuumBag.AllTrashPointsRounded);
        }

        private void OnAllTrashPointsChanged()
        {
            SetText(_vacuumBag.AllTrashPointsRounded);
        }

        private void SetText(int count)
        {
            _allInGamePanel.SetText(count);
            _allInCompletitionPanel.SetText(count);
        }
    }
}
