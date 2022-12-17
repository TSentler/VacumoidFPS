using Money;
using TMPro;
using UnityEngine;
using UnityTools;

namespace UI.Money
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class WalletMoneyTotalText : MonoBehaviour
    {
        [SerializeField] private Wallet _wallet;

        private TextMeshProUGUI _text;
        private CountDown _countDown;

        private void OnValidate()
        {
            if (PrefabChecker.InPrefabFileOrStage(gameObject))
                return;
            
            if (_wallet == null)
                Debug.LogWarning("Wallet was not found!", this);
        }

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            TryGetComponent(out _countDown);
        }

        private void OnEnable()
        {
            SetMoneyText();
            _wallet.Changed += SetMoneyText;
        }

        private void OnDisable()
        {
            _wallet.Changed -= SetMoneyText;
        }

        private void SetMoneyText()
        {
            if (_countDown?.isActiveAndEnabled == true)
            {
                _countDown.Apply(_wallet.Money);
            }
            else
            {
                _text.SetText(_wallet.Money.ToString());
            }
        }
    }
}
