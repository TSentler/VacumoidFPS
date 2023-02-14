using System.Collections.Generic;
using UnityEngine;

namespace SocialNetwork
{
    public class DeviceUIVisibility : MonoBehaviour
    {
        [SerializeField] private bool _isDesktop;
        [SerializeField] private List<GameObject> _mobilePanels, _desktopPanels;
        
        private UnifySocialNetworks _socialNetwork;
        private Coroutine _checkDeviceCoroutine;
        
        private void Awake()
        {
            _socialNetwork = FindObjectOfType<UnifySocialNetworks>();
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
            _desktopPanels.ForEach(item => item.SetActive(false));
            _mobilePanels.ForEach(item => item.SetActive(false));
            if (Defines.IsUnityWebGl == false || Defines.IsUnityEditor)
            {
                if (_isDesktop)
                {
                    _desktopPanels.ForEach(item => item.SetActive(true));
                    return;
                }

                _desktopPanels.ForEach(item => item.SetActive(true));
                _mobilePanels.ForEach(item => item.SetActive(true));
                return;
            }
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
