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

        private VacuumRadius _vacuumRadius;
        private Transform _transform;
        
        public float Radius { get; set; }
        public Vector3 Position { get; set; }
        
        public void UpdateData()
        {
            Radius = _vacuumRadius.Radius;
            Position = _transform.position;
        }

    }
}