using UnityEngine;

namespace LevelLoader
{
    [ExecuteInEditMode]
    public class WallPainter : MonoBehaviour
    {
        [SerializeField] private Material _wallMaterial;
        [SerializeField] private Color _color, _colorShaded;

        private void Awake()
        {
            _wallMaterial.SetColor("_Color", _color);
            _wallMaterial.SetColor("_ColorDim", _colorShaded);
            _wallMaterial.SetColor("_ColorDimSteps", _colorShaded);
            _wallMaterial.SetColor("_ColorDimCurve", _colorShaded);
        }
    }
}
