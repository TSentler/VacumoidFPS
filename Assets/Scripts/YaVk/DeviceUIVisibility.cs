using System.Collections.Generic;
using UnityEngine;

namespace YaVk
{
    public class DeviceUIVisibility : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _mobilePanels, _desktopPanels;
        
        private SocialNetwork _socialNetwork;
        private Coroutine _checkDeviceCoroutine;
        private void Awake()
        {
            _socialNetwork = FindObjectOfType<SocialNetwork>();
        }

        private void OnDisable()
        {
            if (_checkDeviceCoroutine !=null)
            {
                StopCoroutine(_checkDeviceCoroutine);
            }
        }

        private void Start()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            _desktopPanels.ForEach(item => item.SetActive(true));
            _mobilePanels.ForEach(item => item.SetActive(true));
            return;
#else 
            _desktopPanels.ForEach(item => item.SetActive(false));
            _mobilePanels.ForEach(item => item.SetActive(false));
#endif
            _checkDeviceCoroutine = StartCoroutine(
                _socialNetwork.CheckMobileDeviceCoroutine(
                    ActivateDevicePanels));
        }

        private void ActivateDevicePanels(bool isMobile)
        {
            if(isMobile)
            {
                _mobilePanels.ForEach(item => item.SetActive(true));
            }
            else
            {
                _desktopPanels.ForEach(item => item.SetActive(true));
            }
        }
        
    }
}
