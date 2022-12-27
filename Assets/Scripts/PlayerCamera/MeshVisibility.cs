using UnityEngine;

namespace PlayerCamera
{
    [RequireComponent(typeof(ObstaclesAvoider))]
    public class MeshVisibility : MonoBehaviour
    {
        private readonly float _threshold = 0.01f;
        
        [Min(0f), SerializeField] private float _minRootDistance = 0.5f;
        [SerializeField] private Transform _root;
        [SerializeField] private Renderer[] _meshRenderers;

        private ObstaclesAvoider _avoider;
        private bool _isEnabled = true;

        private void Awake()
        {
            _avoider = GetComponent<ObstaclesAvoider>();
        }

        private void OnEnable()
        {
            _avoider.Moved += OnMoved;
        }

        private void OnDisable()
        {
            _avoider.Moved -= OnMoved;
        }

        private void OnMoved()
        {
            if (_isEnabled && 
                Vector3.Distance(transform.position, _root.position) <
                _minRootDistance - _threshold)
            {
                Hide();
            }

            if (_isEnabled == false &&
                Vector3.Distance(transform.position, _root.position) >
                _minRootDistance + _threshold)
            {
                Show();
            }
        }

        private void Show()
        {
            _isEnabled = true;
            for (int i = 0; i < _meshRenderers.Length; i++)
            {
                _meshRenderers[i].enabled = _isEnabled;
            }
        }

        private void Hide()
        {
            _isEnabled = false;
            for (int i = 0; i < _meshRenderers.Length; i++)
            {
                _meshRenderers[i].enabled = _isEnabled;
            }
        }
    }
}
