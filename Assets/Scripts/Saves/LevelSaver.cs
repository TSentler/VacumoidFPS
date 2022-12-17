using UnityEngine;

namespace Saves
{
    public class LevelSaver
    {
        private readonly string _levelName = "Level";
        
        private int _lastLevel = -1;
        private GameSaver _saver;
        
        public LevelSaver()
        {
            _saver = new GameSaver();
        }

        public int GetLevel()
        {
            _lastLevel = PlayerPrefs.HasKey(_levelName)
                ? PlayerPrefs.GetInt(_levelName) 
                : -1;
            return _lastLevel;
        }
        
        public void SaveLevel(int number)
        {
            if (number < 0 || _lastLevel == number)
                return;

            _lastLevel = number;
            _saver.Save(_levelName, number);
        }
    }
}
