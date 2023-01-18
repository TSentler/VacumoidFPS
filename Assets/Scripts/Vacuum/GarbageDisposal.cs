using Trash;
using UnityEngine;
using UnityEngine.Events;

namespace Vacuum
{
    public class GarbageDisposal : MonoBehaviour
    {
        [SerializeField] private VacuumBag _bag;

        public event UnityAction<ITrashCollectable> Collected;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ITrashCollectable trash))
            {
                trash.Sucked();
                _bag.AddTrashPoints(trash.TrashPoints);
                Collected?.Invoke(trash);
            }
        }
    }
}
