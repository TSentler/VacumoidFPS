using Trash;
using Unity.Mathematics;
using UnityEngine;

namespace EcsMicroTrash.Components
{
    public struct StaticGarbageComponent
    {
        public StaticGarbageComponent(StaticGarbage staticGarbage)
        {
            _staticGarbage = staticGarbage;
            var data = new StaticGarbage.Data(staticGarbage);
            Position = data.Position;
            Garbage = data.Garbage;
            IsSucked = false;
        }
        
        private readonly StaticGarbage _staticGarbage;

        public float3 Position;
        public bool IsSucked; 
        
        public MicroGarbage Garbage { get; }
        
        public void Suck()
        {
            IsSucked = true;
            _staticGarbage.Suck();
        }
    }
}