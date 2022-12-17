using Robber;
using UnityEngine;
using UnityTools;

namespace Money
{
    [RequireComponent(typeof(Animator))]
    public class RobberMoneyCollector : MonoBehaviour
    {
        [SerializeField] private GameObject _money;
        [SerializeField] private int _count;

        private Animator _animator;
        private SuckBehaviour _suckBehaviour;
        private MoneyCounter _moneyCounter;

        private void OnValidate()
        {
            if (PrefabChecker.InPrefabFileOrStage(gameObject))
                return;
            
            if (_money == null)
                Debug.LogWarning("Money was not found!", this);
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _suckBehaviour = _animator.GetBehaviour<SuckBehaviour>();
            _moneyCounter = FindObjectOfType<MoneyCounter>();
        }

        private void OnEnable()
        {
            _suckBehaviour.Started += OnSuckStarted;
        }

        private void OnDisable()
        {
            _suckBehaviour.Started -= OnSuckStarted;
        }
        
        private void OnSuckStarted()
        {
            _suckBehaviour.Started -= OnSuckStarted;
            _moneyCounter.Collect(_count);
            _money.transform.parent = null;
            _money.SetActive(true);
        }
    }
}
