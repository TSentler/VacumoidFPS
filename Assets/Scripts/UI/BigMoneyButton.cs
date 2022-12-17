using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BigMoneyButton : MonoBehaviour
    {
        [SerializeField] private Button _firstButton, _secondButton;

        public void OnClick()
        {
            if (false && _firstButton.interactable)
            {
                _firstButton.onClick.Invoke();
            }
            else if (_secondButton.isActiveAndEnabled 
                     && _secondButton.interactable)
            {
                _secondButton.onClick.Invoke();
            }
        }
    }
}
