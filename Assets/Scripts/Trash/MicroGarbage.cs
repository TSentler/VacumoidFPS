using UnityEngine;

namespace Trash
{
    public class MicroGarbage : MonoBehaviour, ITrashCollectable
    {
        [SerializeField] private GameObject _scribble;
        [SerializeField] private float _trashCount;
        
        public bool IsSucked { get; private set; }
        public float TrashPoints => _trashCount;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void SetCount(float trashPoints)
        {
            _trashCount = trashPoints;
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
            _scribble.SetActive(true);
        }
        
        public void Sucked()
        {
            IsSucked = true;
            gameObject.SetActive(false);
        }
    }
}