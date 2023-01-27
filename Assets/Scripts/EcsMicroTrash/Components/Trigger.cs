using Leopotam.EcsLite;
using Trash;

namespace EcsMicroTrash.Components
{
    public struct Trigger : IEcsAutoReset<Trigger>
    {
        public float Radius;
        public MicroGarbageStaticTrigger StaticTrigger;
        
        public void AutoReset(ref Trigger trigger)
        {
            trigger.Radius = 0.06f;
        }
    }
}