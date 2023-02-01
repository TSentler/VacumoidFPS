using Unity.Mathematics;
using UnityEngine;
using Vacuum;

namespace EcsMicroTrash.Components
{
    public struct VacuumComponent
    {
        public VacuumComponent(VacuumRadius vacuumRadius)
        {
            _vacuumRadius = vacuumRadius;
            _transform = vacuumRadius.transform;
            Radius = _vacuumRadius.Radius;
            Position = _vacuumRadius.transform.position;
        }

        public float Radius;
        public float3 Position;
        
        private VacuumRadius _vacuumRadius;
        private Transform _transform;
        
        public void UpdateData()
        {
            Radius = _vacuumRadius.Radius;
            Position = _transform.position;
        }
    }
}