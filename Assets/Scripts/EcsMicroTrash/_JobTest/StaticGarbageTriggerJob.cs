using Trash;
using Unity.Burst;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Jobs;
/*
[BurstCompile]
public struct StaticGarbageTriggerJob : IJobParallelForTransform
{
    [ReadOnly] public float VacuumRadius;
    [ReadOnly] public Vector3 VacuumPosition;

    [WriteOnly] public NativeArray<bool> Sucked;
    
    public NativeArray<StaticGarbage.Data> Triggers;
    
    public void Execute(int index, TransformAccess transform)
    {
        var trigger = Triggers[index];
        if (trigger.IsSucked)
            return;
        
        var minDistance = trigger.Radius + VacuumRadius;
        var distance = (VacuumPosition - transform.position).magnitude;
        if (distance <= minDistance)
        {
            Sucked[index] = true;
            trigger.Suck();
            Triggers[index] = trigger;
        }
    }
}
*/