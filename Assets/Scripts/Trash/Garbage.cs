using System;
using Suckables;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Trash
{
    [RequireComponent(typeof(Collider))]
    public abstract class Garbage : MonoBehaviour, ISuckableToCenter
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
        
        private bool CheckPositive(float value)
        {
            if (value < 0f)
            {
                Debug.LogWarning("Count of garbage less than 0", this);
            }

            return value > 0f;
        }

        protected abstract void SuckHandler();

        public void SetCount(float value)
        {
            if (_trashPoints == 0f && CheckPositive(value))
            {
                _trashPoints = value;
            }
        }
        
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