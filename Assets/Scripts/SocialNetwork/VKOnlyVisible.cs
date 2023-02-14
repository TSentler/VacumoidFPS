using UnityEngine;

namespace SocialNetwork
{
    public class VKOnlyVisible : MonoBehaviour
    {
        private void OnEnable()
        {
            if (Defines.IsVkGames == false)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
