using Trash;
using UnityEngine;

namespace Vacuum
{
    [RequireComponent(typeof(AudioSource), 
        typeof(GarbageDisposal))]
    public class DisposalAudio : MonoBehaviour
    {
        private AudioSource _audio;
        
        private void Awake()
        {
            _audio = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_audio.isPlaying)
                return;
            
            if (other.TryGetComponent(out MicroGarbage garbage))
            {
                _audio.Play();
            }
        }
    }
}
