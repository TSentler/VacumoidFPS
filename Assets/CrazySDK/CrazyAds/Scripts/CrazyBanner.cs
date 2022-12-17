using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CrazyGames
{
    public class CrazyBanner : MonoBehaviour
    {
        public enum BannerSize
        {
            Leaderboard_728x90,
            Medium_300x250,
            Mobile_320x50,
            Main_Banner_468x60,
            Large_Mobile_320x100
        }

        [HideInInspector] public string id;

        [SerializeField] private BannerSize _bannerSize;
        [SerializeField] private Image _image;
        [SerializeField] private RectTransform _banner;

        private bool _visible;

        private Vector2[] _sizes = new Vector2[]
        {
            new Vector2(728, 90),
            new Vector2(300, 250),
            new Vector2(320, 50),
            new Vector2(468, 60),
            new Vector2(320, 100)
        };

        public BannerSize Size => _bannerSize;
        
        public Vector2 Position
        {
            get
            {
                return _banner.anchoredPosition;
            }
            set
            {
                _banner.anchoredPosition = value;
            }
        }

        private void OnValidate()
        {
            if (_banner != null && _banner.sizeDelta != GetCurrentSize())
            {
                Debug.Log("Banner size updated");
                SetBannerSize(_bannerSize);
            }
        }

        private void Awake()
        {
            _image.color = Color.clear;
            id = Guid.NewGuid().ToString();
            CrazyAds.Instance.registerBanner(this);
        }

        private void OnDestroy()
        {
            _image.color = Color.clear;
            if (CrazyAds.Instance)
                CrazyAds.Instance.unregisterBanner(this);
        }

        private void Start()
        {
            MarkVisible(true);
            CrazyAds.Instance.updateBannersDisplay();
        }

        private Vector2 GetCurrentSize()
        {
            return _sizes[(int)_bannerSize];
        }
        
        public void SetBannerSize(BannerSize bannerSize)
        {
            _bannerSize = bannerSize;
            _banner.sizeDelta = _sizes[(int)bannerSize];
        }

        public void SimulateRender()
        {
            if (_visible)
            {
                _image.color = new Color32(46, 37, 68, 255);
            }
            else
            {
                _image.color = Color.clear;
            }
        }

        public void MarkForRefresh()
        {
            id = Guid.NewGuid().ToString();
        }

        public void MarkVisible(bool visibility)
        {
            _visible = visibility;
        }

        public bool isVisible()
        {
            return _visible;
        }
    }
}