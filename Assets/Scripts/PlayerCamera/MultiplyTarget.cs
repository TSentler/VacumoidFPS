using System.Collections.Generic;
using UnityEngine;

namespace PlayerCamera
{
    [RequireComponent(typeof(Camera), 
        typeof(Follow), 
        typeof(Zoom))]
    public class MultiplyTarget : MonoBehaviour
    {
        [SerializeField] private List<Transform> _targets = new();
        
        private Follow _follow;
        private Zoom _zoom;
        private float _maxSqrRemoteness = 200f;
        
        private void OnValidate()
        {
            if (_targets.Count == 0)
                Debug.LogWarning("_targets was not found!", this);
        }

        private void Awake()
        {
            _follow = GetComponent<Follow>();
            _zoom = GetComponent<Zoom>();
        }

        private void LateUpdate()
        {
            if (_targets.Count == 0)
                return;

            var bounds = GetBounds();
            var greatestDistance = 
                new Vector2(bounds.size.x, bounds.size.z);
            _follow.Apply(bounds.center, greatestDistance.y);
            _zoom.Apply(greatestDistance);
        }

        private Bounds GetBounds()
        {
            var firstPosition = _targets[0].position;
            var bounds = new Bounds(firstPosition, Vector3.zero);
            for (int i = 1; i < _targets.Count; i++)
            {
                if (_targets[i].gameObject.activeSelf
                    && (firstPosition - _targets[i].position).sqrMagnitude < 
                    _maxSqrRemoteness)
                {
                    bounds.Encapsulate(_targets[i].position);
                }
            }

            return bounds;
        }
    }
}
