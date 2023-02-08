using System;
using UnityEngine;
using UnityEngine.Events;

namespace Plugins.PointerLock
{
    public class PointerLockHook : MonoBehaviour
    {
        public event UnityAction PointerLocked, PointerUnlocked;
        
        private void Awake()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            PointerLockListener.PointerLockListenerInitialize();
#endif
        }

        private void OnEnable()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            PointerLockListener.AddPointerLockListener();
#endif
        }

        private void OnDisable()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            PointerLockListener.RemovePointerLockListener();
#endif
        }

        public void OnLockCursorChanged(int isLock)
        {
            if (isLock > 0)
            {
                PointerLocked?.Invoke();
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                // Application.ExternalEval("window.focus();");
                PointerUnlocked?.Invoke();
            }
        }
    }
}
