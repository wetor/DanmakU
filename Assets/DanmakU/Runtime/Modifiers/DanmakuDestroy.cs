using Unity.Jobs;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using System.Collections.Generic;

namespace DanmakU.Modifiers
{

    /// <summary>
    /// A MonoBehaviour <see cref="DanmakU.IDanmakuModifier"/> that applies a constant
    /// 自动销毁
    /// </summary>
    [AddComponentMenu("DanmakU/Modifiers/Danmaku Destroy")]
    public class DanmakuDestroy : MonoBehaviour, IDanmakuModifier
    {

        /// <summary>
        /// 生存时间.
        /// </summary>
        public int LifeCounter;


        public JobHandle UpdateDannmaku(DanmakuPool pool, JobHandle dependency = default(JobHandle))
        {
            if (Mathf.Approximately(LifeCounter, 0f)) return dependency;

            return new ApplyRandomDestroy
            {
                LifeCounter = LifeCounter,
                Counters = pool.Counters,
                Times = pool.Times,
                CollisionMasks = pool.CollisionMasks
            }.Schedule(pool.ActiveCount, DanmakuPool.kBatchSize, dependency);
            /*return new ApplyFixedDestroy
            {
                LifeCounter = LifeCounter,
                Counters = pool.Counters,
                Times = pool.Times
            }.ScheduleBatch(pool.ActiveCount, DanmakuPool.kBatchSize, dependency);*/



        }


        struct ApplyRandomDestroy : IJobParallelFor
        {

            public int LifeCounter;
            public NativeArray<int> Counters;
            public NativeArray<float> Times;
            public NativeArray<int> CollisionMasks;

            public void Execute(int index)
            {
                if (Counters[index] >= LifeCounter || CollisionMasks[index] > 0) 
                {
                    Times[index] = float.MinValue;
                }
            }

        }

        struct ApplyFixedDestroy : IJobBatchedFor
        {

            public int LifeCounter;
            public NativeArray<int> Counters;
            public NativeArray<float> Times;

            public unsafe void Execute(int start, int end)
            {
                var ptr = (int*)(Counters.GetUnsafePtr());
                var timePtr = (float*)(Times.GetUnsafePtr());
                var pEnd = ptr + (end - start);
                while (ptr < pEnd)
                {
                    if (*ptr++ >= LifeCounter)
                    {
                        *timePtr = float.MinValue;
                    }
                    timePtr++;

                }
            }

        }

    }

}