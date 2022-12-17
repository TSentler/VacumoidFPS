using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class DoubleTrashText : MonoBehaviour
    {
        [SerializeField] private TrashText _trashText;

        private TextMeshProUGUI _text;
        
        private void OnValidate()
        {
            if (_trashText == null)
                Debug.LogWarning("TrashText was not found!", this);
        }
        
        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            _trashText.Changed += OnChanged;
        }

        private void OnDisable()
        {
            _trashText.Changed -= OnChanged;
        }

        private void OnChanged(string text)
        {
            _text.SetText(text);
        }
    }
}
