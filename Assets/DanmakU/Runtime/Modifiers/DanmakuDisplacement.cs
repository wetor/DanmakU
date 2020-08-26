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
        /// The acceleration applied to bullets. Units is in game units per second per second.
        /// </summary>
        public Range Range;

        public JobHandle UpdateDannmaku(DanmakuPool pool, JobHandle dependency = default(JobHandle))
        {
            var range = Range * Time.deltaTime;
            if (range.Approximately(0f)) return dependency;
            if (Mathf.Approximately(range.Size, 0f))
            {
                return new ApplyRandomDisplacement
                {
                    Range = range.Center,
                    Times = pool.Times,
                    Displacements = pool.Displacements
                }.Schedule(pool.ActiveCount, DanmakuPool.kBatchSize, dependency);
                return new ApplyFixedDisplacement
                {
                    Range = range.Center,
                    Times = pool.Times,
                    Displacements = pool.Displacements
                }.ScheduleBatch(pool.ActiveCount, DanmakuPool.kBatchSize, dependency);
            }
            else
            {
                return new ApplyRandomDisplacement
                {
                    Range = range.Center,
                    Times = pool.Times,
                    Displacements = pool.Displacements
                }.Schedule(pool.ActiveCount, DanmakuPool.kBatchSize, dependency);
            }
        }

        struct ApplyFixedDisplacement : IJobBatchedFor
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

        }

        struct ApplyRandomDisplacement : IJobParallelFor
        {

            public float Range;
            public NativeArray<float> Times;
            public NativeArray<Vector2> Displacements;

            public void Execute(int index)
            {
                Displacements[index] = new Vector2(0.03f, Mathf.Sin(Mathf.PI / 6 * Times[index]*60 /6) * Range);

            }

        }

    }

}