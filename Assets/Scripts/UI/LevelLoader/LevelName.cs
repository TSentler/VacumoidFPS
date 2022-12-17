using System.Collections.Generic;
using LevelLoader;
using UnityEngine;

namespace UI.LevelLoader
{
    [RequireComponent(typeof(LevelInfo))]
    public class LevelName : MonoBehaviour
    {
        [SerializeField] private List<LevelNumberText> _numberTexts = new ();

        private LevelInfo _levelInfo;
        
        private void OnValidate()
        {
            if (_numberTexts.Count == 0)
                Debug.LogWarning("LevelNumberText was not found!", this);
        }

        private void Awake()
        {
            _levelInfo = GetComponent<LevelInfo>();
        }

        private void Start()
        {
            SetAllLevelNumber();
        }

        private void SetAllLevelNumber()
        {
            var number = _levelInfo.LevelNumber + 1;
            foreach (var numberText in _numberTexts)
            {
                numberText.SetNumber(number);
            }
        }
    }
}
