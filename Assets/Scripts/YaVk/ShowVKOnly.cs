using UnityEngine;

namespace YaVk
{
    public class ShowVKOnly : MonoBehaviour
    {
        private void OnEnable()
        {
#if VK_GAMES == false
            gameObject.SetActive(false);
#endif            
        }
    }
}
