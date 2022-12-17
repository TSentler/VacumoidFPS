using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityTools;

namespace LevelLoader
{
    [RequireComponent(typeof(LevelInfo))]
    public class NextLevelLoader : MonoBehaviour
    {
        [SerializeField] private NextLevelButton _nextLevelButtons;
        
        private LevelInfo _levelInfo;

        private void OnValidate()
        {
            if (PrefabChecker.InPrefabFileOrStage(gameObject))
                return;
            
            if (_nextLevelButtons == null)
                Debug.LogWarning("NextLevelButton was not found!", this);
        }
        
        private void Awake()
        {
            _levelInfo = GetComponent<LevelInfo>();
            _nextLevelButtons.SetNextLevelAction(Load);
        }

        private void Load()
        {
            SceneManager.LoadScene(_levelInfo.GetNextLevel());
        }
    }
}
