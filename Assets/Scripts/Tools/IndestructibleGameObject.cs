using UnityEngine;

namespace Tools
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
