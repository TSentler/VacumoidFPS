using PlayerAbilities.Move;
using UnityEngine;
using UnityTools;
using YaVk;

namespace Tutorial
{
    public class MovementTutorial : MonoBehaviour
    {
        [SerializeField] private GameObject _keyboardPanel,
            _stickPanel;
        
        private TopDownMovement _topDownMovement;
        private SocialNetwork _socialNetwork;
        private Coroutine _checkMobileDeviceCoroutine;
        private float _minSqrMoveStep = 0.1f;
        
        private void OnValidate()
        {
            if (PrefabChecker.InPrefabFileOrStage(gameObject))
                return;
            
            if (_keyboardPanel == null)
                Debug.LogWarning("KeyboardPanel was not found!", this);
            if (_stickPanel == null)
                Debug.LogWarning("StickPanel was not found!", this);
        }

        private void Awake()
        {
            _socialNetwork = FindObjectOfType<SocialNetwork>();
            _topDownMovement = FindObjectOfType<TopDownMovement>();
            _keyboardPanel.SetActive(false);
            _stickPanel.SetActive(false);
        }

        private void OnEnable()
        {
            _topDownMovement.Moved += MovedTrigger;
        }

        private void OnDisable()
        {
            _topDownMovement.Moved -= MovedTrigger;
            if (_checkMobileDeviceCoroutine !=null)
            {
                StopCoroutine(_checkMobileDeviceCoroutine);
            }
        }
        
        private void Start()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            _keyboardPanel.SetActive(true);
            _stickPanel.SetActive(true);
            return;
#endif
            _checkMobileDeviceCoroutine = StartCoroutine(
                _socialNetwork.CheckMobileDeviceCoroutine(
                    ActivateTutorialPanel));
        }

        private void ActivateTutorialPanel(bool isMobile)
        {
            if(isMobile)
            {
                _stickPanel.SetActive(true);
            }
            else
            {
                _keyboardPanel.SetActive(true);
            }
        }
        
        private void MovedTrigger(Vector2 direction)
        {
            if (direction.sqrMagnitude < _minSqrMoveStep)
                return;

            _keyboardPanel.SetActive(false);
            _stickPanel.SetActive(false);
            enabled = false;
        }
    }
}
