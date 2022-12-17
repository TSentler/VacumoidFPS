using UnityEngine;

namespace Audio
{
    public class AudioMuter : MonoBehaviour
    {
        [SerializeField] private AudioMuteButton _muteButton;

        private BackgroundAudio _backgroundAudio;
            
        private void Awake()
        {
            _backgroundAudio = FindObjectOfType<BackgroundAudio>();
        }

        private void OnEnable()
        {
            _muteButton.Clicked += OnAudioMuteButtonClicked;
        }

        private void OnDisable()
        {
            _muteButton.Clicked -= OnAudioMuteButtonClicked;
        }
        
        private void Start()
        {
            _muteButton.ChangeIcon(_backgroundAudio?.IsGameAudioOn ?? true);
        }

        private void OnAudioMuteButtonClicked()
        {
            _backgroundAudio.SwitchGameAudio();
            _muteButton.ChangeIcon(_backgroundAudio.IsGameAudioOn);
        }
    }
}
