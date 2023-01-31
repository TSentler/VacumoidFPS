using Trash;
using UnityEngine;

namespace EcsMicroTrash.Components
{
    public struct StaticGarbageComponent
    {
        public StaticGarbageComponent(StaticGarbage staticGarbage)
        {
            _staticGarbage = staticGarbage;
            Data = new StaticGarbage.Data(staticGarbage);
            IsSucked = false;
        }
        
        private StaticGarbage _staticGarbage;
        
        public StaticGarbage.Data Data { get; }
        public bool IsSucked { get; private set; }
        
        public void Suck()
        {
            IsSucked = true;
            _staticGarbage.Suck();
        }
    }
}