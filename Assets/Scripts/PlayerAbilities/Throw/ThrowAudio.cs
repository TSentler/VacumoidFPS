using System;
using UnityEngine;

namespace PlayerAbilities.Throw
{
    [RequireComponent(typeof(AudioSource), 
        typeof(ThrowTimer))]
    public class ThrowAudio : MonoBehaviour
    {
        private AudioSource _audio;
        private ThrowTimer _throwTimer;

        private void Awake()
        {
            _audio = GetComponent<AudioSource>();
            _throwTimer = GetComponent<ThrowTimer>();
        }

        private void OnEnable()
        {
            _throwTimer.FxStarted += OnFxStarted;
        }

        private void OnDisable()
        {
            _throwTimer.FxStarted -= OnFxStarted;
        }

        private void OnFxStarted()
        {
            _audio.Play();
        }
    }
}
