using PlayerAbilities.Move;
using PlayerInput;
using UnityEngine;
using UnityTools;

namespace Tutorial
{
    public class MovementTutorial : MonoBehaviour
    {
        [SerializeField] private GameObject _keyboardPanel;
        
        private FPSMovement _fpsMovement;
        private Coroutine _checkMobileDeviceCoroutine;
        private float _minSqrMoveStep = 0.1f;
        
        private void OnValidate()
        {
            if (PrefabChecker.InPrefabFileOrStage(gameObject))
                return;
            
            if (_keyboardPanel == null)
                Debug.LogWarning("KeyboardPanel was not found!", this);
        }

        private void Awake()
        {
            PlayerInputSource playerInput = FindObjectOfType<PlayerInputSource>();
            _fpsMovement = playerInput.GetComponent<FPSMovement>();
        }

        private void OnEnable()
        {
            _fpsMovement.Moved += OnMoved;
        }

        private void OnDisable()
        {
            _fpsMovement.Moved -= OnMoved;
        }
        
        private void OnMoved(Vector2 direction)
        {
            if (direction.sqrMagnitude < _minSqrMoveStep)
                return;

            _keyboardPanel.SetActive(false);
            enabled = false;
        }
    }
}
