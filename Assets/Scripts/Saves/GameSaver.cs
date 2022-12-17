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

        public int Load(string name)
        {
            if (PlayerPrefs.HasKey(name) == false)
            {
                return 0;
            }
            return PlayerPrefs.GetInt(name);
        }
    }
}
