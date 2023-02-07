using UnityEngine;

namespace EcsMicroTrash.StaticData
{
    [CreateAssetMenu(fileName = "MicroGarbageStaticData", menuName = "MicroGarbageStaticData", order = 0)]
    public class MicroGarbageStaticData : ScriptableObject
    {
        public float Radius = 0.05f,
            MinRotationRatio = 0.01f,
            MaxRotationRatio = 1f,
            MinVelocity = 0.01f,
            MaxVelocity = 0.3f,
            Acceleration = 0.5f,
            Damp = 12;
            
    }
}