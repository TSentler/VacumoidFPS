using UnityEngine;

namespace YaVk
{
    [DisallowMultipleComponent]
    public class IndestructibleGameObject : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}
