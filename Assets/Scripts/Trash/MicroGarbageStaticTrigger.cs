using Suckables;
using UnityEngine;

namespace Trash
{
    public class MicroGarbageStaticTrigger : MonoBehaviour, ISuckable
    {
        [SerializeField] private MicroGarbage _garbage;

        private void OnValidate()
        {
            if (_garbage == null)
            {
                Debug.LogError("MicroGarbage not to found", this);
            }
        }

        public void Suck()
        {
            _garbage.transform.parent = transform.parent;
            _garbage.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        public void SetCount(float oneGarbageCount)
        {
            _garbage.SetCount(oneGarbageCount);
        }
    }
}