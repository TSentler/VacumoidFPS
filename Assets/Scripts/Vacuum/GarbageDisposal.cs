using Trash;
using UnityEngine;

namespace Vacuum
{
    public class GarbageDisposal : MonoBehaviour
    {
        [SerializeField] private VacuumBag _bag;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ITrashCollectable trash))
            {
                trash.Sucked();
                _bag.AddTrashPoints(trash.TrashPoints);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            OnTriggerEnter(other);
        }
    }
}
