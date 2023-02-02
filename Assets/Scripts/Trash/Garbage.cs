using System;
using Suckables;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Trash
{
    public interface ITrashCollectable
    {
        float TrashPoints { get; }

        void Sucked();
    }
    
    [RequireComponent(typeof(Collider))]
    public abstract class Garbage : MonoBehaviour, ISuckableToCenter,
        ITrashCollectable
    {
        [FormerlySerializedAs("_count")] 
        [Min(0), SerializeField] private float _trashPoints = 0f;

        private ISuckCenter _target;
        
        public Transform Target
        {
            get
            {
                if (_target == null)
                    return null;
                
                return _target.GetTransform();
            }
        }

        public float TrashPoints => _trashPoints;

        public event UnityAction SuckStarted;
        
        protected abstract void SuckHandler();

        public void Suck(ISuckCenter center)
        {
            _target = center;
            if (Target == null)
                return;
            
            SuckHandler();
            SuckStarted?.Invoke();
        }
        
        public void Sucked()
        {
            gameObject.SetActive(false);
        }
    }
}