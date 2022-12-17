using System.Collections;
using Saves;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LevelLoader.Saves
{
    [RequireComponent(typeof(LevelInfo))]
    public class FirstLevelLoader : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [Min(0f), SerializeField] private float _delay = 10f;

        private LevelInfo _levelInfo;
        private LevelSaver _saver;

        private void OnValidate()
        {
            if (_slider == null)
                Debug.LogWarning("Slider was not found!", this);
        }

        private void Awake()
        {
            _levelInfo = GetComponent<LevelInfo>();
            _saver = new LevelSaver();
        }

        private IEnumerator Start()
        {
            var lastLevel = _saver.GetLevel();
            var levelName = _levelInfo.TutorialName;
            if (lastLevel > -1)
            {
                levelName = _levelInfo.GetName(lastLevel);
            }

            var ellapsed = 0f;
            while (ellapsed < _delay)
            {
                ellapsed += 0.5f;
                _slider.value = ellapsed / _delay;
                yield return new WaitForSecondsRealtime(0.5f);
            }
            
            SceneManager.LoadScene(levelName);
        }
    }
}
