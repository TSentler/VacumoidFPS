using UnityEngine;

namespace PlayerAbilities.Push
{
    public class Pusher : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Repellent repellent))
            {
                repellent.Push(collision.GetContact(0).point);
            }
        }
    }
}
