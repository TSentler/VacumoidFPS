using UnityEngine;

namespace UnityTools
{
    public class FPSLimit : MonoBehaviour
    {
        [SerializeField] private int _targetFrameRate = 30;

        private void OnEnable()
        {
            Application.targetFrameRate = _targetFrameRate; 
        }

        private void Update()
        {
            // Application.targetFrameRate = _targetFrameRate; 
        }
    }
}
