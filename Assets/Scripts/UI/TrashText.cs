using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TrashText : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        public event UnityAction<string> Changed;
        
        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        public void SetText(int count) 
        {
            if (_text == null)
                return;
            
            _text.SetText(count.ToString());
            Changed?.Invoke(_text.text);
        }
    }
}
