using System;
using UnityEngine;

namespace PlayerAbilities
{
    public class SpeedStat : MonoBehaviour
    {
        [SerializeField] private float _speedMultiply = 1.5f, 
            _runSpeed = 170f;

        public float Value { get; private set; }

        private void Awake()
        {
            Value = _runSpeed;
        }

        public void BoostSpeed()
        {
            Value = _runSpeed * _speedMultiply;
        }

        public void ResetSpeed()
        {
            Value = _runSpeed;
        }

        public void Upgrade(float speed)
        {
            Value = _runSpeed = speed;
        }
    }
}
