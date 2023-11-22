using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Burst;
using Unity.Jobs;

[BurstCompile]
public partial struct MovingSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        //To run the code on main thread without using c# jobs
        //double elapsedTime = SystemAPI.Time.ElapsedTime;
        //foreach (var movement in SystemAPI.Query<VerticalMovementAspect>())
        //{
        //    movement.Move(elapsedTime);

        //}

        //Splitting tasks into multiple jobs
        new MoveAndRotate
        {
            elapsedTime = SystemAPI.Time.ElapsedTime,
        }.ScheduleParallel();
    }
}

[BurstCompile]
readonly partial struct VerticalMovementAspect : IAspect
{
    readonly RefRW<LocalTransform> m_Transform;    
    readonly RefRO<Speed> m_Speed;    

    public void Move(double elapsedTime)
    {
        //Applying back and forth motion
        m_Transform.ValueRW.Position.y = (float)math.sin(elapsedTime * m_Speed.ValueRO.value);

        //Applying Rotation
        m_Transform.ValueRW = m_Transform.ValueRO.RotateY(m_Speed.ValueRO.rotValue);
    }
}


//Job initialization
[BurstCompile]
partial struct MoveAndRotate :IJobEntity 
{
    public double elapsedTime;    
    [BurstCompile]
    void Execute(VerticalMovementAspect vert)
    {
        vert.Move(elapsedTime);
    }
}



