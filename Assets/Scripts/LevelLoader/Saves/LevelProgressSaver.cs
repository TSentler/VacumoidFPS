using Saves;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelLoader.Saves
{
    public class LevelProgressSaver : MonoBehaviour
    {
        [SerializeField] private LevelInfo _levelInfo;
        
        private LevelSaver _saver;
        
        private void OnValidate()
        {
            if (_levelInfo == null)
                Debug.LogWarning("LevelInfo was not found!", this);
        }
        
        private void Awake()
        {
            _saver = new LevelSaver();
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            _saver.SaveLevel(_levelInfo.LevelNumber);
        }
    }
}
