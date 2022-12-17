using Trash;
using UnityEngine;
using UnityEngine.Events;

namespace Vacuum
{
    public class GarbageDisposal : MonoBehaviour
    {
        [SerializeField] private VacuumBag _bag;

        public event UnityAction<Garbage> Collected;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Garbage garbage))
            {
                garbage.Sucked();
                _bag.AddTrashPoints(garbage.TrashPoints);
                Collected(garbage);
            }
        }
    }
}
