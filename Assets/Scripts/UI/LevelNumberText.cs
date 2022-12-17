using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class LevelNumberText : MonoBehaviour
    {
        [Range(0,1), SerializeField] private int _templateType;

        private TMP_Text _text;
        private string _template;
        private string[] _templates = new []
        {
            $"-  №{{0}}  -",
            $"--- *{{0}}* ---"
        };
        private int _number;
        
        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            _template = _templates[_templateType];
            if (_number != 0)
            {
                SetNumber(_number);
            }
        }

        public void SetNumber(int number)
        {
            _number = number;
            _text?.SetText(
                string.Format(_template, number.ToString("00")));
        }
    }
}
