using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelLoader
{
    public class LevelInfo : MonoBehaviour
    {
        private readonly string[] _names = new[]
        {
            "Level1",
            "Level3",
            "LevelLibrary",
            "Level2",
            "LevelOffice",
            "Level4",
            "LevelGym",
            "Level5",
            "LevelArt",
        };
        private readonly string _tutorialName = "Tutorial";
        
        private int _currentLevel = -1;

        public int LevelNumber => _currentLevel;
        public string TutorialName => _tutorialName;

        private void OnValidate()
        {
            if(_names.Length == 0)
                Debug.LogWarning("level names was not found!", this);
        }

        private void Awake()
        {
            var name = SceneManager.GetActiveScene().name;
            for (int i = 0; i < _names.Length; i++)
            {
                if (_names[i] == name)
                {
                    _currentLevel = i;
                    break;
                }
            }
        }

        public string GetNextLevel()
        {
            var number = _currentLevel + 1;
            if (number == _names.Length)
            {
                number = 0;
            }

            return _names[number];
        }

        public string GetName(int number)
        {
            if (number >= _names.Length)
            {
                return _names[0];
            }

            return _names[number];
        }
    }
}
