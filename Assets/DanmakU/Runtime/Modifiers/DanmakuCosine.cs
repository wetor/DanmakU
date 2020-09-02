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
    [AddComponentMenu("DanmakU/Modifiers/Danmaku Cosine")]
    public class DanmakuCosine : MonoBehaviour, IDanmakuModifier
    {

        /// <summary>
        /// 振幅
        /// </summary>
        public float Amplitude;
        /// <summary>
        /// 周期
        /// </summary>
        public float Period;
        /// <summary>
        /// 初相位
        /// </summary>
        [Range(-Mathf.PI, Mathf.PI)]
        public float Phase;

        public int num;

        public JobHandle UpdateDannmaku(DanmakuPool pool, JobHandle dependency = default(JobHandle))
        {
            if (Mathf.Approximately(Amplitude * Period, 0f)) return dependency;

            num = pool.ActiveCount;

            return new ApplyRandomCosine
            {
                Amplitude = Amplitude,
                Period = Period,
                Phase = Phase,
                Counters = pool.Counters,
                Displacements = pool.Displacements,
                Rotations = pool.Rotations
            }.Schedule(pool.ActiveCount, DanmakuPool.kBatchSize, dependency);
            /*return new ApplyFixedCosine
            {
                Amplitude = Amplitude,
                Period = Period,
                Phase = Phase,
                Counters = pool.Counters,
                Displacements = pool.Displacements,
                Rotations = pool.Rotations
            }.ScheduleBatch(pool.ActiveCount, DanmakuPool.kBatchSize, dependency);*/
        }



        struct ApplyRandomCosine : IJobParallelFor
        {

            public float Amplitude;
            public float Period;
            public float Phase;
            public NativeArray<int> Counters;
            public NativeArray<Vector2> Displacements;
            public NativeArray<float> Rotations;

            public void Execute(int index)
            {
                Vector2 displacement = new Vector2(Period, Amplitude * Mathf.Cos(Mathf.PI / 16 * Counters[index] + Phase));
                Rotations[index] = Mathf.Atan2(displacement.y, displacement.x);
                Displacements[index] = displacement;
            }

        }

        struct ApplyFixedCosine : IJobBatchedFor
        {

            public float Amplitude;
            public float Period;
            public float Phase;
            public NativeArray<int> Counters;
            public NativeArray<Vector2> Displacements;
            public NativeArray<float> Rotations;

            public unsafe void Execute(int start, int end)
            {
                var ptr = (Vector2*)(Displacements.GetUnsafePtr());
                var counterPtr = (int*)(Counters.GetUnsafePtr());
                var rotationsPtr = (float*)(Rotations.GetUnsafePtr());
                var pEnd = ptr + (end - start);
                while (ptr < pEnd)
                {
                    ptr->x = Period;
                    ptr->y = Amplitude * Mathf.Cos(Mathf.PI / 16 * *counterPtr + Phase);
                    *rotationsPtr = Mathf.Atan2(ptr->y, ptr->x);
                    rotationsPtr++;
                    counterPtr++;
                    ptr++;
                }
            }

        }

    }

}