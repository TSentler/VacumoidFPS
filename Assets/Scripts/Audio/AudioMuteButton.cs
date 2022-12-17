using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Audio
{
    [RequireComponent(typeof(Button))]
    public class AudioMuteButton : MonoBehaviour
    {
        [SerializeField] private Sprite _switchOn, _switchOff;
        [SerializeField] private Image _image;
        
        private Button _button;
            
        public event UnityAction Clicked;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClicked);
        }

        private void OnClicked()
        {
            Clicked?.Invoke();
        }

        public void ChangeIcon(bool isOn)
        {
            _image.sprite = isOn ? _switchOn : _switchOff;
        }
    }
}
