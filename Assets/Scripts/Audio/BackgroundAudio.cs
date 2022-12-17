using Agava.WebUtility;
using UnityEngine;
using YaVk;

namespace Audio
{
    public class BackgroundAudio : MonoBehaviour
    {
        [SerializeField] private SocialNetwork _socialNetwork;

        private bool _isPlayerMuteAudio;
        
        public bool IsGameAudioOn => AudioListener.pause == false 
                                     && AudioListener.volume > 0f;
        
        private void OnEnable()
        {
            _socialNetwork.AdsStarted += OnAdsStarted;
            _socialNetwork.AdsEnded += OnAdsEnded;
            WebApplication.InBackgroundChangeEvent += SetAudioState;
        }

        private void OnDisable()
        {
            _socialNetwork.AdsStarted -= OnAdsStarted;
            _socialNetwork.AdsEnded -= OnAdsEnded;
            WebApplication.InBackgroundChangeEvent -= SetAudioState;
        }

        private void OnAdsStarted()
        {
            SetAudioState(true);
        }

        private void OnAdsEnded()
        {
            SetAudioState(false);
        }

        private void SetAudioState(bool isMute)
        {
            isMute |= _isPlayerMuteAudio || _socialNetwork.IsAds;
            // Use both pause and volume muting methods at the same time.
            // They're both broken in Web, but work perfect together. Trust me on this.
            AudioListener.pause = isMute;
            AudioListener.volume = AudioListener.pause ? 0f : 1f;
        }
        
        public void SwitchGameAudio()
        {
            _isPlayerMuteAudio = IsGameAudioOn;
            SetAudioState(IsGameAudioOn);
        }
    }
}
