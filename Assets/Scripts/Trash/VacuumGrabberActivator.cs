using Suckables;
using UnityEngine;

namespace Trash
{
    [RequireComponent(typeof(Collider))]
    public class VacuumGrabberActivator : MonoBehaviour, ISuckableToCenter,
        ITrashCollectable
    {
        [SerializeField] private DeformableGarbage _garbage;
        
        private ISuckCenter _target;
        private bool _isReady = true;

        public float TrashPoints { get; } = 0f;

        public void Activate()
        {
            Debug.Log("Activate");
            _isReady = true;
            if (_target != null)
            {
                _garbage.Suck(_target);
            }
        }

        public void Deactivate()
        {
            Debug.Log("Deactivate");
            _isReady = false;
        }
        
        public void Suck(ISuckCenter center)
        {
            if (_target == null || center == null)
                _target = center;
            
            if (_isReady)
            {
                _garbage.Suck(_target);
            }
        }

        public void Sucked()
        {
            if (_isReady)
            {
                _garbage.Sucked();
            }
        }
    }
}