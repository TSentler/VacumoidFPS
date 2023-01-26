using Leopotam.EcsLite;
using UnityEngine;

namespace EcsMicroTrash.Components
{
    public struct Trigger : IEcsAutoReset<Trigger>
    {
        public float Radius;
        public Transform Transform;
        
        public void AutoReset(ref Trigger trigger)
        {
            trigger.Radius = 0.06f;
        }
    }
}