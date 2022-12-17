using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class ToUpgradePanelButton : MonoBehaviour
    {
        [SerializeField] private GameObject _upgradePanel, _moneyWads;
        [Min(0), SerializeField] private float _delay;

        private Button _button;
        private Coroutine _coroutine;

        private void OnValidate()
        {
            if (_moneyWads == null)
                Debug.LogWarning("MoneyWads was not found!", this);
            if (_upgradePanel == null)
                Debug.LogWarning("UpgradePanel was not found!", this);
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(Apply);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Apply);
        }
        
        private IEnumerator ApplyCoroutine()
        {
            yield return new WaitForSeconds(_delay);
            _upgradePanel.SetActive(true);
        }

        private void Apply()
        {
            if (_coroutine != null)
                return;

            _moneyWads.SetActive(true);
            _coroutine = StartCoroutine(ApplyCoroutine());
        }
    }
}
