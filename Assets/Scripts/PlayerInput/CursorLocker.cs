using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayerInput
{
    public class CursorLocker : MonoBehaviour, IPointerDownHandler
    {
        private void Update()
        {
            if (Input.GetButtonDown("CursorUnlock"))
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
