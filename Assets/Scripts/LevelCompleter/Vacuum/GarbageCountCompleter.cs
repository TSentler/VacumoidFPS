using UnityEngine;
using Vacuum;

namespace LevelCompleter.Vacuum
{
    [RequireComponent(typeof(Completer))]
    public class GarbageCountCompleter : MonoBehaviour
    {
        private GarbageCounter _garbageCounter;
        private VacuumBag _vacuumBag;
        private Completer _completer;

        private void Awake()
        {
            _completer = GetComponent<Completer>();
            _vacuumBag = FindObjectOfType<VacuumBag>();
            _garbageCounter = FindObjectOfType<GarbageCounter>();
        }
        
        private void OnEnable()
        {
            _vacuumBag.TrashPointsChanged += OnTrashPointsChanged;
            _completer.Completed += OnCompleted;
        }

        private void OnDisable()
        {
            _vacuumBag.TrashPointsChanged -= OnTrashPointsChanged;
            _completer.Completed -= OnCompleted;
        }

        private void OnTrashPointsChanged(float collected)
        {
            if (_garbageCounter.TargetTrashPoints == _vacuumBag.TrashPoints)
            {
                _completer.Complete();
            }
        }

        private void OnCompleted()
        {
            _vacuumBag.AddToTotalPointsCounter(_vacuumBag.TrashPoints);
        }
    }
}
