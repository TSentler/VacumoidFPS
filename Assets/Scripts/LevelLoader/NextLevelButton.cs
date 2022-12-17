using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LevelLoader
{
    [RequireComponent(typeof(Button))]
    public class NextLevelButton : MonoBehaviour
    {
        [Min(0), SerializeField] private float _delay;
        
        private Button _button;
        private UnityAction _nextLevelAction;
        private Coroutine _coroutine; 

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(Apply);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Apply);
        }
        
        private IEnumerator ApplyCoroutine()
        {
            yield return new WaitForSeconds(_delay);
            _nextLevelAction?.Invoke();
        }

        public void SetNextLevelAction(UnityAction action)
        {
            _nextLevelAction = action;
        }
        
        private void Apply()
        {
            if (_coroutine != null)
                return;

            _coroutine = StartCoroutine(ApplyCoroutine());
        }
    }
}
