using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class CollectedText : MonoBehaviour
    {
        private readonly string _separator = "/";
    
        [SerializeField] private TMP_Text _text;

        private int _lastCount, _lastCollected;
        
        private string Template => $"{{0}}{_separator}{{1}}";

        
        private void Awake()
        {
            SetText();
        }

        private void SetText()
        {
            _text.SetText(string.Format(Template, _lastCollected, _lastCount));
        }

        public void SetCount(int count)
        {
            _lastCount = count;
            SetText();
        }
    
        public void SetCollected(int collected)
        {
            _lastCollected = collected;
            SetText();
        }
    }
}
