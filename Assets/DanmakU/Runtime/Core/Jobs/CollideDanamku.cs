using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;

namespace DanmakU
{

    internal struct CollideDanamku : IJobParallelFor
    {

        Bounds2D Bounds;
        [ReadOnly] NativeArray<Vector2> Positions;
        [WriteOnly] NativeArray<int> Collisions;
        [ReadOnly] NativeArray<float> Rotations;
        [ReadOnly] NativeArray<float> Angles;
        [ReadOnly] NativeArray<float> Speeds;
        [ReadOnly] NativeArray<Vector2> Displacements;
        Vector2 displacement;
        float DeltaTime;
        float Size;

        public CollideDanamku(DanmakuPool pool)
        {
            var radius = pool.ColliderRadius;
            Bounds = new Bounds2D(Vector2.zero, new Vector2(radius, radius));
            Positions = pool.Positions;
            Collisions = pool.CollisionMasks;
            Rotations = pool.Rotations;
            Angles = pool.Angles;
            Speeds = pool.Speeds;
            Displacements = pool.Displacements;
            displacement = new Vector2();
            DeltaTime = Time.deltaTime;
            Size = 0;
        }

        public void Execute(int index)
        {
            Bounds.Center = Positions[index];
            //displacement.x = Displacements[index].x + Speeds[index] * Mathf.Cos(Rotations[index]) * DeltaTime;
            //displacement.y = Displacements[index].y + Speeds[index] * Mathf.Sin(Rotations[index]) * DeltaTime;

            Collisions[index] = DanmakuCollider.TestCollisions(Bounds, out Size);
        }

    }

}