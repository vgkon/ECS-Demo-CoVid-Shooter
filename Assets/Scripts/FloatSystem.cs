﻿
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;
using Unity.Physics;

public class FloatSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;

        var jobHandle = Entities
            .WithName("FloatSystem")
            .ForEach((ref PhysicsVelocity physics, ref Translation position,
            ref Rotation rotation, ref FloatData floatData) =>
            {
                float s = math.sin((deltaTime + position.Value.x) * .5f) * floatData.speed;
                float c = math.cos((deltaTime + position.Value.y) * .5f) * floatData.speed;

                float3 dir = new float3(s, c, s);
                physics.Linear += dir;
            })
            .Schedule(inputDeps);

        jobHandle.Complete();

        return jobHandle;
    }
}
