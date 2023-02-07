using UnityEngine;

namespace Trash
{
    public class MicroGarbage : MonoBehaviour, ITrashCollectable
    {
        [SerializeField] private float _trashCount;
        
        public bool IsSucked { get; private set; }
        public float TrashPoints => _trashCount;

        public void SetCount(float trashPoints)
        {
            _trashCount = trashPoints;
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        public void Sucked()
        {
            IsSucked = true;
            gameObject.SetActive(false);
        }
    }
}