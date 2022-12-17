using Trash;
using UnityEngine;

namespace Vacuum
{
    [RequireComponent(typeof(AudioSource), 
        typeof(GarbageDisposal))]
    public class DisposalAudio : MonoBehaviour
    {
        private AudioSource _audio;
        private GarbageDisposal _disposal;

        private void Awake()
        {
            _audio = GetComponent<AudioSource>();
            _disposal = GetComponent<GarbageDisposal>();
        }

        private void OnEnable()
        {
            _disposal.Collected += OnCollected;
        }

        private void OnDisable()
        {
            _disposal.Collected -= OnCollected;
        }

        private void OnCollected(Garbage garbage)
        {
            if (garbage is MicroGarbage || _audio.isPlaying)
                return;

            _audio.Play();
        }
    }
}
