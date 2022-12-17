using UnityEngine;
using Vacuum;

namespace Money.Vacuum
{
    public class TrashMoneyCollector : MonoBehaviour
    {
        [SerializeField] private MoneyCounter _moneyCounter;
        [Min(0f), SerializeField] private float _factor = 0.25f;
        
        private VacuumBag _vacuumBag;
        
        private void OnValidate()
        {
            if (_moneyCounter == null)
                Debug.LogWarning("MoneyCounter was not found!", this);
        }

        private void Awake()
        {
            _vacuumBag = FindObjectOfType<VacuumBag>();
        }

        private void OnEnable()
        {
            _vacuumBag.TrashPointsChanged += OnTrashPointsChanged;
        }

        private void OnDisable()
        {
            _vacuumBag.TrashPointsChanged -= OnTrashPointsChanged;
        }

        private void OnTrashPointsChanged(float collected)
        {
            var money = collected * _factor;
            _moneyCounter.Collect(money);
        }
    }
}
