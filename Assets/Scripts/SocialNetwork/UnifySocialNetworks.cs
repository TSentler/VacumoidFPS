using System.Collections;
using SocialNetwork.Factory;
using SocialNetwork.Mobile;
using UnityEngine;
using UnityEngine.Events;

namespace SocialNetwork
{
    [DisallowMultipleComponent,
     RequireComponent(typeof(Ads))]
    public class UnifySocialNetworks : MonoBehaviour
    {
        private Initializer _init;
        private MobileDevice _mobileDevice;
        private UnifiedLeaderboardPlatforms _unifiedLeaderboardPlatforms;
        private Ads _ads;

        public Ads Ads => _ads;

        public UnifiedLeaderboardPlatforms Leaderboard =>
            _unifiedLeaderboardPlatforms;
        
        private void Awake()
        {
            _init = new Initializer(new InitializerFactory().Get());
            _mobileDevice = new MobileDevice(_init,new MobileCheckerFactory().Get());
            _unifiedLeaderboardPlatforms = new UnifiedLeaderboardPlatforms();
            _ads = GetComponent<Ads>();
            _ads.Initialize(_init);
        }

        private void Start()
        {
            StartCoroutine(_init.TryInitializeSdkCoroutine());
        }

        public IEnumerator CheckMobileDeviceCoroutine(
            UnityAction<bool> callback) => _mobileDevice.Check(callback);
       
    }
}
