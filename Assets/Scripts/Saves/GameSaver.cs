using UnityEngine;

namespace Saves
{
    public class GameSaver
    {
        public void Save(string name, int value)
        {
            PlayerPrefs.SetInt(name, value);
            PlayerPrefs.Save();
        }

        public void Save(string name, float value)
        {
            PlayerPrefs.SetFloat(name, value);
            PlayerPrefs.Save();
        }

        public int LoadInt(string name, int defaultValue = 0)
        {
            if (PlayerPrefs.HasKey(name) == false)
            {
                return defaultValue;
            }
            return PlayerPrefs.GetInt(name);
        }
        
        public float LoadFloat(string name, float defaultValue = 0f)
        {
            if (PlayerPrefs.HasKey(name) == false)
            {
                return defaultValue;
            }
            return PlayerPrefs.GetFloat(name);
        }
        
    }
}
