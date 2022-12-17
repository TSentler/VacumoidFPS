using UnityEngine;
using UnityTools;
using Vacuum;

namespace UI.Trash
{
    public class GarbageCountPresenter : MonoBehaviour
    {
        [SerializeField] private CollectedText _collectedText;
        [SerializeField] private SmoothSlider _collectedSlider;

        private VacuumBag _vacuumBag;
        private GarbageCounter _garbageCounter;
        
        private void OnValidate()
        {
            if (PrefabChecker.InPrefabFileOrStage(gameObject))
                return;
            
            if (_collectedText == null)
                Debug.LogWarning("CollectedText was not found!", this);
            if (_collectedSlider == null)
                Debug.LogWarning("SmoothSlider was not found!", this);
        }

        private void Awake()
        {
            _vacuumBag = FindObjectOfType<VacuumBag>();
            _garbageCounter = FindObjectOfType<GarbageCounter>();
        }

        private void OnEnable()
        {
            OnTargetTrashPointsChanged();
            _vacuumBag.TrashPointsChanged += OnTrashPointsChanged;
            _garbageCounter.TargetTrashPointsChanged += OnTargetTrashPointsChanged;
        }

        private void OnDisable()
        {
            _vacuumBag.TrashPointsChanged -= OnTrashPointsChanged;
            _garbageCounter.TargetTrashPointsChanged -= OnTargetTrashPointsChanged;
        }
        
        private void OnTargetTrashPointsChanged()
        {
            _collectedText.SetCount(_garbageCounter.TargetTrashPoints);
        }
        
        private void OnTrashPointsChanged(float collected)
        {
            var collectedRound = _vacuumBag.TrashPoints;
            _collectedText.SetCollected(collectedRound);
            float sliderValue = (float)collectedRound / _garbageCounter.TargetTrashPoints;
            _collectedSlider.SetValue(sliderValue);
        }
    }
}