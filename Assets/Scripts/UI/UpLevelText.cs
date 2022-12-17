using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UpLevelText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private int _lastLevel;
        
        private string Template => $"LvL {{0}}";

        private void Awake()
        {
            SetText();
        }

        public void SetLevel(int level)
        {
            _lastLevel = level;
            SetText();
        }
        
        private void SetText()
        {
            _text.SetText(string.Format(Template, _lastLevel));
        }
    }
}
