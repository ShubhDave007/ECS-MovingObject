using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SpeedAuthoring : MonoBehaviour
{
    [SerializeField]internal float value;
    [SerializeField] internal float rotValue; 
}

public class SpeedBaker : Baker<SpeedAuthoring>
{
    public override void Bake(SpeedAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new Speed
        {
            value = authoring.value,
            rotValue = authoring.rotValue,
        });       
    }
}
