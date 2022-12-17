using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(RectTransform))]
    public class SafeArea : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        private Rect _safeArea = new Rect();
        private ScreenOrientation _orientation = ScreenOrientation.AutoRotation;
        private RectTransform _panel;

        private void OnValidate()
        {
            if (_canvas == null)
                Debug.LogWarning("Canvas was not found!", this);
        }

        private void Awake()
        {
            _panel = GetComponent<RectTransform>();
        }

        private void Start()
        {
            RememberScreen();
            ApplySafeArea();
        }

        private void RememberScreen()
        {
            _orientation = Screen.orientation;
            _safeArea = Screen.safeArea;
        }

        private void SetPanelAnchors()
        {
            Rect safeArea = Screen.safeArea;

            Vector2 anchorMin = safeArea.position;
            Vector2 anchorMax = safeArea.position + safeArea.size;

            anchorMin.x /= _canvas.pixelRect.width;
            anchorMin.y /= _canvas.pixelRect.height;

            anchorMax.x /= _canvas.pixelRect.width;
            anchorMax.y /= _canvas.pixelRect.height;

            _panel.anchorMin = anchorMin;
            _panel.anchorMax = anchorMax;
        }

        private void ApplySafeArea()
        {
            SetPanelAnchors();
            RememberScreen();
        }

        private void Update()
        {
            if ((_orientation != Screen.orientation) 
                || (_safeArea != Screen.safeArea))
            {
                ApplySafeArea();
            }
        }
    }
}
