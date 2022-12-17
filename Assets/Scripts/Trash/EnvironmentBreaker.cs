using UnityEngine;

namespace Trash
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnvironmentBreaker : MonoBehaviour
    {
        [SerializeField] private GameObject _fragments;
        [SerializeField] private float _maxVelocity = 50f;
        
        private Rigidbody _rigidbody;
        private Rigidbody[] _childRigidbodies;
        
        private void OnValidate()
        {
            if (_fragments == null)
                Debug.LogWarning("Fragments was not found!", this);
        }
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _childRigidbodies = GetComponentsInChildren<Rigidbody>(); 
        }

        private void Start()
        {
            _fragments.SetActive(false);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_rigidbody.velocity.sqrMagnitude > _maxVelocity)
            {
                Break();
            }
        }
        
        private void Break()
        {
            _fragments.transform.parent = transform.parent;
            _fragments.SetActive(true);
            foreach (var rb in _childRigidbodies)
            {
                rb.velocity = _rigidbody.velocity;
            }
            gameObject.SetActive(false);
        }
    }
}
