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
        private bool _isReady = false;

        public float TrashPoints => _isReady ? _garbage.TrashPoints : 0f;

        public void Activate()
        {
            _isReady = true;
            if (_target != null)
            {
                _garbage.Suck(_target);
            }
        }

        public void Deactivate()
        {
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