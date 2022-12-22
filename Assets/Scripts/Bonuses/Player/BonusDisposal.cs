using UnityEngine;
using UnityEngine.Events;

namespace Bonuses.Player
{
    public class BonusDisposal : MonoBehaviour
    {
        public event UnityAction<Lightning> Boosted;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Lightning lightningBonus))
            {
                Boosted?.Invoke(lightningBonus);
            }
        }
    }
}
