using UnityEngine;

namespace PlayerAbilities.Push
{
    [RequireComponent(typeof(Rigidbody))]
    public class Repellent : MonoBehaviour
    {
        [SerializeField] private float _speed = 300f;
        
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Push(Vector3 point)
        {
            _rigidbody.AddForceAtPosition(
                Vector3.up * _speed * Time.deltaTime,
                point, 
                ForceMode.Impulse);
        }
    }
}
