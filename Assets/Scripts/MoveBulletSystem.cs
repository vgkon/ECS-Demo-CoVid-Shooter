using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;
using Unity.Physics;

public class MoveBulletSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;

        var jobHandle = Entities
            .WithName("FloatSystem")
            .ForEach((ref PhysicsVelocity physics, ref Translation position,
            ref Rotation rotation, ref BulletData bulletData) =>
            {
                physics.Angular = float3.zero;
                physics.Linear += deltaTime * bulletData.speed * math.forward(rotation.Value);
            })
            .Schedule(inputDeps);

        jobHandle.Complete();

        return jobHandle;
    }
}
