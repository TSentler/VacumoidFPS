using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityTools;

namespace LevelCompleter
{
    public class CompletePresenter : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _hide = new(); 
        [SerializeField] private GameObject _lvlCompletionPanel, _moneyButton;

        public event UnityAction InterstitialAdsStarted;
        
        private void OnValidate()
        {
            if (PrefabChecker.InPrefabFileOrStage(gameObject))
                return;
            
            if (_hide.Count == 0)
                Debug.LogWarning("Hide panels was not found!", this);
            if (_lvlCompletionPanel == null)
                Debug.LogWarning("LvlCompletionPanel was not found!", this);
        }

        private IEnumerator CompleteCoroutine()
        {
            _hide.ForEach(item => item.SetActive(false));
            _lvlCompletionPanel.SetActive(true);
            InterstitialAdsStarted?.Invoke();
            yield return new WaitForSeconds(1f);
            _moneyButton.SetActive(true);
        }

        public void Apply()
        {
            StartCoroutine(CompleteCoroutine());
        }
    }
}
