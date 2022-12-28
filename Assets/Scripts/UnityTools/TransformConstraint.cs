using UnityEngine;

namespace UnityTools
{
    public class TransformConstraint : MonoBehaviour
    {
        [SerializeField] private Transform _root;
        [SerializeField] private bool _isPosition = true, _isRotation = true;

        private void LateUpdate()
        {
            UpdateTransform();
        }

        private void UpdateTransform()
        {
            if (_isPosition)
                transform.position = _root.position;
            if (_isRotation)
                transform.rotation = _root.rotation;
        }
    }
}
