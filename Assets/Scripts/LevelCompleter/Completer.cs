using Money;
using UnityEngine;
using UnityEngine.Events;

namespace LevelCompleter
{
    public class Completer : MonoBehaviour
    {
        private bool _isCompleted;
        
        [SerializeField] private MoneyCounter _moneyCounter;
        [SerializeField] private CompletePresenter _completePresenter;

        public event UnityAction Completed;

        public bool IsCompleted => _isCompleted;
        
        private void OnValidate()
        {
            if (_moneyCounter == null)
                Debug.LogWarning("MoneyCounter was not found!", this);
            if (_completePresenter == null)
                Debug.LogWarning("CompletePresenter was not found!", this);
        }

        public void Complete()
        {
            if (_isCompleted)
                return;
            
            _isCompleted = true;
            _moneyCounter.Pause();
            _completePresenter.Apply();
            Completed?.Invoke();
        }
    }
}
