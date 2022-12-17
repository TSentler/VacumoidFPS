using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using UnityTools;
using Upgrade.Move;

namespace Upgrade
{
    [RequireComponent(typeof(Button))]
    public class UpgradeView : MonoBehaviour
    {
        [SerializeField] private Upgrader _upgrader;
        [SerializeField] private TMP_Text _coastText;
        [SerializeField] private UpLevelText _upLevelText;
        
        private Button _button;
        
        private void OnValidate()
        {
            if (PrefabChecker.InPrefabFileOrStage(gameObject))
                return;

            if (_coastText == null)
                Debug.LogWarning("CoastText gameObject was not found!", this);
        }
        
        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            Setup(_upgrader.UpLevel, _upgrader.Coast);
            _button.onClick.AddListener(OnUpgraded);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnUpgraded);
        }

        private void OnUpgraded()
        {
            _upgrader.Upgrade();
            Setup(_upgrader.UpLevel, _upgrader.Coast);
        }

        private void Setup(int level, int money)
        {
            _coastText.SetText(money.ToString());
            _upLevelText.SetLevel(level);
        }
    }
}
