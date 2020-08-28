using Unity.Jobs;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace DanmakU.Modifiers
{

    /// <summary>
    /// A MonoBehaviour <see cref="DanmakU.IDanmakuModifier"/> that applies a constant
    /// displacement to all bullets.
    /// </summary>
    [AddComponentMenu("DanmakU/Modifiers/Danmaku Displacement")]
    public class DanmakuDisplacement : MonoBehaviour, IDanmakuModifier
    {

        /// <summary>
        /// 振幅.
        /// </summary>
        public Range Amplitude;
        /// <summary>
        /// 周期.
        /// </summary>
        public Range xSpeed;

        public int num;

        public JobHandle UpdateDannmaku(DanmakuPool pool, JobHandle dependency = default(JobHandle))
        {
            var amplitude = Amplitude * Time.deltaTime;
            if (amplitude.Approximately(0f)) return dependency;
            num = pool.ActiveCount;
            return new ApplyRandomDisplacement
            {
                Amplitude = amplitude.Center,
                xSpeed = xSpeed.Center,
                Counters = pool.Counters,
                Displacements = pool.Displacements,
                Rotations = pool.Rotations
            }.Schedule(pool.ActiveCount, DanmakuPool.kBatchSize, dependency);

        }

        /*struct ApplyFixedDisplacement : IJobBatchedFor
        {

            public float Range;
            public NativeArray<float> Times;
            public NativeArray<Vector2> Displacements;

            public unsafe void Execute(int start, int end)
            {
                var ptr = (Vector2*)(Displacements.GetUnsafePtr());
                var timePtr = (float*)(Times.GetUnsafePtr());
                var pEnd = ptr + (end - start);
                while (ptr < pEnd)
                {
                    ptr->x = 0.05f;
                    ptr->y = Mathf.Sin(Mathf.PI / 6 * *(timePtr++) * 60 / 6) * Range;
                    ptr++;


                }
            }

        }*/

        struct ApplyRandomDisplacement : IJobParallelFor
        {

            public float Amplitude;
            public float xSpeed;
            public NativeArray<int> Counters;
            public NativeArray<Vector2> Displacements;
            public NativeArray<float> Rotations;

            public void Execute(int index)
            {
                Vector2 displacement = new Vector2(xSpeed, Amplitude * Mathf.Cos(Mathf.PI / 16 * Counters[index]));
                Rotations[index] = Mathf.Atan2(displacement.y, displacement.x);
                Displacements[index] = displacement;
            }

        }

    }

}